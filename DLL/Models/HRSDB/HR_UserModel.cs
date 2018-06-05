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
    /// 绩效平台用户
    /// </summary>
    public class HR_UserModel
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写工号！")]
        [Display(Name = "工号")]
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [Display(Name = "状态")]
        public string STAT2 { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { get; set; }


        /// <summary>
        /// 岗位
        /// </summary>
        [Display(Name = "岗位")]
        public string PostPriv { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [Display(Name = "职位")]
        public int UserPosId { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        [Display(Name = "公司")]
        public string BUKRS { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name = "部门")]
        public int DeptId { get; set; }

        /// <summary>
        /// 中心
        /// </summary>
        [Display(Name = "中心")]
        public string DeptId2 { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Display(Name = "身份证号")]
        public string ICNUM { get; set; }

        /// <summary>
        /// 成本中心
        /// </summary>
        [Display(Name = "成本中心")]
        public string KOSTL { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 员工组
        /// </summary>
        [Display(Name = "员工组")]
        public string PERSG { get; set; }

        /// <summary>
        /// 员工子组
        /// </summary>
        [Display(Name = "员工子组")]
        public string PERSK { get; set; }

        /// <summary>
        /// 工资核算范围
        /// </summary>
        [Display(Name = "工资核算范围")]
        public string ABKRS { get; set; }

        /// <summary>
        /// 人事范围
        /// </summary>
        [Display(Name = "人事范围")]
        public string WERKS { get; set; }

        /// <summary>
        /// 人事子范围
        /// </summary>
        [Display(Name = "人事子范围")]
        public string BTRTL { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user">查询模型</param>
        /// <returns></returns>
        public ResultInfo<List<HR_UserModel>> GetList(HR_UserModel user)
        {
            ResultInfo<List<HR_UserModel>> resualt = new ResultInfo<List<HR_UserModel>>();
            try
            {
                List<HR_UserModel> MenuList = new List<HR_UserModel>();
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var items = DB.Users.Where(p => 1 == 1);
                    if (user.ID > 0)
                        items = items.Where(p => p.Id.Equals(user.ID));
                    if (!string.IsNullOrEmpty(user.UserId))
                        items = items.Where(p => p.UserId.Contains(user.UserId));
                    if (!string.IsNullOrEmpty(user.UserName))
                        items = items.Where(p => p.UserName.Contains(user.UserName));
                    if (!string.IsNullOrEmpty(user.Sex))
                        items = items.Where(p => p.Sex.Equals(user.Sex));
                    if (!string.IsNullOrEmpty(user.BUKRS))
                        items = items.Where(p => p.BUKRS.Contains(user.BUKRS));
                    if (!string.IsNullOrEmpty(user.KOSTL))
                        items = items.Where(p => p.KOSTL.Contains(user.KOSTL));
                    MenuList = items.Select(p => new HR_UserModel()
                    {
                        ID = p.Id,
                        UserId = p.UserId,
                        UserName = p.UserName,
                        Sex = (p.Sex??0).ToString(),
                        BUKRS = p.BUKRS,
                        KOSTL = p.KOSTL,

                        PostPriv = p.PostPriv,
                        UserPosId = p.UserPosId??0,
                        DeptId = p.DeptId??0,
                        DeptId2 = p.DeptId2,
                        ICNUM = p.ICNUM,
                        Mobile = p.Mobile,
                        Password = p.Password,
                        PERSG = (p.PERSG??0).ToString(),
                        PERSK = p.PERSK,
                        ABKRS = p.ABKRS,
                        STAT2 = (p.STAT2??0).ToString(),
                        WERKS = p.WERKS,
                        BTRTL = p.BTRTL
                    }).ToList();
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
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public ResultInfo<HR_UserModel> GetEnetityByID(long ID)
        {
            ResultInfo<HR_UserModel> Resualt = new ResultInfo<HR_UserModel>();
            try
            {
                HR_UserModel item = new HR_UserModel();
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var items = from a in DB.Users
                                where a.Id == ID
                                select a;
                    item = items.Select(p => new HR_UserModel()
                    {
                        ID=p.Id,
                        UserId = p.UserId,
                        UserName = p.UserName,
                        Sex = (p.Sex??0).ToString(),
                        BUKRS = p.BUKRS,
                        KOSTL = p.KOSTL,

                        PostPriv = p.PostPriv,
                        UserPosId = p.UserPosId??0,
                        DeptId = p.DeptId??0,
                        DeptId2 = p.DeptId2,
                        ICNUM = p.ICNUM,
                        Mobile = p.Mobile,
                        Password = p.Password,
                        PERSG = (p.PERSG??0).ToString(),
                        PERSK = p.PERSK,
                        ABKRS = p.ABKRS,
                        STAT2 = (p.STAT2??0).ToString(),
                        WERKS = p.WERKS,
                        BTRTL = p.BTRTL
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
        public ResultInfo<bool> Update(HR_UserModel model, UserModel user)
        {
            ResultInfo<bool> Resualt = new ResultInfo<bool>();
            try
            {
                using (HXOADBDataContext DB = new HXOADBDataContext())
                {
                    var v = DB.Users.Where(p => p.Id.Equals(model.ID)).FirstOrDefault();

                    v.UserId = model.UserId;
                    v.UserName = model.UserName;
                    if (model.Sex != null) v.Sex = Convert.ToInt32(model.Sex);
                    v.BUKRS = model.BUKRS;
                    v.KOSTL = model.KOSTL;

                    v.PostPriv = model.PostPriv;
                    v.UserPosId = model.UserPosId;
                    v.DeptId = model.DeptId;
                    v.DeptId2 = model.DeptId2;
                    v.ICNUM = model.ICNUM;
                    v.Mobile = model.Mobile;
                    v.Password = model.Password;
                    if (model.PERSG != null) v.PERSG = Convert.ToChar(model.PERSG);
                    v.PERSK = model.PERSK;
                    v.ABKRS = model.ABKRS;
                    if (model.STAT2 != null) v.STAT2 = Convert.ToChar(model.STAT2);
                    v.WERKS = model.WERKS;
                    v.BTRTL = model.BTRTL;
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
