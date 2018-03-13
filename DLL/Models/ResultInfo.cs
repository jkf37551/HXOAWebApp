using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class ResultInfo<T>
    {
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
    }
}
