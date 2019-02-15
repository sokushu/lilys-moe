using BangumiProject.Areas.Admin.Process;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject
{
    public class Test2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test01()
        {
            AdminSettingWriteAndRead adminSettingWriteAndRead = new AdminSettingWriteAndRead();
            adminSettingWriteAndRead.Write(
                new BangumiProject.Areas.Admin.Models.AdminSetting
                {
                    IsShowTopPic = true,
                    PicPath = "http://www.google.com/"
                }
                );
            var aa = adminSettingWriteAndRead.Read();
        }
    }
}
