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
    /// 代码模板
    /// </summary>
    public class SYS_CODE_INFOModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [Display(Name ="ID")]
        public long ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [Display(Name ="代码")]
        public string CODE_CODE { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name ="名称")]
        public string CODE_NAME { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string CODE_DESC { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [Display(Name = "状态")]
        public string CODE_FOR_STATUS { get; set; }
        /// <summary>
        /// 代码类型
        /// </summary>
        [Required]
        [Display(Name = "代码类型")]
        public string CODE_FOR_TYPE { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<SYS_CODE_INFOModel>> GetList(SYS_CODE_INFOModel model)
        {
            ResultInfo<List<SYS_CODE_INFOModel>> Resualt = new ResultInfo<List<SYS_CODE_INFOModel>>();
            try
            {
                List<SYS_CODE_INFOModel> ItemList = new List<SYS_CODE_INFOModel>();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_SYS_CODE
                                where 1 == 1
                                select a;
                    if (model.ID > 0)
                        items = items.Where(p => p.CODE_ID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.CODE_CODE))
                        items = items.Where(p => p.CODE_CODE.Contains(model.CODE_CODE));
                    if (!string.IsNullOrEmpty(model.CODE_NAME))
                        items = items.Where(p => p.CODE_NAME.Contains(model.CODE_NAME));
                    if (!string.IsNullOrEmpty(model.CODE_DESC))
                        items = items.Where(p => p.CODE_DESC.Contains(model.CODE_DESC));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_STATUS))
                        items = items.Where(p => p.CODE_FOR_STATUS.Equals(model.CODE_FOR_STATUS));
                    if (!string.IsNullOrEmpty(model.CODE_FOR_TYPE))
                        items = items.Where(p => p.CODE_FOR_TYPE.Equals(model.CODE_FOR_TYPE));
                    ItemList = items.Select(p => new SYS_CODE_INFOModel()
                    {
                        ID = p.CODE_ID,
                        CODE_CODE = p.CODE_CODE,
                        CODE_NAME = p.CODE_NAME,
                        CODE_DESC = p.CODE_DESC,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        CODE_FOR_TYPE = p.CODE_FOR_TYPE
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
        public ResultInfo<SYS_CODE_INFOModel> GetEnetityByID(long ID)
        {
            ResultInfo<SYS_CODE_INFOModel> Resualt = new ResultInfo<SYS_CODE_INFOModel>();
            try
            {
                SYS_CODE_INFOModel item = new SYS_CODE_INFOModel();
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var items = from a in DB.M_SYS_CODE
                                where a.CODE_ID == ID
                                select a;
                    item = items.Select(p => new SYS_CODE_INFOModel()
                    {
                        ID = p.CODE_ID,
                        CODE_CODE = p.CODE_CODE,
                        CODE_NAME = p.CODE_NAME,
                        CODE_DESC = p.CODE_DESC,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        CODE_FOR_TYPE = p.CODE_FOR_TYPE
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
        public ResultInfo<bool> Insert(SYS_CODE_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                M_SYS_CODE item = new M_SYS_CODE()
                {
                    CODE_CODE = model.CODE_CODE,
                    CODE_NAME = model.CODE_NAME,
                    CODE_DESC = model.CODE_DESC,
                    CODE_FOR_STATUS = model.CODE_FOR_STATUS,
                    CODE_FOR_TYPE = model.CODE_FOR_TYPE,
                    CODE_CREATEUSER = user.USER_USERID,
                    CODE_CREATEDATE = DateTime.Now,
                    CODE_LASTUPDATEUSER = user.USER_USERID,
                    CODE_LASTUPDATE = DateTime.Now
                };
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    DB.M_SYS_CODE.InsertOnSubmit(item);
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
        public ResultInfo<bool> Update(SYS_CODE_INFOModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_SYS_CODE.Where(p => p.CODE_ID.Equals(model.ID)).FirstOrDefault();
                    v.CODE_CODE = model.CODE_CODE;
                    v.CODE_NAME = model.CODE_NAME;
                    v.CODE_DESC = model.CODE_DESC;
                    v.CODE_FOR_STATUS = model.CODE_FOR_STATUS;
                    v.CODE_FOR_TYPE = model.CODE_FOR_TYPE;
                    v.CODE_LASTUPDATEUSER = user.USER_USERID;
                    v.CODE_LASTUPDATE = DateTime.Now;
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
                    var v = DB.M_SYS_CODE.Where(p => p.CODE_ID.Equals(ID)).FirstOrDefault();
                    DB.M_SYS_CODE.DeleteOnSubmit(v);
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
        /// 根据类型和代码获取代码名称
        /// </summary>
        /// <param name="typecode">代码类型</param>
        /// <param name="code">代码</param>
        /// <returns></returns>
        public static string GetCodeNameByCode(string typecode, string code)
        {
            string CodeName = "";
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    var v = DB.M_SYS_CODE.Where(p => p.CODE_FOR_TYPE.ToLower().Equals(typecode.Trim().ToLower()) && p.CODE_CODE.ToLower().Equals(code.Trim().ToLower())).FirstOrDefault();
                    if (v != null)
                        CodeName = v.CODE_NAME;
                }
            }
            catch
            { }
            return CodeName;
        }

        /// <summary>
        /// 根据代码类型获取代码列表
        /// </summary>
        /// <returns></returns>
        public static List<SYS_CODE_INFOModel> GetCodeList(string codeType)
        {
            List<SYS_CODE_INFOModel> resualt = new List<SYS_CODE_INFOModel>();
            try
            {
                using (HXAppDataContext DB = new HXAppDataContext())
                {
                    resualt = DB.M_SYS_CODE.Where(p => p.CODE_FOR_TYPE.ToLower().Trim().Equals(codeType.ToLower().Trim()) && p.CODE_FOR_STATUS.Equals("1")).Select(p => new SYS_CODE_INFOModel()
                    {
                        ID = p.CODE_ID,
                        CODE_CODE = p.CODE_CODE,
                        CODE_NAME = p.CODE_NAME,
                        CODE_DESC = p.CODE_DESC,
                        CODE_FOR_STATUS = p.CODE_FOR_STATUS,
                        CODE_FOR_TYPE = p.CODE_FOR_TYPE
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
