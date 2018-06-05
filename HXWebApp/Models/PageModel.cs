using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HXWebApp.Models
{
    /// <summary>
    /// 页面模型
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 查询模型
        /// </summary>
        public T QueryModel { get; set; }
        /// <summary>
        /// 查询结果
        /// </summary>
        public List<T> QueryData { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total_Recoder { get; set; }
        /// <summary>
        /// 每页显示数(最大)
        /// </summary>
        public int Page_Size { get; set; }
        /// <summary>
        /// 总页面数
        /// </summary>
        public int Total_Page { get; set; }
        /// <summary>
        /// 当前页面ID(要查询页面ID)
        /// </summary>
        public int Current_PageId{get;set;}
        
        /// <summary>
        /// 分页查询页面地址
        /// </summary>
        public string PageQueryUrl { get; set; }
    }
}