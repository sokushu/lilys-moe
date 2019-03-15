using BaseProject.Core;
using BaseProject.Process;
using NUnit.Framework;
using NUnitTestProject.BaseProjectTest.Model;
using NUnitTestProject.BaseProjectTest.ModelLoader;
using NUnitTestProject.BaseProjectTest.ModelStream;
using NUnitTestProject.BaseProjectTest.PageSwitch;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject
{
    public class BaseProjectMainTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test01()
        {
            var now0 = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
            BuildMap.Build<TestModel>();
            StringLoader stringLoader = new StringLoader();
            BoolLoader boolLoader = new BoolLoader();
            IntLoader intLoader = new IntLoader();
            TimeLoader timeLoader = new TimeLoader();
            var now1 = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");

            CorePageLoader corePageLoader = new CorePageLoader();
            corePageLoader.SetSwitchPage(new Page(true));

            TestModelMakeStream modelMakeStream = new TestModelMakeStream();
            modelMakeStream.SetModelLoader(stringLoader);
            modelMakeStream.SetModelLoader(boolLoader);
            modelMakeStream.SetModelLoader(intLoader);
            modelMakeStream.SetModelLoader(timeLoader);

            corePageLoader.SetModelStream(modelMakeStream);

            var Test = corePageLoader.Build<TestModel>();
            var now2 = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
            var now3 = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
        }
    }
    
}
