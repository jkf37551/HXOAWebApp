using DLL.MAINBussiness;
using DLL.Models.MainDB;
using HXWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Controllers
{
    [LoginValidate]
    public class SysCodeController : BaseController<SYS_CODE_INFOModel>
    {
        // GET: SysCode
        public ActionResult List()
        {
            return View(_pageInfo);
        }

        public ActionResult Query(PageModel<SYS_CODE_INFOModel> Model)
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

        public ActionResult Create()
        {
            return View(new SYS_CODE_INFOModel());
        }

        [HttpPost]
        public ActionResult Create(SYS_CODE_INFOModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            var resualt = Model.Insert(Model, GetLoginUser());
            if (resualt.IsSuccess)
            {
                return SysInfo("/SysCode/List");
            }
            else
            {
                return SysErro(resualt.Message);
            }
        }

        public ActionResult Edit(long id)
        {
            var resualt = new SYS_CODE_INFOModel().GetEnetityByID(id);
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
        public ActionResult Edit(SYS_CODE_INFOModel Model)
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

        public ActionResult Delete(long id)
        {
            var resualt = new SYS_CODE_INFOModel().Delete(id, GetLoginUser());
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