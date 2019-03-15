using BaseProject.Core;
using NUnitTestProject.BaseProjectTest.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.ModelStream
{
    public class TestModelMakeStream : IModelStream<TestModel>
    {
        public TestModelMakeStream() : base("TestModel") { }
        public override void BuildRulu()
        {
            Open();
            Stop("Name");
        }

        public override TestModel Build()
        {
            TestModel testModel = new TestModel
            {
                Name = (string)values["Name"],
                SignIn = (bool)values["SignIn"],
                Year = (int)values["Year"],
                Pic = (string)values["Pic"],
                DateTime = (DateTime)values["DateTime"]
            };
            return testModel;
        }
    }
}
