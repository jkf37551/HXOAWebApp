using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    /// <summary>
    /// 页面模型
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 本页显示显示数据
        /// </summary>
        public object rows { get; set; }
    }
}
