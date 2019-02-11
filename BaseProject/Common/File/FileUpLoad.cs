using BaseProject.Common.Exception;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaseProject.Common.File
{
    public static class FileUpLoad
    {
        private static HashSet<string> MIMETYPE = new HashSet<string>
        {
            "image/jpeg",
            "image/gif",
            "image/bmp",
            "image/png"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        public static async System.Threading.Tasks.Task FileUpAsync(IFormFile formFile, ICollection<string> UserID)
        {
            if (formFile != null)
            {
                string Type = formFile.ContentType;
                string FileName = formFile.FileName;
                if (MIMETYPE.Contains(Type))
                {
                    // 进行文件名处理,得到文件名
                    FileName = CreateFileName(FileName);
                    // 生成各个文件的路径
                    var FilePath_Image = $"{Final.FilePath_Image}{FileName}";               //大缩略图保存
                    var FilePath_Image_Thumb = $"{Final.FilePath_Image_Thumb}{FileName}";   //缩略图保存位置

                    string TrueFileName;//阿里云OSS的文件Key（包含文件夹的）
                    var IsPublic = false;//是否是公开读取的文件（默认不是）
                    try
                    {
                        using 
                            (
                            Stream stream_img = new FileStream(FilePath_Image, FileMode.Create),
                            stream_img_thumb = new FileStream(FilePath_Image_Thumb, FileMode.Create),
                            stream = formFile.OpenReadStream()
                            )
                        {
                            AliyunUpLoad aliyunUpLoad = new AliyunUpLoad();
                            AliyunImageProcess aliyunImageProcess = new AliyunImageProcess();

                            TrueFileName = aliyunUpLoad.CreateFileName(FileName, IsPublic);
                            //上传到阿里云OSS
                            aliyunUpLoad.AliyunFileUpLoad(stream, TrueFileName);
                            //下载缩略图
                            var Process = "image/auto-orient,1/resize,m_fill,w_400,h_400/quality,q_80/format,jpg";
                            var ossObject = aliyunImageProcess.Process(TrueFileName, Process);
                            await ossObject.Content.CopyToAsync(stream_img_thumb);
                            //下载缩略图的大图
                            var ProcessBig = "image/auto-orient,1/resize,m_lfit,w_600/quality,q_90/format,jpg";
                            var ossObjectBig = aliyunImageProcess.Process(TrueFileName, ProcessBig);
                            await ossObjectBig.Content.CopyToAsync(stream_img);
                        }
                    }
                    catch
                    {
                        //上传文件一般不会发生异常，阿里云的服务器，也不需要对文件类型进行检查了
                        //万一出现异常情况的话，
                        //就删除创建的缩略图文件
                        System.IO.File.Delete(FilePath_Image);                  // 删除源文件
                        System.IO.File.Delete(FilePath_Image_Thumb);            // 删除预览图文件

                        throw new System.Exception("发生错误，请稍后重试");     // 抛出异常，到异常页面
                    }
                    finally
                    {

                    }
                }
                throw new FileTypeErrorException("不支持的文件类型！");
            }
            throw new NullReferenceException("文件为空");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static string CreateFileName(string Name)
        {
            // 进行文件名处理
            string FileName = null;     // 处理之后的真的文件名
            string SFileName = string.Empty;    // 文件的扩展名

            var FileName1 = Name;     // 用户上传的文件名

            int Not = 0;
            if ((Not = FileName1.LastIndexOf('.')) > -1)    //看看用户是否上传了没有后缀名的奇葩文件
            {
                SFileName = FileName1.Substring(Not);
            }
            else
            {
                // 奇葩文件——要我命3000
                // 不管他了
            }
            // 防止重名用随机生成 
            var FileName0 = Guid.NewGuid().ToString("N");
            if (CheckLen($"{FileName0}{FileName1}", 200))  //文件名有点长
            {
                // 直接随即文件名 + 扩展名
                FileName = FileName0 + SFileName;
            }
            else
            {
                // 随机 + 原始文件名
                FileName = FileName0 + FileName1;
            }
            return FileName;
        }

        /// <summary>
        /// 检查字符串的长度
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        private static bool CheckLen(string Input, int Len)
        {
            return Input.Length > Len;
        }
    }
}
