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
    public class SysUserController : BaseController<UserModel>
    {
        // GET: SysUser
        public ActionResult List()
        {
            return View(_pageInfo);
        }

        public ActionResult Query(PageModel<UserModel> Model)
        {
            _pageInfo = Model;
            var result = Model.QueryModel.GetList(Model.QueryModel);
            if (result.IsSuccess)
            {
                _pageInfo.QueryData = result.Data;
                return View("List", _pageInfo);
            }
            else
            {
                return SysErro(result.Message);
            }
        }

        public ActionResult Create()
        {
            return View(new UserModel() { USER_PASSWORD="111111"});
        }

        [HttpPost]
        public ActionResult Create(UserModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var resualt = Model.Insert(Model, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysUser/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }
        
        public ActionResult Delete(long id)
        {
            var resualt = new UserModel().Delete(id, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysUser/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }
        
        public ActionResult UserRoles(long id)
        {
            UserRolesInfo roleInfo = new UserRolesInfo();
            //获取用户信息
            var u_role = _pageInfo.QueryModel.GetEnetityByID(id);
            if (u_role.IsSuccess)
                roleInfo.userInfo = u_role.Data;
            //角色信息
            List<TreeModel> treeItem = new List<TreeModel>();
            //获取所有角色信息
            var AllRoles = new SYS_ROLE_INFOModel().GetList(new SYS_ROLE_INFOModel());
            if (AllRoles.IsSuccess)
            {
                //获取用户角色
                var userroles = _pageInfo.QueryModel.GetUserRolesByUserId(u_role.Data);
                List<string> Rolelist = new List<string>();
                if (userroles.IsSuccess)
                    Rolelist = userroles.Data.Select(p => p.ROLE_CODE).ToList();
                treeItem = AllRoles.Data.Where(p => p.CODE_FOR_STATUS.Equals("1"))
                .Select(v => new TreeModel()
                {
                    id = v.ID.ToString(),
                    code = v.ROLE_CODE,
                    text = v.ROLE_NAME,
                    Ifchecked = Rolelist.Contains(v.ROLE_CODE)
                }).ToList();
            }
            roleInfo.RoleInfo = treeItem;
            return View(roleInfo);
        }

        [HttpPost]
        public ActionResult UserRoles(UserRolesInfo menuifo)
        {
            List<M_USER_ROLES> rmList = new List<M_USER_ROLES>();
            //获取用户角色
            bool ifcheck = false;
            if (menuifo != null && menuifo.RoleInfo != null)
            {
                foreach (var s in menuifo.RoleInfo)
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
                                        if (rmList.Where(p => p.ROLE_CODE.Equals(f.code)).Count() == 0)
                                            rmList.Add(new M_USER_ROLES() { ROLE_CODE = f.code, USERID = menuifo.userInfo.USER_USERID });
                                    }
                                }
                            }
                            if (ifcheck)
                                if (rmList.Where(p => p.ROLE_CODE.Equals(m.code)).Count() == 0)
                                    rmList.Add(new M_USER_ROLES() { ROLE_CODE = m.code, USERID = menuifo.userInfo.USER_USERID });
                        }
                    }
                    if (ifcheck)
                        if (rmList.Where(p => p.ROLE_CODE.Equals(s.code)).Count() == 0)
                            rmList.Add(new M_USER_ROLES() { ROLE_CODE = s.code, USERID = menuifo.userInfo.USER_USERID });
                }
            }
            var resualt = menuifo.userInfo.SaveUserRoles(rmList, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysUser/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }
    }

    /// <summary>
    /// 用户角色信息
    /// </summary>
    public class UserRolesInfo
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public UserModel userInfo { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<TreeModel> RoleInfo { get; set; }
    }
}