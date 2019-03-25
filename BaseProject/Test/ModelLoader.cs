using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ModelLoader<Model> : IModelLoader<Model>
    {
        /// <summary>
        /// 要填充的字段名
        /// </summary>
        public string[] PropertiesName { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="PropertiesName">要填充的字段名</param>
        public ModelLoader(params string[] PropertiesName)
        {
            if (PropertiesName == null || PropertiesName.Length == 0)
            {
                PropertiesName = new string[1] { typeof(Model).Name };
            }
            else
            {
                this.PropertiesName = PropertiesName;
            }
            
        }

        /// <summary>
        /// 加载数据之后的处理
        /// </summary>
        /// <param name="model">加载的数据</param>
        /// <returns>处理完的数据</returns>
        public abstract Model AfterProcess(Model model);
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns>加载的数据</returns>
        public abstract Model Load();
    }
}
