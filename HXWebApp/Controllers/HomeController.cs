using DB;
using DLL.MAINBussiness;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [LoginValidate]
        public ActionResult Index()
        {
            //登录用户信息
            Users loginuser = GetLoginUser();
            //菜单信息
            List<TreeModel> treeItem = new List<TreeModel>();
            var menulist = UserDAL.GetUserMenusByUserId(loginuser);
            if (menulist.IsSuccess)
            {
                treeItem = menulist.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_1") && p.CODE_FOR_STATUS.Equals("1"))
                    .Select(v => new TreeModel()
                    {
                        id = v.MENU_ID.ToString(),
                        code = v.MENU_CODE,
                        text = v.MENU_NAME
                    }).ToList();
                for (int i = 0; i < treeItem.Count; i++)
                {
                    treeItem[i].menu = menulist.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_2") && p.CODE_FOR_STATUS.Equals("1") && p.MENU_UPMENU.Equals(treeItem[i].code))
                        .Select(v => new MenuInfo()
                        {
                            id = v.MENU_ID.ToString(),
                            code = v.MENU_CODE,
                            text = v.MENU_NAME
                        }).ToList();
                    for (int j = 0; j < treeItem[i].menu.Count; j++)
                    {
                        treeItem[i].menu[j].items = menulist.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_3") && p.CODE_FOR_STATUS.Equals("1") && p.MENU_UPMENU.Equals(treeItem[i].menu[j].code))
                            .Select(v => new FuncInfo()
                            {
                                id = v.MENU_ID.ToString(),
                                code = v.MENU_CODE,
                                text = v.MENU_NAME,
                                href = v.MENU_URL
                            }).ToList();
                    }
                }
            }
            treeItem = treeItem.OrderBy(p => p.id).ToList();
            ViewBag.menuList = treeItem;
            ViewBag.menuStr = Newtonsoft.Json.JsonConvert.SerializeObject(treeItem);
            return View(loginuser);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //清除登录用户信息
            Session.Remove(ConstDictionary.LoginUser);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = UserDAL.LoginValidate(new Users() { UserId = model.UserId, Password = model.Password });
            if (result.IsSuccess)
            {
                //设置用户登录信息
                Session.Add(ConstDictionary.LoginUser, result.Data);
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }
    }
}