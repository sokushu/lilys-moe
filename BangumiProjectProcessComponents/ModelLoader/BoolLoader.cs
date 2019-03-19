using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectProcessComponents.ModelLoader
{
    public class BoolLoader : IModelLoader<bool>
    {
        public BoolLoader(params string[] PropertiesName) : base(PropertiesName) { }
        public override bool AfterProcess(bool model)
        {
            return model;
        }

        /// <summary>
        /// 
        /// param : bool
        /// </summary>
        /// <param name="Input">bool</param>
        /// <returns></returns>
        public override bool LoadModel(params object[] Input)
        {
            return (bool)Input[0];
        }
    }
}
