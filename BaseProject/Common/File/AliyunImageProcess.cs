using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Common.File
{
    public class AliyunImageProcess
    {
        // 创建OssClient实例。
        private readonly OssClient Client = AliyunKeyConfig.client;

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
            catch (System.Exception)
            {
                return null;
            }

        }
    }
}
