using BangumiProject.Areas.Error.Models;
using BangumiProject.Areas.Files.Process.AliyunOSS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Files.Process
{
    public class FileUpLoadProcess
    {
//        public Images Images { get; private set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="File">上传的文件</param>
//        /// <param name="ReadUser">上传文件谁可以读取？</param>
//        /// <returns></returns>
//        public async Task<ReturnType> FileUpLoadToAliyunOSSAsync(IFormFile File, User User)
//        {
//            if (File == null)
//            {
//                throw new ErrorException(1, "文件为空");
//            }
//            IPicProcess PicProcess = new ImageProcessTwo();

//            //这次文件保存使用了阿里云OSS，所以，
//            //主机内只保存缩略图即可，只有用户需要大图时，才将完整图片链接显示出来

//            //检查文件类型
//            string Type = File.ContentType;
//            if (PicProcess.FileTypeCheck(Type))
//            {
//                // 进行文件名处理,得到文件名
//                string FileName = PicProcess.CreateFileName(File.FileName);
//                // 生成各个文件的路径
//                var FilePath_Image = Final.FilePath_Image + FileName;                           //大缩略图保存
//                var FilePath_Image_Thumb = Final.FilePath_Image_Thumb + FileName;               //缩略图保存位置

//                string TrueFileName;//阿里云OSS的文件Key（包含文件夹的）
//                var IsPublic = false;//是否是公开读取的文件（默认不是）
//                try
//                {
//                    using
//                        (
//                        Stream stream_img = new FileStream(FilePath_Image, FileMode.Create),
//                        stream_img_thumb = new FileStream(FilePath_Image_Thumb, FileMode.Create),
//                        stream = File.OpenReadStream()
//                        )
//                    {
//                        FileUpLoad fileUpLoad = new FileUpLoad();
//                        AliyunImageProcess aliyunImageProcess = new AliyunImageProcess();

//                        TrueFileName = fileUpLoad.CreateFileName(FileName, IsPublic);
//                        //上传到阿里云OSS
//                        fileUpLoad.AliyunFileUpLoad(stream, TrueFileName);
//                        //下载缩略图
//                        var Process = "image/auto-orient,1/resize,m_fill,w_400,h_400/quality,q_80/format,jpg";
//                        var ossObject = aliyunImageProcess.Process(TrueFileName, Process);
//                        await ossObject.Content.CopyToAsync(stream_img_thumb);
//                        //下载缩略图的大图
//                        var ProcessBig = "image/auto-orient,1/resize,m_lfit,w_600/quality,q_90/format,jpg";
//                        var ossObjectBig = aliyunImageProcess.Process(TrueFileName, ProcessBig);
//                        await ossObjectBig.Content.CopyToAsync(stream_img);
//                    }

//                    MoeTools moeTools = new MoeTools();
//                    // 生成Image数据
//                    var ReadUsers = moeTools.GetImage_ReadUsers(Public: false, Users: User.Id);

//                    Images Image = new Images
//                    {
//                        ImageName = File.FileName,
//                        ImagePath = FilePath_Image,//实际大图缩略图的存储位置
//                        ReadUsers = ReadUsers,
//                        UpLoadUsers = User,
//                        ContentType = Type,
//                        StaticPath = TrueFileName//阿里云Oss的存储路径（key）
//                    };

//                    Images = Image;

//                    return ReturnType.FileUpLoadOK;
//                }
//                catch (Exception e)
//                {
//#if DEBUG
//                    Console.WriteLine(e);
//#endif
//                    //上传文件一般不会发生异常，阿里云的服务器，也不需要对文件类型进行检查了
//                    //万一出现异常情况的话，
//                    //就删除创建的缩略图文件
//                    System.IO.File.Delete(FilePath_Image);                  // 删除源文件
//                    System.IO.File.Delete(FilePath_Image_Thumb);            // 删除预览图文件

//                    throw new ErrorException(2, "发生错误，请稍后重试");       // 抛出异常，到异常页面
//                }
//            }
//            else
//            {
//                return ReturnType.AdminLog;
//            }
//        }

//        public async Task<ReturnType> AdmingLogAsync(bool IsAdmin, IFormFile File)
//        {
//            //支援Log版本文件的上传
//            //检查用户，如果是管理员，放行
//            if (IsAdmin)
//            {
//                //目前只支持接收文本文件（因为是我本人操作，懒得文件认证了）
//                FileMode fileMode;
//                if (System.IO.File.Exists(Final.FilePath_VersionLog))
//                {
//                    fileMode = FileMode.Append;
//                }
//                else
//                {
//                    fileMode = FileMode.OpenOrCreate;
//                }

//                using (var fileStream = new FileStream(Final.FilePath_VersionLog, fileMode))
//                {
//                    await File.CopyToAsync(fileStream);
//                    await fileStream.FlushAsync();
//                }

//                return ReturnType.FileUpLoadOK;
//            }
//            else
//            {
//                //上传的不是图片
//                throw new ErrorException(2, "您上传的不是图片，或是本站不支持的图片，请重新上传");
//            }
//        }
    }
    public enum ReturnType
    {
        AdminLog,
        FileUpLoadOK,

    }
}
