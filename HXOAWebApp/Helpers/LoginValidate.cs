using HXOAWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXOAWebApp.Helpers
{
    public class LoginValidate : FilterAttribute, IAuthorizationFilter
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
                    filterContext.Result = new RedirectResult("/Home/SysLogin");
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("/Home/SysLogin/" + ex.Message);
            }
        }
    }
}