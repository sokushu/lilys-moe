using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;

namespace BangumiProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
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

                    var user = await _userManager.GetUserAsync(User);
                    string policyName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    Final.YURI_TYPE _type = policyName.GetYuri_Type();//获取权限类型

                    bool YuriMode = HttpContext.YuriModeCheck();
                    bool ShowNotYuriPage = false;
                    UIMode iMode = HttpContext.UIModeCheck(YuriMode);
                    switch (iMode)
                    {
                        case UIMode.Normal_:
                        case UIMode.Normal_G:
                            //普通模式下，是显示所有数据，所以显示警告
                            //可以关掉。
                            //后期再写相关功能模块
                            ShowNotYuriPage = true;
                            break;
                        default:
                            //其他的模式，不管了
                            break;
                    }
                    //将通用数据写入到Session里面
                    HttpContext.SetComm(new Common
                    {
                        IsSignIn = true
                    });
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
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
