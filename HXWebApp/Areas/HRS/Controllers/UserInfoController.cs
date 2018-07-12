using DLL.Models.HRSDB;
using HXWebApp.Controllers;
using HXWebApp.Models;
using System.Web.Mvc;

namespace HXWebApp.Areas.HRS.Controllers
{
    [LoginValidate]
    public class UserInfoController : BaseController<HR_UserModel>
    {
        // GET: /HRS/UserInfo
        public ActionResult List()
        {
            return View(_pageInfo);
        }

        public ActionResult Query(PageModel<HR_UserModel> Model)
        {
            _pageInfo = Model;
            var result = Model.QueryModel.GetList(Model.QueryModel,1,20);
            if (result.IsSuccess)
            {
                _pageInfo.QueryData = result.Data;
                _pageInfo.Current_PageId = result.Current_PageId;
                _pageInfo.Page_Size = result.Page_Size;
                _pageInfo.Total_Page = result.Total_Page;
                _pageInfo.Total_Recoder = result.Total_Recoder;

                _pageInfo.QueryModel = Model.QueryModel;

                //获取路径参数信息
                var items = this.HttpContext.Request.Url.Segments;
                
                if (items != null && items.Length > 0)
                {
                    for (int i = 0; i < items.Length - 1; i++)
                        _pageInfo.Key_Model = (string.IsNullOrEmpty(_pageInfo.Key_Model)?"":(_pageInfo.Key_Model+".")) + items[i].Replace("/", "");
                    _pageInfo.Controll = items[items.Length - 2].Replace("/", "");
                }
                _pageInfo.Action = "PageQuery";
                Session.Add(_pageInfo.Key_Model, _pageInfo);
                return View("List", _pageInfo);
            }
            else
            {
                return SysErro(result.Message);
            }
        }

        public ActionResult PageQuery(int PageId)
        {
            //获取路径参数信息
            string sessionstr = "";
            var items = this.HttpContext.Request.Url.Segments;

            if (items != null && items.Length > 0)
            {
                for (int i = 0; i < items.Length - 1; i++)
                    sessionstr = (string.IsNullOrEmpty(sessionstr) ? "" : (sessionstr + ".")) + items[i].Replace("/", "");
            }

            _pageInfo = (PageModel<HR_UserModel>)Session[sessionstr];
            var result = _pageInfo.QueryModel.GetList(_pageInfo.QueryModel,PageId,_pageInfo.Page_Size);
            if (result.IsSuccess)
            {
                _pageInfo.Current_PageId = result.Current_PageId;
                _pageInfo.QueryData = result.Data;
                return View("List", _pageInfo);
            }
            else
            {
                return SysErro(result.Message);
            }
        }

        public ActionResult Detail(long id)
        {
            var resualt = new HR_UserModel().GetEnetityByID(id);
            if (resualt.IsSuccess)
            {
                //操作成功
                return View(resualt.Data);
            }
            else
            {
                //操作失败
                return SysErro(resualt.Message);
            }
        }
        
        public ActionResult Edit(long id)
        {
            var resualt = new HR_UserModel().GetEnetityByID(id);
            if (resualt.IsSuccess)
            {
                //操作成功
                return View(resualt.Data);
            }
            else
            {
                //操作失败
                return SysErro(resualt.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(HR_UserModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var resualt = Model.Update(Model, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysCode/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }
    }
}