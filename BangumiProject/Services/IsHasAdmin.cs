using BangumiProject.Areas.Users.Models;
using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.AspNetCore.Identity;

namespace BangumiProject.Services
{
    public class IsHasAdmin : IEmpty
    {
        public IsHasAdmin(UserManager<Users> _userManager)
        {
            // 确认是否存在管理员用户
            var count = _userManager.GetUsersInRoleAsync(Final.Yuri_Admin).Result.Count;
            if (count > 0)
            {
                //存在管理员
                Common.HasAdmin = true;
            }
            else
            {
                //不存在管理员，注册时第一个用户就是管理员用户
                Common.HasAdmin = false;
            }
        }
    }
}
