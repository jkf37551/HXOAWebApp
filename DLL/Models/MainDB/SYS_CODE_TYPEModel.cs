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
    /// 代码类型
    /// </summary>
    public class SYS_CODE_TYPEModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 类型代码
        /// </summary>
        [Required]
        [Display(Name = "类型代码")]
        public string TYPE_CODE { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [Display(Name = "类型名称")]
        public string TYPE_DESC { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码类型
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<SYS_CODE_TYPEModel>> GetList(SYS_CODE_TYPEModel model)
        {
            ResultInfo<List<SYS_CODE_TYPEModel>> Resualt = new ResultInfo<List<SYS_CODE_TYPEModel>>();
            try
            {
                List<SYS_CODE_TYPEModel> ItemList = new List<SYS_CODE_TYPEModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_SYS_TYPE
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.TYPE_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.TYPE_CODE))
                        items = items.Where(p => p.TYPE_CODE.Contains(model.TYPE_CODE));
                    if (!string.IsNullOrEmpty(model.TYPE_DESC))
                        items = items.Where(p => p.TYPE_DESC.Contains(model.TYPE_DESC));
                    ItemList = items.Select(p => new SYS_CODE_TYPEModel()
                    {
                        ID = p.TYPE_ID,
                        TYPE_CODE = p.TYPE_CODE,
                        TYPE_DESC = p.TYPE_DESC
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
        public ResultInfo<SYS_CODE_TYPEModel> GetEnetityByID(long ID)
        {
            ResultInfo<SYS_CODE_TYPEModel> Resualt = new ResultInfo<SYS_CODE_TYPEModel>();
            try
            {
                SYS_CODE_TYPEModel item = new SYS_CODE_TYPEModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_SYS_TYPE
                                where a.TYPE_ID == ID
                                select a;
                    item = items.Select(p => new SYS_CODE_TYPEModel()
                    {
                        ID = p.TYPE_ID,
                        TYPE_CODE = p.TYPE_CODE,
                        TYPE_DESC = p.TYPE_DESC
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
        public ResultInfo<bool> Insert(SYS_CODE_TYPEModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                M_SYS_TYPE item = new M_SYS_TYPE()
                {
                    TYPE_CODE = model.TYPE_CODE,
                    TYPE_DESC = model.TYPE_DESC,
                    TYPE_CREATEUSER = user.USER_USERID,
                    TYPE_CREATE_DATE = DateTime.Now,
                    TYPE_LASTUPDATEUSER = user.USER_USERID,
                    TYPE_LASTUPDATE = DateTime.Now
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.M_SYS_TYPE.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(SYS_CODE_TYPEModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_SYS_TYPE.Where(p => p.TYPE_ID.Equals(model.ID)).FirstOrDefault();
                    v.TYPE_DESC = model.TYPE_DESC;
                    v.TYPE_CODE = model.TYPE_CODE;
                    v.TYPE_LASTUPDATEUSER = user.USER_USERID;
                    v.TYPE_LASTUPDATE = DateTime.Now;
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
                    var v = DB.M_SYS_TYPE.Where(p => p.TYPE_ID.Equals(ID)).FirstOrDefault();
                    DB.M_SYS_TYPE.DeleteOnSubmit(v);
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
        /// 根据类型代码获取类型描述
        /// </summary>
        /// <param name="code">类型代码</param>
        /// <returns>类型描述</returns>
        public static string GetTypeNameByCode(string code)
        {
            string TypeName = "";
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_SYS_TYPE.Where(p => p.TYPE_CODE.ToLower().Equals(code.Trim().ToLower())).FirstOrDefault();
                    if (v != null)
                        TypeName = v.TYPE_DESC;
                }
            }
            catch
            { }
            return TypeName;
        }

        /// <summary>
        /// 获取全部类型
        /// </summary>
        /// <returns></returns>
        public static List<SYS_CODE_TYPEModel> GetALLTypeList()
        {
            List<SYS_CODE_TYPEModel> resualt = new List<SYS_CODE_TYPEModel>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    resualt = DB.M_SYS_TYPE.Select(p => new SYS_CODE_TYPEModel()
                    {
                        ID = p.TYPE_ID,
                        TYPE_CODE = p.TYPE_CODE,
                        TYPE_DESC = p.TYPE_DESC
                    }).ToList();
                }
            }
            catch
            {
            }
            return resualt;
        }
        #endregion
    }
}
