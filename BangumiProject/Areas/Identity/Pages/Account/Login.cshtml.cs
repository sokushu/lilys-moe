﻿using BangumiProject.Controllers;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BasePageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<User> _userManager;
        //加入内存缓存
        private readonly IMemoryCache _cache;

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _cache = memoryCache;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

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

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("用户登录成功");

                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    string policyName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    Final.YURI_TYPE _type = policyName.GetYuri_Type();//获取权限类型

                    bool YuriMode = HttpContext.YuriModeCheck();
                    UIMode iMode = HttpContext.UIModeCheck(YuriMode);

                    //将通用数据写入到Session里面
                    HttpContext.SetComm(new BaseModel
                    {
                        IsSignIn = true,
                        UI = UI.CreateUI(YuriMode, iMode),
                        YuriMode = YuriMode,
                        BackPicPath = user.UserBackPic ?? string.Empty,
                        UIMode = iMode,
                        Username = user.UserName,
                        YURI_TYPE = _type
                    });
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("您的用户名已被锁定");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "用户名或密码错误，登陆失败");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
