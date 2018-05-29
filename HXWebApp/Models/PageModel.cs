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
    }
}