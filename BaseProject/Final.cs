using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 为 .NET Core 设计一个 3D 图形渲染库
    /// <see cref="https://www.cnblogs.com/savorboard/p/designing-a-3d-rendering-library-for-net-core.html"/>
    /// 棒！使用.NET Core构建3D游戏引擎
    /// <see cref="https://www.cnblogs.com/savorboard/p/net-core-game-engine.html"/>
    /// </summary>
    public class Final
    {

        public const int StatusCode403 = 403;
        public const int StatusCode404 = 404;
        public const int StatusCodeBangumiNotFound = 10001;
        public const string Image_JPEG = "image/jpeg";
        /*
         * ====================================================
         * 以下是Session的Key
         * ====================================================
         */
        public const string YuriMode = nameof(YuriMode);



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
        public const string FilePath_DownLoad = @"E:\TEST\DownLoad\";
        public const string FilePath_DownLoad_Log = @"E:\TEST\DownLoad\Log\";

        /// <summary>
        /// 搜索引擎的索引库
        /// </summary>
        public const string Search_Index = @"E:\programme\test";
        public const string AdminSetting = @"E:\TEST\WebSetting.conf";
#else
        
        /// <summary>
        /// 文件存储地址
        /// </summary>
        public const string FilePath_DownLoad = "/home/BangumiFiles/FileDownLoad/";
        public const string FilePath_DownLoad_Log = "/home/BangumiFiles/FileDownLoad/Log/";
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
        public const string StaticFiles_css = "/lib/mdui-v0.4.2/css/mdui.css";
        public const string StaticFiles_JS = "/lib/mdui-v0.4.2/js/mdui.js";
#else
        public const string DBStr = "Data Source="+ DateBasePath + "BangumiProject.db";
        public const string StaticFiles_css = "https://files.lilys.moe/public/lib/mdui/css/mdui.min.css";
        public const string StaticFiles_JS = "https://files.lilys.moe/public/lib/mdui/js/mdui.min.js";
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
        public const string Route_Index =                   nameof(Route_Index);
        public const string Route_Profile_UID =             nameof(Route_Profile_UID);
        public const string Route_Profile =                 nameof(Route_Profile);
        public const string Route_Test =                    nameof(Route_Test);
        public const string Route_Bangumi_Create =          nameof(Route_Bangumi_Create);
        public const string Route_Bangumi_Create_POST =     nameof(Route_Bangumi_Create_POST);
        public const string Route_Bangumi_Index =           nameof(Route_Bangumi_Index);
        /// <summary>
        /// 查看一部详细的动画
        /// </summary>
        public const string Route_Bangumi_Details =         nameof(Route_Bangumi_Details);
        public const string Route_Bangumi_Edit =            nameof(Route_Bangumi_Edit);
        public const string Route_Bangumi_Edit_POST =       nameof(Route_Bangumi_Edit_POST);
        public const string Route_Bangumi_Delete =          nameof(Route_Bangumi_Delete);
        public const string Route_Bangumi_Delete_POST =     nameof(Route_Bangumi_Delete_POST);
        public const string Route_Video_Index =             nameof(Route_Video_Index);
        public const string Route_Video_Details =           nameof(Route_Video_Details);
        public const string Route_Video_Create =            nameof(Route_Video_Create);
        public const string Route_Video_Create_POST =       nameof(Route_Video_Create_POST);
        public const string Route_Video_Edit =              nameof(Route_Video_Edit);
        public const string Route_Video_Edit_POST =         nameof(Route_Video_Edit_POST);
        public const string Route_Video_Delete =            nameof(Route_Video_Delete);
        public const string Route_Video_Delete_POST =       nameof(Route_Video_Delete_POST);
        public const string Route_Files_Index =             nameof(Route_Files_Index);
        public const string Route_Files_Details =           nameof(Route_Files_Details);
        public const string Route_Files_Create_POST =       nameof(Route_Files_Create_POST);
        public const string Route_PlaySouce_Details =       nameof(Route_PlaySouce_Details);
        public const string Route_PlaySouce_Create_POST =   nameof(Route_PlaySouce_Create_POST);
        public const string Route_PlaySouce_Create =        nameof(Route_PlaySouce_Create);
        public const string Route_BangumiMemo_Create_POST = nameof(Route_BangumiMemo_Create_POST);
        public const string Route_BangumiMemo_Create =      nameof(Route_BangumiMemo_Create);
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
