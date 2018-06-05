using DLL.Models.HRSDB;
using HXWebApp.Controllers;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var result = Model.QueryModel.GetList(Model.QueryModel);
            if (result.IsSuccess)
            {
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