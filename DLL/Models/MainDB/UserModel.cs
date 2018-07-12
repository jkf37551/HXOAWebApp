using DB;
using DLL.Models.HRSDB;
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
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public long ID { get; set; }

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
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<UserModel>> GetList(UserModel model)
        {
            ResultInfo<List<UserModel>> Resualt = new ResultInfo<List<UserModel>>();
            try
            {
                List<UserModel> ItemList = new List<UserModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_USERS
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.UID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.USER_USERID))
                        items = items.Where(p => p.USER_USERID.Contains(model.USER_USERID));
                    if (!string.IsNullOrEmpty(model.USER_NAME))
                        items = items.Where(p => p.USER_NAME.Contains(model.USER_NAME));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_USERSTATUS))
                        items = items.Where(p => p.CODE_FOR_USERSTATUS.Contains(model.CODE_FOR_USERSTATUS));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_SEX))
                        items = items.Where(p => p.CODE_FOR_SEX.Contains(model.CODE_FOR_SEX));
                    ItemList = items.Select(p => new UserModel()
                    {
                        ID = p.UID,
                        CODE_FOR_SEX = p.CODE_FOR_SEX,
                        CODE_FOR_USERSTATUS = p.CODE_FOR_USERSTATUS,
                        USER_NAME = p.USER_NAME,
                        USER_PASSWORD = p.USER_PASSWORD,
                        USER_USERID=p.USER_USERID
                    }).ToList();
                }
                Resualt.Data = ItemList;
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
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public ResultInfo<UserModel> GetEnetityByID(long ID)
        {
            ResultInfo<UserModel> Resualt = new ResultInfo<UserModel>();
            try
            {
                UserModel item = new UserModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_USERS
                                where a.UID == ID
                                select a;
                    item = items.Select(p => new UserModel()
                    {
                        ID = p.UID,
                        CODE_FOR_SEX = p.CODE_FOR_SEX,
                        CODE_FOR_USERSTATUS = p.CODE_FOR_USERSTATUS,
                        USER_NAME = p.USER_NAME,
                        USER_USERID = p.USER_USERID,
                        USER_PASSWORD=p.USER_PASSWORD
                    }).FirstOrDefault();
                }
                Resualt.Data = item;
                if (item != null)
                    Resualt.IsSuccess = true;
                else
                {
                    Resualt.IsSuccess = false;
                    Resualt.Message = "未找到相应的对象";
                }
            }
            catch (Exception ex)
            {
                Resualt.IsSuccess = false;
                Resualt.Message = ex.Message;
            }
            return Resualt;
        }
        /// <summary>
        /// 新增代码类型
        /// </summary>
        /// <param name="model">新增的信息</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> Insert(UserModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                M_USERS item = new HR_UserModel().GetList(new HR_UserModel() { UserId = model.USER_USERID }, 1, 1).Data.Select(p => new M_USERS()
                {
                    CODE_FOR_SEX = p.STAT2,
                    CODE_FOR_USERSTATUS = "1",
                    USER_NAME = p.UserName,
                    USER_PASSWORD = p.Password,
                    USER_USERID = p.UserId,
                    USER_CREATE_DATE = DateTime.Now,
                    USER_LASTUPDATE = DateTime.Now,
                    USER_CREATEUSER = user.USER_USERID,
                    USER_LASTUPDATEUSER = user.USER_USERID
                }).FirstOrDefault();
                if (item == null)
                    throw new Exception("绩效平台中没有此工号信息！");
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.M_USERS.InsertOnSubmit(item);
                    DB.SubmitChanges();
                }
                Resualt.Data = true;
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
        /// 编辑代码类型
        /// </summary>
        /// <param name="model">更新数据</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> Update(UserModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_USERS.Where(p => p.UID.Equals(model.ID)).FirstOrDefault();
                    v.CODE_FOR_SEX = model.CODE_FOR_SEX;
                    v.CODE_FOR_USERSTATUS = model.CODE_FOR_USERSTATUS;
                    v.USER_NAME = model.USER_NAME;
                    v.USER_PASSWORD = model.USER_PASSWORD;
                    v.USER_USERID = model.USER_USERID;
                    v.USER_LASTUPDATE = DateTime.Now;
                    v.USER_LASTUPDATEUSER = user.USER_USERID;
                    DB.SubmitChanges();
                }
                Resualt.Data = true;
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
        /// 删除代码类型
        /// </summary>
        /// <param name="model">要删除的数据</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> Delete(long ID, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    //删除用户
                    var v = DB.M_USERS.Where(p => p.UID.Equals(ID)).FirstOrDefault();
                    DB.M_USERS.DeleteOnSubmit(v);
                    //删除用户角色
                    var UrList = DB.M_USER_ROLES.Where(p => p.USERID.Equals(v.USER_USERID)).ToList();
                    if (UrList != null && UrList.Count > 0)
                        DB.M_USER_ROLES.DeleteAllOnSubmit(UrList);
                    DB.SubmitChanges();
                }
                Resualt.Data = true;
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
        /// 保存角色菜单
        /// </summary>
        /// <param name="rolemenus">角色菜单信息</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> SaveUserRoles(List<M_USER_ROLES> Userroles, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                List<M_USER_ROLES> items = Userroles.Select(p => new M_USER_ROLES()
                {
                    ROLE_CODE = p.ROLE_CODE,
                    USERID = p.USERID,
                    UR_CREATEUSER = user.USER_USERID,
                    UR_CREATE_DATE = DateTime.Now,
                    UR_LASTUPDATE = DateTime.Now,
                    UR_LASTUPDATEUSER = user.USER_USERID
                }).ToList();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    //删除之前的记录
                    var dlist = DB.M_USER_ROLES.Where(p => p.USERID.Equals(Userroles[0].USERID)).ToList();
                    DB.M_USER_ROLES.DeleteAllOnSubmit(dlist);
                    DB.M_USER_ROLES.InsertAllOnSubmit(items);
                    DB.SubmitChanges();
                }
                Resualt.Data = true;
                Resualt.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Resualt.IsSuccess = false;
                Resualt.Message = ex.Message;
            }
            return Resualt;
        }
        #endregion
    }
}
