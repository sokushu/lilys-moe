using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Search.Controllers
{
    [Area("Search")]
    public class SearchController : Controller
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Search")]
        public IActionResult Index(string w)
        {
            return View();
        }
    }
}