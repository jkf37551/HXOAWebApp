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
        public string id { get; set; }
        public string code { get; set; }
        public string text { get; set; }
        /// <summary>
        /// 默认展示页
        /// </summary>
        public string homePage { get; set; }
        public List<MenuInfo> menu { get; set; }
    }

    /// <summary>
    /// 标题信息
    /// </summary>
    public class MenuInfo
    {
        public string id { get; set; }
        public string code { get; set; }
        public string text { get; set; }
        public List<FuncInfo> items { get; set; }
    }

    /// <summary>
    /// 菜单信息
    /// </summary>
    public class FuncInfo
    {
        public string id { get; set; }
        public string code { get; set; }
        public string text { get; set; }
        public string href { get; set; }
    }
}