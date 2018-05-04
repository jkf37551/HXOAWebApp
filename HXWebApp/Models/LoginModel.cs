using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HXWebApp.Models
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        [Display(Name = "账号")]
        public string UserId { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [Display(Name = "电子邮件")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}