using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProjectDBServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BangumiProject.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        //加入内存缓存
        private readonly IMemoryCache _cache;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cache = memoryCache;
        }
    }
}