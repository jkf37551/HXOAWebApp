using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp
{
    // 用户登录验证
    public class LoginValidate : FilterAttribute, IAuthorizationFilter, IExceptionFilter
    {
        /// <summary>
        /// 自定义扩展属性，验证用户登录
        /// </summary>
        /// <param name="filterContext">AuthorizationContext 类将封装有关控制器、HTTP 上下文、请求上下文、路由数据、操作描述符和操作结果的信息。</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext == null)
                    throw new Exception("HTTP 上下文不存在！");

                if (filterContext.HttpContext.Session == null)
                    throw new Exception("服务器Session不可用！");

                if (filterContext.HttpContext.Session[ConstDictionary.LoginUser] == null)
                    filterContext.Result = new RedirectResult("/Home/Login/");
            }
            catch (Exception ex)
            {
                filterContext.Result = new Controllers.BaseController<object>().SysErro(ex.Message);
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext">异常信息</param>
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;  //表示异常已处理
            filterContext.Result=(new Controllers.BaseController<object>().SysErro(filterContext.Exception.Message));
        }
    }
}