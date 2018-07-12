using DB;
using DLL.Models.MainDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models.HRSDB
{
    /// <summary>
    /// 门店信息
    /// </summary>
    public class HR_WerksModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// 门店代码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写门店代码！")]
        [Display(Name = "门店代码")]
        public string WERKS { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [Display(Name = "门店名称")]
        public string NAME1 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int? STATS { get; set; }

        /// <summary>
        /// 绩效类型
        /// </summary>
        [Display(Name = "绩效类型")]
        public string AREA { get; set; }
        
        /// <summary>
        /// 公司
        /// </summary>
        [Display(Name = "公司")]
        public string BUKRS { get; set; }

        /// <summary>
        /// 分部
        /// </summary>
        [Display(Name = "分部")]
        public string BZIRK { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Display(Name = "区域")]
        public string BZIRK2 { get; set; }

        /// <summary>
        /// 门店模式
        /// </summary>
        [Display(Name = "门店模式")]
        public string ZTYPE { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user">查询模型</param>
        /// <param name="Current_Page">查询页面</param>
        /// <param name="Page_Size">每页显示数</param>
        /// <returns></returns>
        public ResultInfo<List<HR_WerksModel>> GetList(HR_WerksModel model, int Current_Page, int Page_Size)
        {
            ResultInfo<List<HR_WerksModel>> resualt = new ResultInfo<List<HR_WerksModel>>();
            try
            {
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var items = DB.T_HR_SAP_WERKS.Where(p => 1 == 1);
                    if (model.ID > 0)
                        items = items.Where(p => p.WID.Equals(model.ID));
                    if (!string.IsNullOrEmpty(model.WERKS))
                        items = items.Where(p => p.WERKS.Contains(model.WERKS));
                    if (!string.IsNullOrEmpty(model.NAME1))
                        items = items.Where(p => p.NAME1.Contains(model.NAME1));
                    if (model.STATS != null)
                        items = items.Where(p => p.STATS.Equals(model.STATS));
                    if (!string.IsNullOrEmpty(model.BUKRS))
                        items = items.Where(p => p.BUKRS.Contains(model.BUKRS));
                    if (!string.IsNullOrEmpty(model.BZIRK))
                        items = items.Where(p => p.BZIRK.Contains(model.BZIRK));
                    if (!string.IsNullOrEmpty(model.BZIRK2))
                        items = items.Where(p => p.BZIRK2.Contains(model.BZIRK2));
                    if (!string.IsNullOrEmpty(model.ZTYPE))
                        items = items.Where(p => p.ZTYPE.Contains(model.ZTYPE));
                    //获取指定页面数据
                    resualt.Total_Recoder = items.Count();
                    resualt.Current_PageId = Current_Page;
                    resualt.Page_Size = Page_Size;
                    if (Page_Size > 0 && Current_Page > 0)
                    {
                        resualt.Total_Page = resualt.Total_Recoder / Page_Size + (resualt.Total_Recoder % Page_Size > 0 ? 1 : 0);
                        resualt.Data = items.OrderBy(p => p.WID).Skip(Page_Size * (Current_Page - 1)).Take(Page_Size).Select(p => new HR_WerksModel()
                        {
                            ID = p.WID,
                            AREA = p.AREA,
                            BZIRK = p.BZIRK,
                            BZIRK2 = p.BZIRK2,
                            BUKRS = p.BUKRS,
                            NAME1 = p.NAME1,
                            WERKS = p.WERKS,
                            STATS = p.STATS,
                            ZTYPE = p.ZTYPE
                        }).ToList();
                    }
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

        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public ResultInfo<HR_WerksModel> GetEnetityByID(long ID)
        {
            ResultInfo<HR_WerksModel> Resualt = new ResultInfo<HR_WerksModel>();
            try
            {
                HR_WerksModel item = new HR_WerksModel();
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var items = from a in DB.T_HR_SAP_WERKS
                                where a.WID == ID
                                select a;
                    item = items.Select(p => new HR_WerksModel()
                    {
                        ID = p.WID,
                        WERKS = p.WERKS,
                        NAME1 = p.NAME1,
                        AREA = p.AREA,
                        BUKRS = p.BUKRS,
                        BZIRK = p.BZIRK,
                        BZIRK2 = p.BZIRK2,
                        STATS = p.STATS.Value,
                        ZTYPE = p.ZTYPE
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
        /// 编辑代码类型
        /// </summary>
        /// <param name="model">更新数据</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        public ResultInfo<bool> Update(HR_WerksModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var v = DB.T_HR_SAP_WERKS.Where(p => p.WID.Equals(model.ID)).FirstOrDefault();

                    v.WERKS = model.WERKS;
                    v.AREA = model.AREA;
                    v.STATS = model.STATS;
                    v.BUKRS = model.BUKRS;
                    v.BZIRK = model.BZIRK;

                    v.BZIRK2 = model.BZIRK2;
                    v.NAME1 = model.NAME1;
                    v.ZTYPE = model.ZTYPE;
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
