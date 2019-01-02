using BangumiProject.Process.MoeMushi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Models
{
    public class MoeMushiContext : DbContext
    {

        public DbSet<TimeLineInfo> TimeLines { get; set; }

        public DbSet<AnimeInfoSea> AnimeInfoSeas { get; set; }

        public DbSet<_LogInfo> LogInfos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public MoeMushiContext(DbContextOptions<MoeMushiContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
