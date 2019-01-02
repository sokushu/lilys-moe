﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangumiProject.Controllers
{
    public class Final
    {

        public const int StatusCode403 = 403;
        public const int StatusCode404 = 404;
        public const int StatusCodeBangumiNotFound = 10001;
        public const string Image_JPEG = "image/jpeg";

        /*
         * ====================================================
         * 以下是应用使用的文件，默认文件等
         * 正式版使用的是Linux下的路径
         * ====================================================
         */
#if DEBUG
        /// <summary>
        /// 文件存储地址
        /// </summary>
        public const string FilePath = @"E:\TEST\";
        public const string FilePath_VersionLog = @"E:\TEST\TestLog";
        public const string DateBasePath = "";
        /// <summary>
        /// 图片储存目录
        /// </summary>
        public const string FilePath_Image = @"E:\TEST\TESTPIC\";
        public const string FilePath_Image_Static = "";
        /// <summary>
        /// 缩略图储存目录
        /// </summary>
        public const string FilePath_Image_Thumb = @"E:\TEST\TESTPIC\Thumb\";
        public const string FilePath_ImageProcess_Thumb = @"E:\TEST\TESTPIC\ProcessThumb\";
#else
        
        /// <summary>
        /// 文件存储地址
        /// </summary>
        public const string FilePath = "/home/BangumiFiles/";
        public const string FilePath_VersionLog = "/home/BangumiFiles/TestLog";
        public const string DateBasePath = "/home/datebase/";
        /// <summary>
        /// 图片储存目录
        /// </summary>
        public const string FilePath_Image = "/home/BangumiFiles/pic/";
        public const string FilePath_Image_Static = "";
        /// <summary>
        /// 缩略图储存目录
        /// </summary>
        public const string FilePath_Image_Thumb = "/home/BangumiFiles/pic/Thumb/";
        public const string FilePath_ImageProcess_Thumb = "/home/BangumiFiles/pic/ProcessThumb/";

#endif

        /// <summary>
        /// 默认的用户头像
        /// </summary>
        public const string DefaultUserPic = "/images/6c573befgw1fabxbznz9sj20qo0xcdk4.jpg";
        /// <summary>
        /// 默认的动画图片
        /// </summary>
        public const string DefaultAnimePic = "/images/6c573befgw1fabxbznz9sj20qo0xcdk4.jpg";
        public const string OneLineStr = "???/0???";

#if DEBUG
        public const string DBStr = "Data Source=BangumiProject.db";
        public const string StaticFiles_css = "/lib/materialize/css/materialize.min.css";
        public const string StaticFiles_JS = "/lib/materialize/js/materialize.min.js";
#else
        public const string DBStr = "Data Source="+ DateBasePath + "BangumiProject.db";
        public const string StaticFiles_css = "https://files.lilys.moe/public/lib/materialize/css/materialize.min.css";
        public const string StaticFiles_JS = "https://files.lilys.moe/public/lib/materialize/js/materialize.min.js";
#endif
        public const string DBStr_MoeMushi = "Data Source=" + DateBasePath + "MoeMushi.db";
        /*
         * ====================================================
         * 以下是权限
         * ====================================================
         */

        /// <summary>
        /// 管理员
        /// </summary>
        public const string Yuri_Admin = "Admin,";
        /// <summary>
        /// 仅次于管理员
        /// </summary>
        public const string Yuri_Girl = "Girl,";
        /// <summary>
        /// 一般权限5
        /// </summary>
        public const string Yuri_Yuri5 = "Yuri5,";
        /// <summary>
        /// 一般权限4
        /// </summary>
        public const string Yuri_Yuri4 = "Yuri4,";
        /// <summary>
        /// 一般权限3
        /// </summary>
        public const string Yuri_Yuri3 = "Yuri3,";
        /// <summary>
        /// 一般权限2
        /// </summary>
        public const string Yuri_Yuri2 = "Yuri2,";
        /// <summary>
        /// 一般权限1
        /// </summary>
        public const string Yuri_Yuri1 = "Yuri1,";
        /// <summary>
        /// 猪狗不如权限
        /// </summary>
        public const string Yuri_Boy = "Boy,";

        /*
         * ====================================================
         * 以下是路由
         * ====================================================
         */

        public const string Route_Profile_UID = "Profile_Uid";
        public const string Route_Profile = "Profile";
        public const string Route_Test = "Test";

        /*
         * ====================================================
         * 以下是阿里云OSS的文件使用
         * ====================================================
         */
        public const string AliyunOssPublic = "public/";
        public const string AliyunOssPrivate = "private/";
        /// <summary>
        /// yourBucketName
        /// </summary>
        public const string BucketName = "lilys-moe-hk";

#if DEBUG
        /// <summary>
        /// yourEndpoint
        /// </summary>
        public const string Endpoint = "files.lilys.moe";
        public const string AliyunConfig = @"E:\TEST\AliyunConfig.conf";
#else
        /// <summary>
        /// yourEndpoint
        /// 这个是内网，正式编译时使用
        /// </summary>
        public const string Endpoint = "oss-cn-hongkong-internal.aliyuncs.com";
        public const string AliyunConfig = "/home/BangumiFiles/AliyunConfig.conf";
#endif

        /// <summary>
        /// yourAccessKeyId
        /// </summary>
        public static string AccessKeyID = string.Empty;
        /// <summary>
        /// yourAccessKeySecret
        /// </summary>
        public static string AccessKeySecret = string.Empty;
        /*
         * ====================================================
         * 以下是阿里云OSS的文件使用
         * ====================================================
         */
        /// <summary>
        /// 所有的动画（不包含任何List数据）
        /// </summary>
        public const string Cache_AllAnime = "AllAnime";
        /// <summary>
        /// 所有的动画标签
        /// </summary>
        public const string Cache_AllAnimeTags = "AllAnimeTags";
        /// <summary>
        /// 获取所有的动画的年份
        /// </summary>
        public const string Cache_AllAnimeYear = "AllAnimeYears";
    }

    /// <summary>
    /// 这个是判断是否已经有了管理员，第一个创建的用户即为管理员
    /// </summary>
    public class Common
    {
        public static bool HasAdmin { get; set; }
    }
}