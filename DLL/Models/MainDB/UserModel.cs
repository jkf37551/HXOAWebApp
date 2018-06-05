using DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models.MainDB
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class UserModel
    {
        #region 属性
        /// <summary>
        /// 账号
        /// </summary>
        [Required(AllowEmptyStrings =false,ErrorMessage ="请填写账号！")]
        [Display(Name = "账号")]
        public string USER_USERID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings =false,ErrorMessage ="请填写密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string USER_PASSWORD { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string USER_NAME { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [Display(Name = "状态")]
        public string CODE_FOR_USERSTATUS { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string CODE_FOR_SEX { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 根据用户获取菜单信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public static ResultInfo<List<M_MENU_INFO>> GetUserMenusByUserId(UserModel user)
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
                                where a.USERID.Equals(user.USER_USERID) && a.ROLE_CODE.Equals(b.ROLE_CODE) && b.MENU_CODE.Equals(c.MENU_CODE)
                                select c;
                    MenuList = items.Distinct().ToList();
                }
                resualt.IsSuccess = true;
                resualt.Data = MenuList;
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
        public ResultInfo<List<M_ROLES>> GetUserRolesByUserId(UserModel user)
        {
            ResultInfo<List<M_ROLES>> Resualt = new ResultInfo<List<M_ROLES>>();
            try
            {
                List<M_ROLES> RoleList = new List<M_ROLES>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_USER_ROLES
                                from b in DB.M_ROLES
                                where a.USERID.Equals(user.USER_USERID) && a.ROLE_CODE.Equals(b.ROLE_CODE)
                                select b;
                    RoleList = items.Distinct().ToList();
                }
                Resualt.Data = RoleList;
                Resualt.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Resualt.IsSuccess = false;
                Resualt.Message = ex.Message;
            }
            return Resualt;
        }

        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="roleCodeList">角色代码列表</param>
        /// <param name="doUser">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> SetUserRoles(UserModel user, List<string> roleCodeList, UserModel doUser)
        {
            ResultInfo<bool> resualt = new ResultInfo<bool>();
            try
            {
                List<M_USER_ROLES> RMList = new List<M_USER_ROLES>();
                for (int i = 0; i < roleCodeList.Count; i++)
                {
                    RMList.AddRange(roleCodeList.Select(p => new M_USER_ROLES()
                    {
                        USERID = user.USER_USERID,
                        ROLE_CODE = p,
                        UR_CREATEUSER = doUser.USER_USERID,
                        UR_CREATE_DATE = DateTime.Now,
                        UR_LASTUPDATE = DateTime.Now,
                        UR_LASTUPDATEUSER = doUser.USER_USERID
                    }));
                }
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    List<M_USER_ROLES> DeleteItems = DB.M_USER_ROLES.Where(p => p.USERID.Equals(user.USER_USERID)).ToList();
                    //删除以前的角色信息
                    if (DeleteItems != null && DeleteItems.Count > 0)
                        DB.M_USER_ROLES.DeleteAllOnSubmit(DeleteItems);
                    //保存最新的角色信息
                    if (RMList != null && RMList.Count > 0)
                        DB.M_USER_ROLES.InsertAllOnSubmit(RMList);
                    DB.SubmitChanges();
                }
                resualt.IsSuccess = true;
            }
            catch (Exception ex)
            {
                resualt.IsSuccess = false;
                resualt.Message = ex.Message;
            }
            return resualt;
        }
        #endregion
    }
}
