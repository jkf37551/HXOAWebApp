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
    /// 角色模型
    /// </summary>
    public class SYS_ROLE_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        [Required]
        [Display(Name = "角色代码")]
        public string ROLE_CODE { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [Display(Name = "角色名称")]
        public string ROLE_NAME { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [Display(Name = "状态")]
        public string CODE_FOR_STATUS { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<SYS_ROLE_INFOModel>> GetList(SYS_ROLE_INFOModel model)
        {
            ResultInfo<List<SYS_ROLE_INFOModel>> Resualt = new ResultInfo<List<SYS_ROLE_INFOModel>>();
            try
            {
                List<SYS_ROLE_INFOModel> ItemList = new List<SYS_ROLE_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_ROLES
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.ROLE_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.ROLE_CODE))
                        items = items.Where(p => p.ROLE_CODE.Contains(model.ROLE_CODE));
                    if (!string.IsNullOrEmpty(model.ROLE_NAME))
                        items = items.Where(p => p.ROLE_NAME.Contains(model.ROLE_NAME));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_STATUS))
                        items = items.Where(p => p.CODE_FOR_STATUS.Equals(model.CODE_FOR_STATUS));
                    ItemList = items.Select(p => new SYS_ROLE_INFOModel()
                    {
                        ID = p.ROLE_ID,
                        ROLE_CODE = p.ROLE_CODE,
                        ROLE_NAME = p.ROLE_NAME,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS
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
        public ResultInfo<SYS_ROLE_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<SYS_ROLE_INFOModel> Resualt = new ResultInfo<SYS_ROLE_INFOModel>();
            try
            {
                SYS_ROLE_INFOModel item = new SYS_ROLE_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_ROLES
                                where a.ROLE_ID == ID
                                select a;
                    item = items.Select(p => new SYS_ROLE_INFOModel()
                    {
                        ID = p.ROLE_ID,
                        ROLE_CODE = p.ROLE_CODE,
                        ROLE_NAME = p.ROLE_NAME,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS
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
        public ResultInfo<bool> Insert(SYS_ROLE_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                M_ROLES item = new M_ROLES()
                {
                     ROLE_CODE = model.ROLE_CODE,
                     ROLE_NAME = model.ROLE_NAME,
                     ROLE_CREATEUSER = user.USER_USERID,
                     ROLE_CREATE_DATE = DateTime.Now,
                     ROLE_LASTUPDATE = DateTime.Now,
                     ROLE_LASTUPDATEUSER = user.USER_USERID,
                     CODE_FOR_STATUS = model.CODE_FOR_STATUS
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.M_ROLES.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(SYS_ROLE_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_ROLES.Where(p => p.ROLE_ID.Equals(model.ID)).FirstOrDefault();
                    v.CODE_FOR_STATUS = model.CODE_FOR_STATUS;
                    v.ROLE_CODE = model.ROLE_CODE;
                    v.ROLE_LASTUPDATE = DateTime.Now;
                    v.ROLE_LASTUPDATEUSER = user.USER_USERID;
                    v.ROLE_NAME = model.ROLE_NAME;
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
                    var v = DB.M_ROLES.Where(p => p.ROLE_ID.Equals(ID)).FirstOrDefault();
                    DB.M_ROLES.DeleteOnSubmit(v);
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
        /// 根据角色代码获取角色菜单
        /// </summary>
        /// <param name="rolecode">角色代码</param>
        /// <returns></returns>
        public ResultInfo<List<M_ROLE_MENUS>> GetRoleMenus(long roleid)
        {
            ResultInfo<List<M_ROLE_MENUS>> Resualt = new ResultInfo<List<M_ROLE_MENUS>>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_ROLES
                                from b in DB.M_ROLE_MENUS
                                where a.ROLE_ID.Equals(roleid) && a.ROLE_CODE.Equals(b.ROLE_CODE)
                                && a.CODE_FOR_STATUS.Equals("1")
                                select b;
                    Resualt.Data = items.Distinct().ToList();
                }
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
        public ResultInfo<bool> SaveRoleMenus(List<M_ROLE_MENUS> rolemenus, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                List<M_ROLE_MENUS> items = rolemenus.Select(p => new M_ROLE_MENUS()
                {
                    ROLE_CODE = p.ROLE_CODE,
                    MENU_CODE = p.MENU_CODE,
                    RM_CREATEUSER = user.USER_USERID,
                    RM_CREATE_DATE = DateTime.Now,
                    RM_LASTUPDATE = DateTime.Now,
                    RM_LASTUPDATEUSER = user.USER_USERID
                }).ToList();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    //删除之前的记录
                    var dlist = DB.M_ROLE_MENUS.Where(p => p.ROLE_CODE.Equals(rolemenus[0].ROLE_CODE)).ToList();
                    DB.M_ROLE_MENUS.DeleteAllOnSubmit(dlist);
                    DB.M_ROLE_MENUS.InsertAllOnSubmit(items);
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
