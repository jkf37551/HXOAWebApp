using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultInfo<T>
    {
        #region 属性
        /// <summary>
        /// 操作结果：true操作成功,false操作失败
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 提示信息(错误提示信息)
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public T Data { get; set; }

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
        public int Current_PageId { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 构造函数(默认初始化为操作失败)
        /// </summary>
        public ResultInfo()
        {
            IsSuccess = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccess">操作信息</param>
        public ResultInfo(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccess">操作信息</param>
        /// <param name="code">错误代码</param>
        public ResultInfo(bool isSuccess, string code)
        {
            IsSuccess = isSuccess;
            Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ResultInfo(bool isSuccess, string code, string message)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResultInfo(bool isSuccess, string code, string message, T data)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
            Data = data;
        }
        #endregion
    }
}
