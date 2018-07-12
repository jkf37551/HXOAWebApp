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
    /// 服务器模板
    /// </summary>
    public class T_SERVER_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        [Required]
        [Display(Name = "服务器地址")]
        public string SERVER_IP { get; set; }

        /// <summary>
        /// 应用信息
        /// </summary>
        [Required]
        [Display(Name = "应用信息")]
        public string SERVER_DESC { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string SERVER_LOGIN_USER { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        public string SERVER_LOGIN_PASSWORD { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<T_SERVER_INFOModel>> GetList(T_SERVER_INFOModel model)
        {
            ResultInfo<List<T_SERVER_INFOModel>> Resualt = new ResultInfo<List<T_SERVER_INFOModel>>();
            try
            {
                List<T_SERVER_INFOModel> ItemList = new List<T_SERVER_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_SERVER_INFO
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.SERVER_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.SERVER_IP))
                        items = items.Where(p => p.SERVER_IP.Contains(model.SERVER_IP));
                    if (!string.IsNullOrEmpty(model.SERVER_DESC))
                        items = items.Where(p => p.SERVER_DESC.Contains(model.SERVER_DESC));
                    if (!string.IsNullOrEmpty(model.SERVER_LOGIN_USER))
                        items = items.Where(p => p.SERVER_LOGIN_USER.Contains(model.SERVER_LOGIN_USER));
                    if (!string.IsNullOrEmpty(model.SERVER_LOGIN_PASSWORD))
                        items = items.Where(p => p.SERVER_LOGIN_PASSWORD.Contains(model.SERVER_LOGIN_PASSWORD));
                    ItemList = items.Select(p => new T_SERVER_INFOModel()
                    {
                        ID = p.SERVER_ID,
                        SERVER_IP = p.SERVER_IP,
                        SERVER_DESC = p.SERVER_DESC,
                        SERVER_LOGIN_USER = p.SERVER_LOGIN_USER,
                        SERVER_LOGIN_PASSWORD = p.SERVER_LOGIN_PASSWORD
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
        public ResultInfo<T_SERVER_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<T_SERVER_INFOModel> Resualt = new ResultInfo<T_SERVER_INFOModel>();
            try
            {
                T_SERVER_INFOModel item = new T_SERVER_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_SERVER_INFO
                                where a.SERVER_ID == ID
                                select a;
                    item = items.Select(p => new T_SERVER_INFOModel()
                    {
                        ID = p.SERVER_ID,
                        SERVER_IP = p.SERVER_IP,
                        SERVER_DESC = p.SERVER_DESC,
                        SERVER_LOGIN_USER = p.SERVER_LOGIN_USER,
                        SERVER_LOGIN_PASSWORD = p.SERVER_LOGIN_PASSWORD
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
        public ResultInfo<bool> Insert(T_SERVER_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                T_SERVER_INFO item = new T_SERVER_INFO()
                {
                     SERVER_IP = model.SERVER_IP,
                     SERVER_DESC = model.SERVER_DESC,
                     SERVER_LOGIN_USER = model.SERVER_LOGIN_USER,
                     SERVER_LOGIN_PASSWORD = model.SERVER_LOGIN_PASSWORD,
                    CREATE_DATE = DateTime.Now,
                    LASTUPDATE = DateTime.Now,
                    LASTUPDATEUSER = user.USER_USERID,
                    CREATEUSER = user.USER_USERID
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.T_SERVER_INFO.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(T_SERVER_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.T_SERVER_INFO.Where(p => p.SERVER_ID.Equals(model.ID)).FirstOrDefault();
                    v.SERVER_IP = model.SERVER_IP;
                    v.SERVER_DESC = model.SERVER_DESC;
                    v.SERVER_LOGIN_USER = model.SERVER_LOGIN_USER;
                    v.SERVER_LOGIN_PASSWORD = model.SERVER_LOGIN_PASSWORD;
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
                    var v = DB.T_SERVER_INFO.Where(p => p.SERVER_ID.Equals(ID)).FirstOrDefault();
                    DB.T_SERVER_INFO.DeleteOnSubmit(v);
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
