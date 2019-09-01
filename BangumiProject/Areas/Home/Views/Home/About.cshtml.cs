using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProjectDBServices.PageModels.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BangumiProject.Areas.Home.Views.Home
{
    public class AboutModel : BaseModel
    {
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string Wenti { get; set; }
        }
    }

    
}