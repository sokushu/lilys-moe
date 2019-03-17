using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Files.Process.AliyunOSS
{
    public class FileUpLoad
    {
        // 创建OssClient实例。
        private readonly OssClient Client = Config.client;
        private readonly AutoResetEvent _event = Config._event;
        /// <summary>
        /// 
        /// </summary>
        public FileUpLoad()
        {
            
        }

        /// <summary>
        /// 上传文件到OSS实例
        /// 
        /// 国产游戏 ≠ 差游戏
        /// 国产游戏 ≠ 抄袭
        /// 国产游戏 ≠ 圈钱
        /// 国产游戏 ≠ 昧良心
        /// 可能会抛出异常
        /// </summary>
        public PutObjectResult AliyunFileUpLoad(IFormFile file, bool IsPublic)
        {
            using (Stream FileStream = file.OpenReadStream())
            {
                //获取文件名
                var FileName = CreateFileName(file.FileName, IsPublic);
                //上传文件
                return Client.PutObject(Final.BucketName, FileName, FileStream);
            }
        }

        /// <summary>
        /// 上传文件到OSS实例
        /// 
        /// 可能会抛出异常
        /// </summary>
        public PutObjectResult AliyunFileUpLoad(Stream stream, string FileName)
        {
            using (stream)
            {
                return Client.PutObject(
                bucketName: Final.BucketName,
                key: FileName,
                content: stream
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="FileName"></param>
        public void AliyunFileUpLoadAsync(Stream stream, string FileName)
        {
            using (stream)
            {
                // 增加自定义元信息。
                var metadata = new ObjectMetadata();

                Client.BeginPutObject(
                    bucketName: Final.BucketName,
                    key: FileName,
                    content: stream,
                    callback: AliyunFileUpLoadCallBack,
                    state: "OK"
                    );

                _event.WaitOne();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private void AliyunFileUpLoadCallBack(IAsyncResult ar)
        {
            try
            {
                Client.EndPutObject(ar);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.Error.WriteLine($"******************{ex.Message}************************");
#endif
            }
            finally
            {
                _event.Set();
            }
            
        }

        /// <summary>
        /// 创建文件key
        /// 上传的文件的路径
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="IsPublic">是否文件公开读取</param>
        /// <returns></returns>
        public string CreateFileName(string FileName, bool IsPublic)
        {
            return IsPublic ? $"{Final.AliyunOssPublic}{FileName}" : $"{Final.AliyunOssPrivate}{FileName}";
        }
    }
}
