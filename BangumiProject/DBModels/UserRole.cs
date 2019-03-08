using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    public class UserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// 经验值
        /// </summary>
        public int KenGenChi { get; set; }
        /// <summary>
        /// 记录最后登录时间，用于改经验值
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
