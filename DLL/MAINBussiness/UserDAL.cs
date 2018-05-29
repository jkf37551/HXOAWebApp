using DB;
using DLL.Helpers;
using DLL.Models;
using DLL.Models.MainDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.MAINBussiness
{
    public class UserDAL
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Userid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static ResultInfo<UserModel> LoginValidate(UserModel User)
        {
            ResultInfo<UserModel> resualt = new ResultInfo<UserModel>();
            try
            {
                Users v = null;
                if (User == null)
                    throw new Exception("缺少用户信息！");
                if (string.IsNullOrEmpty(User.USER_USERID))
                    throw new Exception("缺少用户账号！");
                if (string.IsNullOrEmpty(User.USER_PASSWORD))
                    throw new Exception("缺少密码！");
                using (HXOADBDataContext UserDB = new HXOADBDataContext())
                {
                    v = UserDB.Users.Where(p => p.UserId.Equals(User.USER_USERID)).FirstOrDefault();
                }
                if (v == null)
                    throw new Exception("用户不存在！");
                if (String.IsNullOrEmpty(v.Password))
                    throw new Exception("用户未授权,请先初始化用户信息！");

                if (!v.Password.ToLower().Equals(MySecurity.MD5Encrypt(User.USER_PASSWORD)))
                    throw new Exception("账号或密码错误！");

                User.CODE_FOR_SEX = v.Sex.ToString();
                User.USER_NAME = v.UserName;

                resualt.IsSuccess = true;
                resualt.Data = User;
            }
            catch (Exception ex)
            {
                resualt.IsSuccess = false;
                resualt.Message = ex.Message;
            }
            return resualt;
        }        
    }
}
