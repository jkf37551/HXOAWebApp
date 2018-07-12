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
    /// 应用模型
    /// </summary>
    public class T_APP_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required]
        [Display(Name = "应用名称")]
        public string APP_DESC { get; set; }

        /// <summary>
        /// 应用IP
        /// </summary>
        [Display(Name = "应用IP")]
        public string APP_IP { get; set; }

        /// <summary>
        /// 应用内网地址
        /// </summary>
        [Display(Name = "应用内网地址")]
        public string APP_IN_URL { get; set; }

        /// <summary>
        /// 应用外网地址
        /// </summary>
        [Display(Name = "应用外网地址")]
        public string APP_OUT_URL { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<T_APP_INFOModel>> GetList(T_APP_INFOModel model)
        {
            ResultInfo<List<T_APP_INFOModel>> Resualt = new ResultInfo<List<T_APP_INFOModel>>();
            try
            {
                List<T_APP_INFOModel> ItemList = new List<T_APP_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_APP_INFO
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.APP_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.APP_IP))
                        items = items.Where(p => p.APP_IP.Contains(model.APP_IP));
                    if (!string.IsNullOrEmpty(model.APP_DESC))
                        items = items.Where(p => p.APP_DESC.Contains(model.APP_DESC));
                    if (!string.IsNullOrEmpty(model.APP_IN_URL))
                        items = items.Where(p => p.APP_IN_URL.Contains(model.APP_IN_URL));
                    if (!string.IsNullOrEmpty(model.APP_OUT_URL))
                        items = items.Where(p => p.APP_OUT_URL.Contains(model.APP_OUT_URL));
                    ItemList = items.Select(p => new T_APP_INFOModel()
                    {
                        ID = p.APP_ID,
                        APP_IP = p.APP_IP,
                        APP_DESC = p.APP_DESC,
                        APP_IN_URL = p.APP_IN_URL,
                        APP_OUT_URL = p.APP_OUT_URL
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
        public ResultInfo<T_APP_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<T_APP_INFOModel> Resualt = new ResultInfo<T_APP_INFOModel>();
            try
            {
                T_APP_INFOModel item = new T_APP_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_APP_INFO
                                where a.APP_ID == ID
                                select a;
                    item = items.Select(p => new T_APP_INFOModel()
                    {
                        ID = p.APP_ID,
                        APP_DESC = p.APP_DESC,
                        APP_IP = p.APP_IP,
                        APP_IN_URL = p.APP_IN_URL,
                        APP_OUT_URL = p.APP_OUT_URL
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
        public ResultInfo<bool> Insert(T_APP_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                T_APP_INFO item = new T_APP_INFO()
                {
                    APP_IP = model.APP_IP,
                    APP_DESC = model.APP_DESC,
                    APP_IN_URL = model.APP_IN_URL,
                    APP_OUT_URL = model.APP_OUT_URL,
                    CREATE_DATE = DateTime.Now,
                    LASTUPDATE = DateTime.Now,
                    LASTUPDATEUSER = user.USER_USERID,
                    CREATEUSER = user.USER_USERID
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.T_APP_INFO.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(T_APP_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.T_APP_INFO.Where(p => p.APP_ID.Equals(model.ID)).FirstOrDefault();
                    v.APP_IP = model.APP_IP;
                    v.APP_DESC = model.APP_DESC;
                    v.APP_IN_URL = model.APP_IN_URL;
                    v.APP_OUT_URL = model.APP_OUT_URL;
                    v.LASTUPDATE = DateTime.Now;
                    v.LASTUPDATEUSER = user.USER_USERID;
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
                    var v = DB.T_APP_INFO.Where(p => p.APP_ID.Equals(ID)).FirstOrDefault();
                    DB.T_APP_INFO.DeleteOnSubmit(v);
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
