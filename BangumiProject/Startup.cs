using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoeUtilsBox;
using MoeUtilsBox.String;
using BangumiProject.Component.Interface;
using BangumiProject.Areas.Users.Models;
using BangumiProject.Services;
using MoeUtilsBox.Utils;

namespace BangumiProject
{
    /// <summary>
    /// 一些常用的命令：
    /// 
    /// Add-Migration InitialCreate
    /// Update-Database
    /// 
    /// 以上是只有单个Context时使用的数据库命令
    /// 当有多行的时候，就需要进行选择了
    /// 
    /// Add-Migration -Context BangumiProjectContext "代码文件名字"
    /// Update-Database -Context BangumiProjectContext "代码文件名字"
    /// 
    /// 以上就是多行的时候进行的操作
    /// 
    /// dotnet ef migrations add --context BangumiProjectContext "Init"
    /// dotnet ef database update --context BangumiProjectContext "Init"
    /// 
    /// ef migrations remove
    /// 
    /// 
    /// Asp.Net Core 的帮助文档：
    /// <see cref="https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-2.1"/>
    /// 
    /// 关于这个工程的一些信息
    /// 
    /// 现在我们的工程糙一点不要紧的，访问人数并不多。但是访问人数多起来的时候就要对代码进行重构了。
    /// 加入缓存机制，提高处理性能。
    /// 
    /// 现在要注意一个问题:
    /// 我们有部分数据是从数据库全部加载的，如果数据不多，问题不大。
    /// 如果数据多的话，会影响效率。这部分是未来的优化方向
    /// 
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;
            Mkdir();
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            });

            //加载爬虫数据库
            services.AddDbContext<MoeMushiContext>(option =>
                option.UseSqlite(Final.DBStr_MoeMushi)
            );
            //加载数据库
            services.AddDbContextPool<BangumiProjectContext>(option =>
                option.UseSqlite(Final.DBStr), poolSize: 128);

            //使用微软提供的内存缓存
            services.AddMemoryCache(cache =>
            {
                //cache.SizeLimit = 2048; //缓存最大大小
            });

            services.AddIdentity<Users, IdentityRole>(ConfigurationBinder =>
            {
                ConfigurationBinder.User.RequireUniqueEmail = false;
                ConfigurationBinder.Password.RequiredLength = 6;
                ConfigurationBinder.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<BangumiProjectContext>().AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie设置
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.LogoutPath = "/LogOut";
                options.SlidingExpiration = true;
            });

            //添加跨站访问限制
#if DEBUG
            services.AddCors(option =>
            {
                option.AddPolicy("Test", bilder => bilder.WithOrigins("http://localhost:5000").AllowCredentials());
            });
#else
            services.AddCors(option =>
            {
                option.AddPolicy("Test", bilder => bilder.WithOrigins("http://lilys.moe").AllowCredentials());
            });
#endif
            services.AddSession(session =>
            {
                session.IdleTimeout = TimeSpan.FromHours(1);
                session.IOTimeout = TimeSpan.FromHours(1);
            });
            services.AddAuthorization(option => 
            {
                List<string> Yuris = new List<string>{ Final.Yuri_Admin, Final.Yuri_Girl, Final.Yuri_Yuri5, Final.Yuri_Yuri4, Final.Yuri_Yuri3, Final.Yuri_Yuri2, Final.Yuri_Yuri1, Final.Yuri_Boy };

                option.AddPolicy(Final.Yuri_Admin, policy => policy.RequireRole(Yuris.Take(1).ToArray()));
                option.AddPolicy(Final.Yuri_Girl, policy => policy.RequireRole(Yuris.Take(2).ToArray()));
                option.AddPolicy(Final.Yuri_Yuri5, policy => policy.RequireRole(Yuris.Take(3).ToArray()));
                option.AddPolicy(Final.Yuri_Yuri4, policy => policy.RequireRole(Yuris.Take(4).ToArray()));
                option.AddPolicy(Final.Yuri_Yuri3, policy => policy.RequireRole(Yuris.Take(5).ToArray()));
                option.AddPolicy(Final.Yuri_Yuri2, policy => policy.RequireRole(Yuris.Take(6).ToArray()));
                option.AddPolicy(Final.Yuri_Yuri1, policy => policy.RequireRole(Yuris.Take(7).ToArray()));
                option.AddPolicy(Final.Yuri_Boy, policy => policy.RequireRole(Yuris.ToArray()));
            });
            //这个其实没什么用的
            services.AddTransient<IEmailSender, Services.EmailSender>();
            //==============================================================
            //服务注册
            //==============================================================
            services.AddScoped<ICommDB, CommDB>();

            services.AddSingleton<IConfig, BangumiProject.Areas.Bangumi.Config>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 开启错误页面
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // 发生服务器错误
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("Test");
            
            // 启用HTTPS
            app.UseHttpsRedirection();
            // 开启404相关页面
            app.UseStatusCodePagesWithReExecute("/Error");
            //添加一个新的目录
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Final.FilePath_Image_Thumb),
                RequestPath = new PathString("/Image/Thumb"),
                //设置静态文件的304缓存
                OnPrepareResponse  = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            //这个要加上，不然wwwroot的文件会404
            app.UseStaticFiles(new StaticFileOptions()
            {
                //设置静态文件的304缓存
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc();

        }

        /// <summary>
        /// 创建项目必要的文件夹
        /// </summary>
        private void Mkdir()
        {
            
            if (!Directory.Exists(Final.FilePath))
                Directory.CreateDirectory(Final.FilePath);
            if (!Directory.Exists(Final.FilePath_Image))
                Directory.CreateDirectory(Final.FilePath_Image);
            if (!Directory.Exists(Final.FilePath_ImageProcess_Thumb))
                Directory.CreateDirectory(Final.FilePath_ImageProcess_Thumb);
            if (!Directory.Exists(Final.FilePath_Image_Thumb))
                Directory.CreateDirectory(Final.FilePath_Image_Thumb);
            if (!File.Exists(Final.FilePath_VersionLog))
                File.CreateText(Final.FilePath_VersionLog);
#if !DEBUG
            if (!Directory.Exists(Final.DateBasePath))
                Directory.CreateDirectory(Final.DateBasePath);
            string ProjectPath = System.IO.Directory.GetCurrentDirectory();
            foreach (var file in Directory.GetFiles(ProjectPath))
            {
                try
                {
                    int cut = 0;
                    string fileName;
                    bool check = (cut = (fileName = file.GetFileName()).LastIndexOf('.')) > 0 ? fileName.Substring(cut).EndsWith("db") : false;
                    if (check)
                    {
                        File.Move(file, $"{Final.DateBasePath}{fileName}");
                    }
                }
                catch (Exception)
                {
                
                }
                
            }
#endif
        }
    }

    public interface IConfig
    {
        
    }
}
