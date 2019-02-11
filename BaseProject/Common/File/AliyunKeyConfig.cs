using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaseProject.Common.File
{
    public class AliyunKeyConfig
    {
        public static OssClient client { get; } = new OssClient(
            Final.Endpoint,
            Final.AccessKeyID,
            Final.AccessKeySecret,
            new ClientConfiguration
            {
                IsCname = true
            }
            );

        static AliyunKeyConfig()
        {
            if (System.IO.File.Exists(Final.AliyunConfig))
            {
                string[] ID = System.IO.File.ReadAllLines(Final.AliyunConfig);
                if (ID[0].Length < ID[1].Length)
                {
                    Final.AccessKeyID = ID[0];
                    Final.AccessKeySecret = ID[1];
                }
                else
                {
                    Final.AccessKeyID = ID[1];
                    Final.AccessKeySecret = ID[0];
                }
            }
            else
            {
                throw new System.Exception("没有阿里云的AccessKeyID和AccessKeySecret，将无法使用OSS服务");
            }
        }
        public static AutoResetEvent _event { get; } = new AutoResetEvent(false);
    }
}
