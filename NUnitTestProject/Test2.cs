using BaseProject.Core;
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
            CorePageLoader corePageLoader = new CorePageLoader();

            AnimeInfoModelStream animeInfoModelStream = new AnimeInfoModelStream(IsSignIn:true);
            animeInfoModelStream.SetModelLoader(new TestModelLoader());
            animeInfoModelStream.SetModelLoader(new TestModelBoolLoader());
            corePageLoader.SetModelStream(animeInfoModelStream);

            var Model = corePageLoader.Build<Model>();
        }
    }

    public class AnimeInfoModelStream : IModelStream<Model>
    {
        private bool IsSignIn { get; set; }
        public AnimeInfoModelStream(bool IsSignIn) : base("Model")
        {
            this.IsSignIn = IsSignIn;
        }

        public override void BuildRulu()
        {
            Stop();
            if (IsSignIn)
            {
                Open("IsSignIn");
                Open("AAA");
            }
        }
    }

    public class TestModelLoader : IModelLoader<string>
    {
        public TestModelLoader() : base("AAA") { }
        public override string AfterProcess(string model)
        {
            return model;
        }

        public override string LoadModel()
        {
            return "1Hello";
        }
    }

    public class TestModelBoolLoader : IModelLoader<bool>
    {
        public TestModelBoolLoader() : base("IsSignIn") { }
        public override bool AfterProcess(bool model)
        {
            return model;
        }

        public override bool LoadModel()
        {
            return true;
        }
    }

    public class Model
    {
        public string AAA { get; set; }

        public bool IsSignIn { get; set; }
    }
}
