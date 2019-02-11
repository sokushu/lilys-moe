using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Common.File
{
    public class AliyunDownload
    {
        // 创建OssClient实例。
        private readonly OssClient Client = AliyunKeyConfig.client;

        /// <summary>
        /// 获取阿里OSS的链接
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string GetFileURL(string FileName)
        {
            //生成下载URL
            var req = new GeneratePresignedUriRequest(Final.BucketName, FileName, SignHttpMethod.Get)
            {
                //设置访问链接失效时间20分钟
                Expiration = DateTime.Now.AddMinutes(20)
            };
            //创建链接
            var uri = Client.GeneratePresignedUri(req);
            UriBuilder uriBuilder = new UriBuilder(uri)
            {
                Scheme = "https",
                Port = 443
            };
            //将连接转换为string
            return uriBuilder.Uri.ToString();
        }
    }
}
