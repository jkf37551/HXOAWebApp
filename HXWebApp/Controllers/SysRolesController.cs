using DB;
using DLL.Models.MainDB;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Controllers
{
    [LoginValidate]
    public class SysRolesController : BaseController<SYS_ROLE_INFOModel>
    {
        // GET: SysRoles
        public ActionResult List()
        {
            return View(_pageInfo);
        }

        public ActionResult Query(PageModel<SYS_ROLE_INFOModel> Model)
        {
            _pageInfo = Model;
            _pageInfo.QueryData = Model.QueryModel.GetList(Model.QueryModel).Data;
            return View("List", _pageInfo);
        }

        public ActionResult Create()
        {
            return View(new SYS_ROLE_INFOModel());
        }

        [HttpPost]
        public ActionResult Create(SYS_ROLE_INFOModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var resualt = Model.Insert(Model, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysRoles/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }

        public ActionResult Edit(long id)
        {
            var resualt = new SYS_ROLE_INFOModel().GetEnetityByID(id);
            if (resualt.IsSuccess)
            {
                //操作成功
                return View(resualt.Data);
            }
            else
            {
                //操作失败
                return SysErro(resualt.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(SYS_ROLE_INFOModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var resualt = Model.Update(Model, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysRoles/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }

        public ActionResult Delete(long id)
        {
            var resualt = new SYS_ROLE_INFOModel().Delete(id, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysRoles/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }

        public ActionResult RoleMenus(long id)
        {
            RoleMenusInfo roleInfo = new RoleMenusInfo();
            //获取角色信息
            var r_role = _pageInfo.QueryModel.GetEnetityByID(id);
            if (r_role.IsSuccess)
                roleInfo.roleInfo = r_role.Data;
            //菜单信息
            List<TreeModel> treeItem = new List<TreeModel>();
            //获取所有菜单信息
            var AllMenus = new SYS_MENU_INFOModel().GetList(new SYS_MENU_INFOModel());
            if (AllMenus.IsSuccess)
            {
                //获取角色菜单
                var rolemenus = _pageInfo.QueryModel.GetRoleMenus(id);
                List<string> Menulist = new List<string>();
                if (rolemenus.IsSuccess)
                    Menulist = rolemenus.Data.Select(p => p.MENU_CODE).ToList();
                treeItem = AllMenus.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_1") && p.CODE_FOR_STATUS.Equals("1"))
                .Select(v => new TreeModel()
                {
                    id = v.ID.ToString(),
                    code = v.MENU_CODE,
                    text = v.MENU_NAME,
                    Ifchecked = Menulist.Contains(v.MENU_CODE)
                }).ToList();
                for (int i = 0; i < treeItem.Count; i++)
                {
                    treeItem[i].menu = AllMenus.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_2") && p.CODE_FOR_STATUS.Equals("1") && p.MENU_UPMENU.Equals(treeItem[i].code))
                        .Select(v => new MenuInfo()
                        {
                            id = v.ID.ToString(),
                            code = v.MENU_CODE,
                            text = v.MENU_NAME,
                            Ifchecked = Menulist.Contains(v.MENU_CODE)
                        }).ToList();
                    for (int j = 0; j < treeItem[i].menu.Count; j++)
                    {
                        treeItem[i].menu[j].items = AllMenus.Data.Where(p => p.CODE_FOR_MENU_LEVEL.Equals("MENU_LEVEL_3") && p.CODE_FOR_STATUS.Equals("1") && p.MENU_UPMENU.Equals(treeItem[i].menu[j].code))
                            .Select(v => new FuncInfo()
                            {
                                id = v.ID.ToString(),
                                code = v.MENU_CODE,
                                text = v.MENU_NAME,
                                href = v.MENU_URL,
                                Ifchecked = Menulist.Contains(v.MENU_CODE)
                            }).ToList();
                    }
                }
            }

            roleInfo.MenuInfo = treeItem;
            return View(roleInfo);
        }

        [HttpPost]
        public ActionResult RoleMenus(RoleMenusInfo menuifo)
        {
            List<M_ROLE_MENUS> rmList = new List<M_ROLE_MENUS>();
            //获取角色菜单
            bool ifcheck = false;
            if (menuifo != null && menuifo.MenuInfo != null)
            {
                foreach (var s in menuifo.MenuInfo)
                {
                    ifcheck = (s.Ifchecked || ifcheck);
                    if (s.menu != null)
                    {
                        foreach (var m in s.menu)
                        {
                            ifcheck = (m.Ifchecked || ifcheck);
                            if (m.items != null)
                            {
                                foreach (var f in m.items)
                                {
                                    if (f.Ifchecked)
                                    {
                                        ifcheck = f.Ifchecked;
                                        if (rmList.Where(p => p.MENU_CODE.Equals(f.code)).Count() == 0)
                                            rmList.Add(new M_ROLE_MENUS() { MENU_CODE = f.code, ROLE_CODE = menuifo.roleInfo.ROLE_CODE });
                                    }
                                }
                            }
                            if (ifcheck)
                                if (rmList.Where(p => p.MENU_CODE.Equals(m.code)).Count() == 0)
                                    rmList.Add(new M_ROLE_MENUS() { MENU_CODE = m.code, ROLE_CODE = menuifo.roleInfo.ROLE_CODE });
                        }
                    }
                    if (ifcheck)
                        if (rmList.Where(p => p.MENU_CODE.Equals(s.code)).Count() == 0)
                            rmList.Add(new M_ROLE_MENUS() { MENU_CODE = s.code, ROLE_CODE = menuifo.roleInfo.ROLE_CODE });
                }
            }
            var resualt = menuifo.roleInfo.SaveRoleMenus(rmList, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysRoles/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }
    }

    /// <summary>
    /// 角色菜单信息
    /// </summary>
    public class RoleMenusInfo
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public SYS_ROLE_INFOModel roleInfo { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<TreeModel> MenuInfo { get; set; }
    }
}