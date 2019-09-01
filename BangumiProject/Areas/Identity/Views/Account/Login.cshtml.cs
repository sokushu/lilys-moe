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
            [Required(ErrorMessage = "���䲻��Ϊ��")]
            [EmailAddress(ErrorMessage = "�����ʽ����")]
            [Display(Name = "����")]
            public string Email { get; set; }

            [Required(ErrorMessage = "���벻��Ϊ��")]
            [DataType(DataType.Password)]
            [Display(Name = "����")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}