using OpenAuth.Repository.Domain;
using System.Collections.Generic;

namespace OpenAuth.App.Response
{
    public class FlowVerificationResp : FlowInstance
    {
        /// <summary>
        /// 预览表单数据
        /// </summary>
        public string FrmPreviewHtml
        {
            get { return FormUtil.Preview(this); }
        }

        /// <summary>
        /// 预览表单数据表单项包含读写控制权限）
        /// </summary>
        public string FrmHtml
        {
            get
            {
                if (this.FrmType != 0)  //只有开原版动态表单才需要转换
                {
                    return string.Empty;
                }

                return FormUtil.GetHtml(this.FrmContentData, this.FrmContentParse, this.FrmData, "", this.CanWriteFormItemIds);
            }
        }

        /// <summary>
        /// 下个节点的执行权限方式
        /// </summary>
        public string NextNodeDesignateType { get; set; }

        /// <summary>
        /// 当前节点的可写表单Id
        /// </summary>
        public string[] CanWriteFormItemIds { get; set; }
    }
    public class switchFlow
    {
        public string bxm { get; set; }
        public decimal JE { get; set; }
        public string FlowSchemesId { get; set; }
        public List<switchFlow_SignerType> data { get; set; }
    }
    public class switchFlow_SignerType
    {
        public string Signer { get; set; }
        //public string SignerId { get; set; }
        public string SignerType { get; set; }
        /// <summary>
        /// 经费号
        /// </summary>
        public string NodeCode { get; set; }
        /// <summary>
        /// AuditOrSign
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// SignReason
        /// </summary>
        public string Description { get; set; }
    }
}
