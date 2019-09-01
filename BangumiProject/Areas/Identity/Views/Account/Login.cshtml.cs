using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BangumiProject.Areas.Identity.Views.Account
{
    public class LoginModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "邮箱不能为空")]
            [EmailAddress(ErrorMessage = "邮箱格式错误")]
            [Display(Name = "邮箱")]
            public string Email { get; set; }

            [Required(ErrorMessage = "密码不能为空")]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}