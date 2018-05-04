using DB;
using DLL.Helpers;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.MAINBussiness
{
    public class UserDAL
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Userid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static ResultInfo<Users> LoginValidate(Users User)
        {
            ResultInfo<Users> resualt = new ResultInfo<Users>();
            try
            {
                Users v = null;
                if (User == null)
                    throw new Exception("缺少用户信息！");
                if (string.IsNullOrEmpty(User.UserId))
                    throw new Exception("缺少用户账号！");
                if (string.IsNullOrEmpty(User.Password))
                    throw new Exception("缺少密码！");
                using (HXOADBDataContext UserDB = new HXOADBDataContext())
                {
                    v = UserDB.Users.Where(p => p.UserId.Equals(User.UserId)).FirstOrDefault();
                }
                if (v == null)
                    throw new Exception("用户不存在！");
                if (String.IsNullOrEmpty(v.Password))
                    throw new Exception("用户未授权,请先初始化用户信息！");

                if (!v.Password.ToLower().Equals(MySecurity.MD5Encrypt(User.Password)))
                    throw new Exception("账号或密码错误！");

                resualt.IsSuccess = true;
                resualt.Data = v;
            }
            catch (Exception ex)
            {
                resualt.IsSuccess = false;
                resualt.Message = ex.Message;
            }
            return resualt;
        }

        /// <summary>
        /// 根据用户获取用户角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public List<M_ROLES> GetUserRolesByUserId(Users user)
        {
            try
            {
                List<M_ROLES> Resualt = new List<M_ROLES>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_USER_ROLES
                                from b in DB.M_ROLES
                                where a.USERID.Equals(user.UserId) && a.ROLE_CODE.Equals(b.ROLE_CODE)
                                select b;
                    Resualt = items.Distinct().ToList();
                }
                return Resualt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="roleCodeList">角色代码列表</param>
        /// <param name="doUser">操作人</param>
        /// <returns></returns>
        public bool SetUserRoles(Users user, List<string> roleCodeList, Users doUser)
        {
            try
            {
                List<M_USER_ROLES> RMList = new List<M_USER_ROLES>();
                for (int i = 0; i < roleCodeList.Count; i++)
                {
                    RMList.AddRange(roleCodeList.Select(p => new M_USER_ROLES()
                    {
                        USERID = user.UserId,
                        ROLE_CODE = p,
                        UR_CREATEUSER = doUser.UserId,
                        UR_CREATE_DATE = DateTime.Now,
                        UR_LASTUPDATE = DateTime.Now,
                        UR_LASTUPDATEUSER = doUser.UserId
                    }));
                }
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    List<M_USER_ROLES> DeleteItems = DB.M_USER_ROLES.Where(p => p.USERID.Equals(user.UserId)).ToList();
                    //删除以前的角色信息
                    if (DeleteItems != null && DeleteItems.Count > 0)
                        DB.M_USER_ROLES.DeleteAllOnSubmit(DeleteItems);
                    //保存最新的角色信息
                    if (RMList != null && RMList.Count > 0)
                        DB.M_USER_ROLES.InsertAllOnSubmit(RMList);
                    DB.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据用户获取菜单信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public static ResultInfo<List<M_MENU_INFO>> GetUserMenusByUserId(Users user)
        {
            ResultInfo<List<M_MENU_INFO>> resualt = new ResultInfo<List<M_MENU_INFO>>();
            try
            {
                List<M_MENU_INFO> MenuList = new List<M_MENU_INFO>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_USER_ROLES
                                from b in DB.M_ROLE_MENUS
                                from c in DB.M_MENU_INFO
                                where a.USERID.Equals(user.UserId) && a.ROLE_CODE.Equals(b.ROLE_CODE) && b.MENU_CODE.Equals(c.MENU_CODE)
                                select c;
                    MenuList = items.Distinct().ToList();
                }
                resualt.IsSuccess = true;
                resualt.Data=MenuList;
            }
            catch (Exception ex)
            {
                resualt.IsSuccess = false;
                resualt.Message = ex.Message;
            }
            return resualt;
        }
    }
}
