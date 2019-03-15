using BangumiProject.Process.DBService;
using BaseProject.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BangumiProject.Process.ModelLoader
{
    public class AuthorizationLoader : IModelLoader<bool>
    {
        private IServices services { get; set; }
        private ClaimsPrincipal claimsPrincipal { get; set; }
        private string QuanXian { get; set; }
        public AuthorizationLoader(
            IServices services, 
            ClaimsPrincipal claimsPrincipal,
            string QuanXian
            )
            : base("Authorization")
        {
            this.services = services;
            this.claimsPrincipal = claimsPrincipal;
            this.QuanXian = QuanXian;
        }
        public override bool AfterProcess(bool model)
        {
            return model;
        }

        public override bool LoadModel()
        {
            return services.AuthorizationService.AuthorizeAsync(claimsPrincipal, QuanXian).Result.Succeeded;
        }
    }
}
