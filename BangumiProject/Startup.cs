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
    /// һЩ���õ����
    /// 
    /// Add-Migration InitialCreate
    /// Update-Database
    /// 
    /// ������ֻ�е���Contextʱʹ�õ����ݿ�����
    /// ���ж��е�ʱ�򣬾���Ҫ����ѡ����
    /// 
    /// Add-Migration -Context BangumiProjectContext "�����ļ�����"
    /// Update-Database -Context BangumiProjectContext "�����ļ�����"
    /// 
    /// ���Ͼ��Ƕ��е�ʱ����еĲ���
    /// 
    /// dotnet ef migrations add --context BangumiProjectContext "Init"
    /// dotnet ef database update --context BangumiProjectContext "Init"
    /// 
    /// ef migrations remove
    /// 
    /// 
    /// Asp.Net Core �İ����ĵ���
    /// <see cref="https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-2.1"/>
    /// 
    /// ����������̵�һЩ��Ϣ
    /// 
    /// �������ǵĹ��̲�һ�㲻Ҫ���ģ��������������ࡣ���Ƿ���������������ʱ���Ҫ�Դ�������ع��ˡ�
    /// ���뻺����ƣ���ߴ������ܡ�
    /// 
    /// ����Ҫע��һ������:
    /// �����в��������Ǵ����ݿ�ȫ�����صģ�������ݲ��࣬���ⲻ��
    /// ������ݶ�Ļ�����Ӱ��Ч�ʡ��ⲿ����δ�����Ż�����
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

            //�����������ݿ�
            services.AddDbContext<MoeMushiContext>(option =>
                option.UseSqlite(Final.DBStr_MoeMushi)
            );
            //�������ݿ�
            services.AddDbContextPool<BangumiProjectContext>(option =>
                option.UseSqlite(Final.DBStr), poolSize: 128);

            //ʹ��΢���ṩ���ڴ滺��
            services.AddMemoryCache(cache =>
            {
                //cache.SizeLimit = 2048; //��������С
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
                // Cookie����
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.LogoutPath = "/LogOut";
                options.SlidingExpiration = true;
            });

            //��ӿ�վ��������
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
            //�����ʵûʲô�õ�
            services.AddTransient<IEmailSender, Services.EmailSender>();
            //==============================================================
            //����ע��
            //==============================================================
            services.AddScoped<ICommDB, CommDB>();

            services.AddSingleton<IConfig, BangumiProject.Areas.Bangumi.Config>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // ��������ҳ��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // ��������������
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("Test");
            
            // ����HTTPS
            app.UseHttpsRedirection();
            // ����404���ҳ��
            app.UseStatusCodePagesWithReExecute("/Error");
            //���һ���µ�Ŀ¼
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Final.FilePath_Image_Thumb),
                RequestPath = new PathString("/Image/Thumb"),
                //���þ�̬�ļ���304����
                OnPrepareResponse  = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            //���Ҫ���ϣ���Ȼwwwroot���ļ���404
            app.UseStaticFiles(new StaticFileOptions()
            {
                //���þ�̬�ļ���304����
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
        /// ������Ŀ��Ҫ���ļ���
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
