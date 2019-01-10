using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class SQLiteController : ControllerBase
    {
        private readonly BangumiProjectContext _DB;
        public SQLiteController(BangumiProjectContext _DB)
        {
            this._DB = _DB;
        }

        [HttpPost]
        public IActionResult ExSQL(string SQL)
        {
#if DEBUG
            return new JsonResult($"OK: {_DB.Database.ExecuteSqlCommand(SQL)}");
#else
            return new JsonResult("Hello");
#endif
        }
    }
}