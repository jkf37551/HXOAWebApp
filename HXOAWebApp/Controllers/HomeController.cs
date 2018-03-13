using DLL.Models;
using HXOAWebApp.Helpers;
using HXOAWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXOAWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Login");
            //return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 返回登录页面
        /// </summary>
        /// <param name="Id">提示信息</param>
        /// <returns></returns>
        public ActionResult SysLogin(string Id)
        {
            string LoginMsg = "请先登录系统！";
            if (!string.IsNullOrWhiteSpace(Id))
                LoginMsg = Id;
            ViewData["Info"] = "<script>alert('" + LoginMsg + "');this.location='/home/login';</script>";
            return View("Index");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            ResultInfo<string> resualt = new ResultInfo<string>();
            try
            {
                String UserId, Password;
                UserId = collection["userid"].Trim();
                Password = collection["password"].Trim();

                resualt.IsSuccess = true;
                Session[ConstDictionary.LoginUser] = resualt;

                //var result = UserDAL.ValidateLogin(UserId, Password);
                //if (result.IsSuccess)
                //{
                //    Session.Clear();
                //    if (Session[ConstDict.LoginUser] != null)
                //        Session.Remove(ConstDict.LoginUser);
                //    Session[ConstDict.LoginUser] = UserDAL.GetUserByUserid(UserId).Data;

                //    resualt.IsSuccess = true;
                //}
                //else
                //{
                //    resualt.IsSuccess = false;
                //    resualt.Message = "账号或者密码有误，请重试！";
                //}
            }
            catch (Exception ex)
            {
                resualt.IsSuccess = false;
                resualt.Message = ex.Message;
            }
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(resualt));
            //return Content(fastJSON.JSON.ToJSON(resualt));
        }

        [LoginValidate]
        public ActionResult MainDefault()
        {
            //    try
            //    {
            //        Users loginUser = (Users)Session[ConstDict.LoginUser];
            //        M_SYS_INFO info = new M_SYS_INFO();
            //        info.SYS_UPDATE_CONTENT = GetTree("SYS", loginUser);
            //        return View(info);
            //    }
            //    catch (Exception ex)
            //    {
            //        return SysErroMsg(ex.Message);
            //    }

            return View();
        }

        //获取用户菜单信息
        //private string GetTree(string ID, Users loginUser)
        //{
        //    string TreeStr = "";
        //    try
        //    {
        //        HXOADBDataContext DB = new HXOADBDataContext();
        //        //var RList = DB.M_USER_ROLES.Where(p => p.USERID.ToLower().Trim().Equals(loginUser.UserId.ToLower().Trim())).Select(p => p.ROLE_CODE).ToList();
        //        //var MList = DB.M_ROLE_MENUS.Where(p => RList.Contains(p.ROLE_CODE)).Select(p => p.MENU_CODE).ToList();
        //        //var Minfo = DB.M_MENU_INFO.Where(p => MList.Contains(p.MENU_CODE) && p.CODEID_FOR_MENUSTATUS.Trim().Equals("1")).ToList();
        //        var Minfo = (from v in DB.M_MENU_INFO
        //                     from p in DB.M_ROLE_MENUS
        //                     from k in DB.M_USER_ROLES
        //                     where k.USERID.ToLower().Trim().Equals(loginUser.UserId.ToLower().Trim()) && k.ROLE_CODE.ToLower().Trim().Equals(p.ROLE_CODE.ToLower().Trim())
        //                     && p.MENU_CODE.ToLower().Trim().Equals(v.MENU_CODE.ToLower().Trim()) && v.CODEID_FOR_MENUSTATUS.ToLower().Trim().Equals("1")
        //                     select v).Distinct().ToList();
        //        var items = Minfo.Where(p => p.MENU_UPMENU.ToLower().Trim().Equals(ID.ToLower().Trim())).ToList();

        //        if (items != null && items.Count > 0)
        //        {
        //            foreach (var item in items)
        //            {
        //                if (item.MENU_CODE.Equals("MAIN_BUSY")) //默认选中系统业务
        //                    TreeStr = TreeStr + "<div title=\"" + item.MENU_NAME + "\" iconCls=\"icon-light\" selected=\"true\" style=\"overflow:auto;padding:10px;\">\n";
        //                else
        //                    TreeStr = TreeStr + "<div title=\"" + item.MENU_NAME + "\" iconCls=\"icon-light\" selected=\"false\" style=\"overflow:auto;padding:10px;\">\n";

        //                TreeStr = TreeStr + "<ul class=\"easyui-tree\">\n";
        //                var v1 = Minfo.Where(p => p.MENU_UPMENU.Equals(item.MENU_CODE)).ToList();
        //                if (v1 != null && v1.Count > 0)
        //                {
        //                    foreach (var f1 in v1)
        //                    {
        //                        if (f1.MENU_CODE.Equals("BUSY_MYWORK")) //默认展开个人办公
        //                            TreeStr = TreeStr + "<li state=\"open\">\n";
        //                        else
        //                            TreeStr = TreeStr + "<li state=\"closed\">\n";
        //                        TreeStr = TreeStr + "<span>" + f1.MENU_NAME + "</span>";
        //                        TreeStr = TreeStr + "<ul>\n";

        //                        var v2 = Minfo.Where(p => p.MENU_UPMENU.Equals(f1.MENU_CODE)).ToList();
        //                        if (v2 != null && v2.Count > 0)
        //                        {
        //                            foreach (var f2 in v2)
        //                            {
        //                                TreeStr = TreeStr + "<li><span><a link=\"" + f2.MENU_URL + "\">" + f2.MENU_NAME + "</a></span></li>";
        //                            }
        //                        }
        //                        TreeStr = TreeStr + "</ul>\n";
        //                        TreeStr = TreeStr + "</li>\n";
        //                    }
        //                }
        //                TreeStr = TreeStr + "</ul>\n";
        //                TreeStr = TreeStr + "</div>\n";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysErroMsg(ex.Message);
        //    }
        //    return TreeStr;
        //}
    }
}