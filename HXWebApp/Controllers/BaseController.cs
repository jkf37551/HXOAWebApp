using DB;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public Users GetLoginUser()
        {
            try
            {
                return (Users)Session[ConstDictionary.LoginUser];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}