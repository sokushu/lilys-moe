using BangumiProjectDBServices.PageModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Controllers
{
    public class BasePageModel : PageModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override PageResult Page()
        {
            Common common = HttpContext.GetComm();
            ViewData[nameof(Common)] = common;
            return base.Page();
        }
    }
}
