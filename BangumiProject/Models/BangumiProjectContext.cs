﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BangumiProject.Models
{
    public class BangumiProjectContext : IdentityDbContext<Users, IdentityRole, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Anime> Anime { get; set; }
        public DbSet<AnimeTag> AnimeTag { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTags> BlogTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AnimeSouce> AnimeSouces { get; set; }
        public DbSet<AnimeSouceComm> AnimeSouceComms { get; set; }
        public DbSet<AnimeComm> AnimeComms { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Memo> Memos { get; set; }
        public DbSet<UserAnimeInfo> UserAnimeInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public BangumiProjectContext(DbContextOptions<BangumiProjectContext> options)
           : base(options)
        {
        }

        public BangumiProjectContext() : base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // 数据库建立种子数据
            builder.Entity<IdentityRole>().HasData(
                // 建立用户权限数据
                new IdentityRole { Name = Final.Yuri_Admin, NormalizedName = Final.Yuri_Admin.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Girl, NormalizedName = Final.Yuri_Girl.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Yuri5, NormalizedName = Final.Yuri_Yuri5.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Yuri4, NormalizedName = Final.Yuri_Yuri4.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Yuri3, NormalizedName = Final.Yuri_Yuri3.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Yuri2, NormalizedName = Final.Yuri_Yuri2.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Yuri1, NormalizedName = Final.Yuri_Yuri1.ToUpper() },
                new IdentityRole { Name = Final.Yuri_Boy, NormalizedName = Final.Yuri_Boy.ToUpper() }
            );

            // 添加动画索引：动画ID，动画名字，动画类型
            builder.Entity<Anime>().HasIndex(Anime => new { Anime.AnimeID, Anime.AnimeName, Anime.AnimeType });
            // 添加动画源索引：源ID，源的名字
            builder.Entity<AnimeSouce>().HasIndex(ASouce => new { ASouce.ID, ASouce.Name });
            // 动画标签索引：标签ID，标签名字
            builder.Entity<AnimeTag>().HasIndex(ATag => new { ATag.TagID, ATag.TagName });

            
            // 动画的类型映射
            var converter = new ValueConverter<AnimeType, string>(
                v => v.ToString(),
                v => (AnimeType)Enum.Parse(typeof(AnimeType), v));
            builder.Entity<Anime>().Property(e => e.AnimeType).HasConversion(converter);

            // 外键约束
            builder.Entity<Images>().HasOne(img => img.UpLoadUsers).WithMany(user => user.Images).HasConstraintName("Images_User_PK");
            builder.Entity<Images>().HasOne(img => img.Photos).WithMany(photo => photo.Images).HasConstraintName("Image_Photo_PK");
            builder.Entity<UserAnimeInfo>().HasOne(UAInfo => UAInfo.Users).WithMany(user => user.UserAnimeInfos).HasConstraintName("UserAnimeInfo_User_PK");
            builder.Entity<UserAnimeInfo>().HasOne(UAInfo => UAInfo.SubAnime).WithMany(anime => anime.UserAnimeInfos).HasConstraintName("UserAnimeInfo_Anime_PK");
            builder.Entity<Memo>().HasOne(memo => memo.UserAnimeInfo).WithMany(UAInfo => UAInfo.Memos).HasConstraintName("Memo_UserAnimeInfo_PK");
            builder.Entity<Blog>().HasOne(blog => blog.UpLoadUser).WithMany(user => user.Blogs).HasConstraintName("Blog_User_PK");
            builder.Entity<BlogTags>().HasOne(blogtag => blogtag.Blogs).WithMany(blog => blog.TagIDs).HasConstraintName("BlogTags_Blog_PK");
            builder.Entity<Comment>().HasOne(comm => comm.Users).WithMany(user => user.Comments).HasConstraintName("Comment_User_PK");
            builder.Entity<Comment>().HasOne(comm => comm.Blogs).WithMany(blog => blog.Comments).HasConstraintName("Comment_Blogs_PK");
            builder.Entity<AnimeSouce>().HasOne(AniSou => AniSou.Anime).WithMany(anime => anime.Souce).HasConstraintName("AnimeSouce_Anime_PK");
            builder.Entity<AnimeTag>().HasOne(animetag => animetag.Anime).WithMany(anime => anime.Tags).HasConstraintName("AnimeTag_Anime_PK");
            builder.Entity<AnimeSouceComm>().HasOne(aniSoComm => aniSoComm.AnimeSouce).WithMany(aniSou => aniSou.AnimeSouceComms).HasConstraintName("AnimeSouceComm_AnimeSouce_PK");
            builder.Entity<AnimeSouceComm>().HasOne(aniSoComm => aniSoComm.Users).WithMany(user => user.AnimeSouceComms).HasConstraintName("AnimeSouceComm_User_PK");
            builder.Entity<AnimeComm>().HasOne(anicomm => anicomm.Users).WithMany(user => user.AnimeComms).HasConstraintName("AnimeComm_User_PK");
            builder.Entity<AnimeComm>().HasOne(aniComm => aniComm.Anime).WithMany(anime => anime.AnimeComms).HasConstraintName("AnimeComm_Anime_PK");

            //默认值
            builder.Entity<Users>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<UserAnimeInfo>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Memo>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Photos>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Images>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Blog>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<BlogTags>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Comment>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<Anime>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<AnimeSouce>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<AnimeTag>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<AnimeSouceComm>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
            builder.Entity<AnimeComm>().Property(v => v.Time).HasDefaultValueSql("datetime('now')");
        }
    }
}
