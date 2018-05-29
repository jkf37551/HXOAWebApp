using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HXWebApp.Models
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeModel
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 默认展示页ID
        /// </summary>
        public string homePage { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<MenuInfo> menu { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Ifchecked { get; set; }
    }

    /// <summary>
    /// 标题信息
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 功能菜单列表
        /// </summary>
        public List<FuncInfo> items { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Ifchecked { get; set; }
    }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public class FuncInfo
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 功能代码(模块代码)
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 功能URL地址
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Ifchecked { get; set; }
    }
}