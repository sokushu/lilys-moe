using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Files.Process.AliyunOSS
{
    public class AliyunImageProcess
    {
        // 创建OssClient实例。
        private readonly OssClient Client = Config.client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Process"></param>
        /// <returns></returns>
        public OssObject Process(string FileName, string Process)
        {
            try
            {
                return Client.GetObject(new GetObjectRequest(Final.BucketName, FileName, Process));
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
