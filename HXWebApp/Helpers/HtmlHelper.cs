using DLL.Models.MainDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWebApp.Helpers
{
    /// <summary>
    /// 页面辅助类
    /// </summary>
    public class HtmlHelper
    {
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
                if (string.IsNullOrEmpty(typecode))
                    typecode = "";
                if (string.IsNullOrEmpty(code))
                    code = "";
                CodeName = SYS_CODE_INFOModel.GetCodeNameByCode(typecode, code);
            }
            catch
            { }
            return CodeName;
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
                if (string.IsNullOrEmpty(code))
                    code = "";
                TypeName = SYS_CODE_TYPEModel.GetTypeNameByCode(code);
            }
            catch
            { }
            return TypeName;
        }
        
        /// <summary>
        /// 获取代码类型列表
        /// </summary>
        /// <param name="selectedvalue">默认选中项</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetALLTypeList(string selectedvalue)
        {
            IEnumerable<SelectListItem> SlectItemList = null;
            try
            {
                if (string.IsNullOrEmpty(selectedvalue))
                    selectedvalue = "";
                var TypeList = SYS_CODE_TYPEModel.GetALLTypeList();
                if (TypeList != null)
                {
                    SlectItemList = TypeList.Select(p => new SelectListItem()
                    {
                        Text = p.TYPE_DESC,
                        Value = p.TYPE_CODE,
                        Selected = (p.TYPE_CODE.Trim().ToLower().Equals(selectedvalue.Trim().ToLower()))
                    }).ToList();
                }
            }
            catch
            { }
            return SlectItemList;
        }

        /// <summary>
        /// 根据代码类型获取代码列表
        /// </summary>
        /// <param name="selectedvalue">默认选中项</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetCodeList(string codeType,string selectedvalue)
        {
            IEnumerable<SelectListItem> SlectItemList = null;
            try
            {
                if (string.IsNullOrEmpty(codeType))
                    codeType = "";
                if (string.IsNullOrEmpty(selectedvalue))
                    selectedvalue = "";
                var TypeList = SYS_CODE_INFOModel.GetCodeList(codeType);
                if (TypeList != null)
                {
                    SlectItemList = TypeList.Select(p => new SelectListItem()
                    {
                        Text = p.CODE_NAME,
                        Value = p.CODE_CODE,
                        Selected = (p.CODE_CODE.Trim().ToLower().Equals(selectedvalue.Trim().ToLower()))
                    }).ToList();
                }
            }
            catch
            { }
            return SlectItemList;
        }
    }
}