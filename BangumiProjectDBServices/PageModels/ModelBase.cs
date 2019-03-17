using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.PageModels
{
    public class ModelBase
    {
        public bool IsSignIn { get; set; }

        public bool HasKenGen { get; set; }

        public string Username { get; set; }

        public string UID { get; set; }

        public string Email { get; set; }
    }
}
