using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Controllers
{
    public class BaseController : Controller
    {

        private int HashCode { get; set; } = 0;

        public BaseController()
        {

        }
        
        protected virtual bool IsChange(object Obj)
        {
            int HashCode = Obj.GetHashCode();
            if (HashCode == 0)
            {
                this.HashCode = HashCode;
            }
            PartialView();
            return this.HashCode != HashCode;
        }

        protected virtual void Clean()
        {
            HashCode = 0;
        }
    }
}