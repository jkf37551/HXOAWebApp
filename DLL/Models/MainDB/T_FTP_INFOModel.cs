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
    /// FTP信息
    /// </summary>
    public class T_FTP_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// FTP地址
        /// </summary>
        [Required]
        [Display(Name = "FTP地址")]
        public string FTP_ADDRESS { get; set; }

        /// <summary>
        /// 用途描述
        /// </summary>
        [Required]
        [Display(Name = "用途描述")]
        public string FTP_DESC { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string FTP_LOGIN_USER { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        public string FTP_LOGIN_PASSWORD { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<T_FTP_INFOModel>> GetList(T_FTP_INFOModel model)
        {
            ResultInfo<List<T_FTP_INFOModel>> Resualt = new ResultInfo<List<T_FTP_INFOModel>>();
            try
            {
                List<T_FTP_INFOModel> ItemList = new List<T_FTP_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_FTP_INFO
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.FTP_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.FTP_ADDRESS))
                        items = items.Where(p => p.FTP_ADDRESS.Contains(model.FTP_ADDRESS));
                    if (!string.IsNullOrEmpty(model.FTP_DESC))
                        items = items.Where(p => p.FTP_DESC.Contains(model.FTP_DESC));
                    if (!string.IsNullOrEmpty(model.FTP_LOGIN_USER))
                        items = items.Where(p => p.FTP_LOGIN_USER.Contains(model.FTP_LOGIN_USER));
                    if (!string.IsNullOrEmpty(model.FTP_LOGIN_PASSWORD))
                        items = items.Where(p => p.FTP_LOGIN_PASSWORD.Contains(model.FTP_LOGIN_PASSWORD));
                    ItemList = items.Select(p => new T_FTP_INFOModel()
                    {
                        ID = p.FTP_ID,
                        FTP_ADDRESS = p.FTP_ADDRESS,
                        FTP_DESC = p.FTP_DESC,
                        FTP_LOGIN_USER = p.FTP_LOGIN_USER,
                        FTP_LOGIN_PASSWORD = p.FTP_LOGIN_PASSWORD
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
        public ResultInfo<T_FTP_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<T_FTP_INFOModel> Resualt = new ResultInfo<T_FTP_INFOModel>();
            try
            {
                T_FTP_INFOModel item = new T_FTP_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.T_FTP_INFO
                                where a.FTP_ID == ID
                                select a;
                    item = items.Select(p => new T_FTP_INFOModel()
                    {
                        ID = p.FTP_ID,
                         FTP_ADDRESS = p.FTP_ADDRESS,
                         FTP_DESC = p.FTP_DESC,
                         FTP_LOGIN_USER = p.FTP_LOGIN_USER,
                         FTP_LOGIN_PASSWORD = p.FTP_LOGIN_PASSWORD
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
        public ResultInfo<bool> Insert(T_FTP_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                T_FTP_INFO item = new T_FTP_INFO()
                {
                     FTP_ADDRESS = model.FTP_ADDRESS,
                    FTP_DESC = model.FTP_DESC,
                    FTP_LOGIN_USER = model.FTP_LOGIN_USER,
                    FTP_LOGIN_PASSWORD = model.FTP_LOGIN_PASSWORD,
                    CREATE_DATE = DateTime.Now,
                    LASTUPDATE = DateTime.Now,
                    LASTUPDATEUSER = user.USER_USERID,
                    CREATEUSER = user.USER_USERID
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.T_FTP_INFO.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(T_FTP_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.T_FTP_INFO.Where(p => p.FTP_ID.Equals(model.ID)).FirstOrDefault();
                    v.FTP_ADDRESS = model.FTP_ADDRESS;
                    v.FTP_DESC = model.FTP_DESC;
                    v.FTP_LOGIN_USER = model.FTP_LOGIN_USER;
                    v.FTP_LOGIN_PASSWORD = model.FTP_LOGIN_PASSWORD;
                    v.CREATE_DATE = DateTime.Now;
                    v.CREATEUSER = user.USER_USERID;
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
                    var v = DB.T_FTP_INFO.Where(p => p.FTP_ID.Equals(ID)).FirstOrDefault();
                    DB.T_FTP_INFO.DeleteOnSubmit(v);
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
