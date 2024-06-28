using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenAuth.App.Interface;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.Repository;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Interface;

namespace OpenAuth.App
{
    public class FlowSchemeApp :BaseStringApp<FlowScheme,OpenAuthDBContext>
    {
        public void Add(FlowScheme flowScheme)
        {
            if (Repository.Any(u => u.SchemeName == flowScheme.SchemeName))
            {
                throw new Exception("流程名称已经存在");
            }

            var user = _auth.GetCurrentUser().User;
            flowScheme.CreateUserId = user.Id;
            flowScheme.CreateUserName = user.Name;
            Repository.Add(flowScheme);
        }

        public FlowScheme FindByCode(string code)
        {
            return Repository.FirstOrDefault(u => u.SchemeCode == code);
        }

        public void Update(FlowScheme flowScheme)
        {
            if (Repository.Any(u => u.SchemeName == flowScheme.SchemeName && u.Id != flowScheme.Id))
            {
                throw new Exception("流程名称已经存在");
            }

            UnitWork.Update<FlowScheme>(u => u.Id == flowScheme.Id, u => new FlowScheme
            {
                SchemeContent = flowScheme.SchemeContent,
                SchemeName = flowScheme.SchemeName,
                ModifyDate = DateTime.Now,
                FrmId = flowScheme.FrmId,
                FrmType = flowScheme.FrmType,
                Disabled = flowScheme.Disabled,
                Description = flowScheme.Description,
                OrgId = flowScheme.OrgId
            });
        }

        public async Task<TableData> Load(QueryFlowSchemeListReq request)
        {
            var result = new TableData();
            var objs = GetDataPrivilege("u");
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.SchemeName.Contains(request.key) || u.Id.Contains(request.key));
            }

            result.data = await objs.OrderByDescending(u => u.CreateDate)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).ToListAsync();
            result.count = await objs.CountAsync();
            return result;
        }

        public TableData Swicth(string key, decimal JE)
        {
            var result = new TableData();

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("字段key为空");
            }
            var objs1 = GetDataPrivilege("u");
            //开头与关键字相同
            IQueryable<FlowScheme> objs = objs1.Where(u => u.SchemeName.StartsWith(key));

            //从数据库读取流程和网报的配对,
            //如果读取到则从流程获取顺序

            FlowScheme sys_configdetail = new FlowScheme();

            if (objs.Count() == 0)
            {
                //找不到匹配类型,返回默认流程
                IQueryable<FlowScheme> aabb = objs1.Where(u => u.SchemeName == "默认");
                //IQueryable<FlowScheme> aabb = objs.Where(u => u.SchemeName == "默认");

                ////.FirstOrDefault();
                //var aadsf = objs.Where(u => u.SchemeName.Contains("默认"));
                //.FirstOrDefault();
                if (aabb.Count() == 0)
                {
                    throw new Exception("未设置名称为'默认'的流程模板");
                }
                sys_configdetail = aabb.FirstOrDefault();
            }
            else if (objs.Count() == 1)
            {
                //找到1个匹配,直接返回
                sys_configdetail = objs.FirstOrDefault();
            }
            else
            {
                //找到多个
                //根据保修总金额筛选
                decimal a = JE;
                foreach (FlowScheme s in objs)
                {
                    string[] _n = s.SchemeName.Split("_");
                    decimal value = decimal.Parse(_n[2]);
                    switch (_n[1])
                    {
                        case "大于":
                            if (a > value) sys_configdetail = s;
                            break;
                        case "大于等于":
                            if (a >= value) sys_configdetail = s;
                            break;
                        case "小于":
                            if (a < value) sys_configdetail = s;
                            break;
                        case "小于等于":
                            if (a <= value) sys_configdetail = s;
                            break;
                        default:
                            continue;
                    }
                }
            }

            //转换模型
            schemeContentModel schemeModel = JsonConvert.DeserializeObject<schemeContentModel>(sys_configdetail.SchemeContent);

            List<NodesItem> nodeNames = orderby(schemeModel);
            List<string> nodelist = new List<string>();
            foreach(NodesItem n in nodeNames)
            {
                nodelist.Add(n.name);
            }
            var ggg = new { nodelist , id = sys_configdetail.Id };

            result.data = ggg;
            return result;
        }
        /// <summary>
        /// 给流程节点排序
        /// </summary>
        /// <param name="schemeModel"></param>
        /// <returns></returns>
        public List<NodesItem> orderby(schemeContentModel schemeModel)
        {
            //从这里给节点排序
            string startnodeid = schemeModel.nodes.Find(v => v.type == "start round mix").id;
            string nextid = startnodeid;
            //按顺序输出的名称
            List<NodesItem> nodeNames = new List<NodesItem>();
            while (nextid != "")
            {
                LinesItem node2 = schemeModel.lines.Find(v => v.from == nextid);
                if (node2 == null)
                {
                    nextid = "";
                }
                else
                {
                    NodesItem node = schemeModel.nodes.Find(v => v.id == nextid);
                    if (node.type == "node")
                    {
                        nodeNames.Add(node);
                    }
                    nextid = node2.to;
                }
            }
            return nodeNames;
        }

        public FlowSchemeApp(IUnitWork<OpenAuthDBContext> unitWork, IRepository<FlowScheme, OpenAuthDBContext> repository, IAuth auth) : base(unitWork, repository, auth)
        {
        }
    }
}
