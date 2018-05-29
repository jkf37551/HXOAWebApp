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
    /// 菜单模型
    /// </summary>
    public class SYS_MENU_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 菜单代码
        /// </summary>
        [Required]
        [Display(Name = "菜单代码")]
        public string MENU_CODE { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        [Display(Name = "菜单名称")]
        public string MENU_NAME { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        [Required]
        [Display(Name = "菜单级别")]
        public string CODE_FOR_MENU_LEVEL { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        [Required]
        [Display(Name = "菜单状态")]
        public string CODE_FOR_STATUS { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [Display(Name = "上级菜单")]
        public string MENU_UPMENU { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [Display(Name = "菜单地址")]
        public string MENU_URL { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<SYS_MENU_INFOModel>> GetList(SYS_MENU_INFOModel model)
        {
            ResultInfo<List<SYS_MENU_INFOModel>> Resualt = new ResultInfo<List<SYS_MENU_INFOModel>>();
            try
            {
                List<SYS_MENU_INFOModel> ItemList = new List<SYS_MENU_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_MENU_INFO
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.MENU_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.MENU_CODE))
                        items = items.Where(p => p.MENU_CODE.Contains(model.MENU_CODE));
                    if (!string.IsNullOrEmpty(model.MENU_NAME))
                        items = items.Where(p => p.MENU_NAME.Contains(model.MENU_NAME));
                    if (!string.IsNullOrEmpty(model.MENU_UPMENU))
                        items = items.Where(p => p.MENU_UPMENU.Equals(model.MENU_UPMENU));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_STATUS))
                        items = items.Where(p => p.CODE_FOR_STATUS.Equals(model.CODE_FOR_STATUS));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_MENU_LEVEL))
                        items = items.Where(p => p.CODE_FOR_MENU_LEVEL.Equals(model.CODE_FOR_MENU_LEVEL));
                    if (!string.IsNullOrEmpty(model.MENU_URL))
                        items = items.Where(p => p.MENU_URL.Contains(model.MENU_URL));
                    ItemList = items.Select(p => new SYS_MENU_INFOModel()
                    {
                        ID = p.MENU_ID,
                        MENU_CODE = p.MENU_CODE,
                        MENU_NAME = p.MENU_NAME,
                        CODE_FOR_MENU_LEVEL = p.CODE_FOR_MENU_LEVEL,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        MENU_UPMENU = p.MENU_UPMENU,
                        MENU_URL = p.MENU_URL
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
        public ResultInfo<SYS_MENU_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<SYS_MENU_INFOModel> Resualt = new ResultInfo<SYS_MENU_INFOModel>();
            try
            {
                SYS_MENU_INFOModel item = new SYS_MENU_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_MENU_INFO
                                where a.MENU_ID == ID
                                select a;
                    item = items.Select(p => new SYS_MENU_INFOModel()
                    {
                        ID = p.MENU_ID,
                        MENU_CODE = p.MENU_CODE,
                        MENU_NAME = p.MENU_NAME,
                        MENU_URL = p.MENU_URL,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        CODE_FOR_MENU_LEVEL = p.CODE_FOR_MENU_LEVEL,
                        MENU_UPMENU = p.MENU_UPMENU
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
        public ResultInfo<bool> Insert(SYS_MENU_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                M_MENU_INFO item = new M_MENU_INFO()
                {
                    CODE_FOR_MENU_LEVEL = model.CODE_FOR_MENU_LEVEL,
                    MENU_CODE = model.MENU_CODE,
                    CODE_FOR_STATUS = model.CODE_FOR_STATUS,
                    MENU_CREATEUSER = user.USER_USERID,
                    MENU_CREATE_DATE = DateTime.Now,
                    MENU_LASTUPDATE = DateTime.Now,
                    MENU_LASTUPDATEUSER = user.USER_USERID,
                    MENU_NAME = model.MENU_NAME,
                    MENU_UPMENU = model.MENU_UPMENU,
                    MENU_URL = model.MENU_URL
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.M_MENU_INFO.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(SYS_MENU_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_MENU_INFO.Where(p => p.MENU_ID.Equals(model.ID)).FirstOrDefault();
                    v.CODE_FOR_MENU_LEVEL = model.CODE_FOR_MENU_LEVEL;
                    v.CODE_FOR_STATUS = model.CODE_FOR_STATUS;
                    v.MENU_CODE = model.MENU_CODE;
                    v.MENU_LASTUPDATE = DateTime.Now;
                    v.MENU_LASTUPDATEUSER = user.USER_USERID;
                    v.MENU_NAME = model.MENU_NAME;
                    v.MENU_UPMENU = model.MENU_UPMENU;
                    v.MENU_URL = model.MENU_URL;
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
                    var v = DB.M_MENU_INFO.Where(p => p.MENU_ID.Equals(ID)).FirstOrDefault();
                    DB.M_MENU_INFO.DeleteOnSubmit(v);
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
        /// 根据菜单级别获取上级菜单
        /// </summary>
        /// <param name="menuLevel">菜单级别</param>
        /// <returns></returns>
        public static List<SYS_MENU_INFOModel> GetUpMenuListByLevel(string menuLevel)
        {
            List<SYS_MENU_INFOModel> resualt = new List<SYS_MENU_INFOModel>();
            try
            {
                string upmenulevel = "";
                if (menuLevel.Length > 1)
                    upmenulevel = menuLevel.Substring(0, menuLevel.Length - 1) + (Convert.ToInt32(menuLevel.Substring(menuLevel.Length - 1)) - 1).ToString();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    resualt = DB.M_MENU_INFO.Where(p => p.CODE_FOR_MENU_LEVEL.ToLower().Trim().Equals(upmenulevel.ToLower().Trim()) && p.CODE_FOR_STATUS.Equals("1")).Select(p => new SYS_MENU_INFOModel()
                    {
                        ID = p.MENU_ID,
                        CODE_FOR_MENU_LEVEL = p.CODE_FOR_MENU_LEVEL,
                        MENU_CODE = p.MENU_CODE,
                        MENU_NAME = p.MENU_NAME,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        MENU_UPMENU = p.MENU_UPMENU,
                        MENU_URL = p.MENU_URL
                    }).ToList();
                }
            }
            catch
            { }
            return resualt;
        }

        /// <summary>
        /// 根据菜单代码获取菜单名称
        /// </summary>
        /// <param name="menucode">菜单代码</param>
        /// <returns></returns>
        public static string GetMenuNameByCode(string menucode)
        {
            string Name = "";
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_MENU_INFO.Where(p => p.MENU_CODE.ToLower().Equals(menucode.Trim().ToLower())).FirstOrDefault();
                    if (v != null)
                        Name = v.MENU_NAME;
                }
            }
            catch
            { }
            return Name;
        }
        #endregion
    }
}
