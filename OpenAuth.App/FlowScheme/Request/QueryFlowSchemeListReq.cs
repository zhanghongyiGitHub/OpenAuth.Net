using OpenAuth.App.Flow;
using System.Collections.Generic;

namespace OpenAuth.App.Request
{
    public class QueryFlowSchemeListReq : PageReq
    {
        public string orgId { get; set; }
    }

    public class schemeContentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int initNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LinesItem> lines { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NodesItem> nodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> areas { get; set; }
    }

    public class LinesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @from { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Cls cls { get; set; }
    }

    public class Cls
    {
        /// <summary>
        /// 
        /// </summary>
        public string linkType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int linkThickness { get; set; }
    }

    public class NodesItem
    {  /// <summary>
       /// 
       /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string defaultIcon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string belongto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public setInfo setInfo { get; set; }
    }

    public class setInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int NodeRejectType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NodeConfluenceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NodeDesignate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThirdPartyUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NodeDesignateData NodeDesignateData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> CanWriteFormItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfluenceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConfluenceOk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NodeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Taged { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TagedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 黎思思
        /// </summary>
        public string UserName { get; set; }
    }

    public class NodeDesignateData
    {  /// <summary>
       /// 
       /// </summary>
        public List<string> users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> orgs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Texts { get; set; }
    }
}
