using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = BangumiProject.Areas.Users.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using MoeUtilsBox;
using BangumiProject.Areas.Bangumi.Models;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    [Area("Bangumi")]
    [Route("/BangumiSub")]
    [ApiController]
    public class BangumiSubController : ControllerBase
    {
        private readonly ICommDB _DBServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiSubController(
            ICommDB _DBServices,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._userManager = _userManager;
            this._DBServices = _DBServices;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }
        // GET: /BangumiSub
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode((int)HttpStatusCode.Forbidden);
        }

        // GET: /BangumiSub/5
        [HttpGet("{id?}", Name = "Get")]
        public IActionResult Get(int id)
        {
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return new JsonResult("");
            }
            string UserID = _userManager.GetUserId(HttpContext.User);
            return new JsonResult("");
        }

        // POST: /BangumiSub
        [HttpPost]
        public IActionResult Post([FromBody] int AnimeID = -1)
        {
            if (_DBServices.HasAnimeID(AnimeID))
            {

            }
            return new JsonResult("");
        }

        // PUT: /BangumiSub/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return new JsonResult("");
        }

        // DELETE: /ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new JsonResult("");
        }
    }
}
