using DLL.Models.MainDB;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Controllers
{
    public class BaseController<T> : Controller where T : class, new()
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserModel _LoginUser;
        /// <summary>
        /// 页面模型
        /// </summary>
        public PageModel<T> _pageInfo;

        public BaseController()
        {
            //初始化页面模型
            _pageInfo = new PageModel<T>()
            {
                QueryModel = new T(),
                Current_PageId = 1, //默认查询第一页
                Page_Size = 20, //默认每页显示20条记录
                Total_Page = 1, //初始化页面数为1
                Total_Recoder = 0,  //初始化记录查询条目数为0
                PageQueryUrl = ""   //设置分页查询地址
            };
        }
        
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        public UserModel GetLoginUser()
        {
            try
            {
                return (UserModel)Session[ConstDictionary.LoginUser];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 操作成功提示信息
        /// </summary>
        /// <param name="Url">要跳转的地址</param>
        /// <returns></returns>
        public ActionResult SysInfo(string Url)
        {
            return Content("<script>alert('操作成功！');window.location.href = '" + Url + "';</script>");
        }

        /// <summary>
        /// 操作失败提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        public ActionResult SysErro(string Msg)
        {
            Msg = Msg.Replace("'", "\\'").Replace("\"", "\\\"").Replace("\n", "").Replace("\t", "").Replace("\r","");
            return Content("<script>alert('操作失败！" + Msg + "');window.history.go(-1);</script>");
        }
        
    }
}