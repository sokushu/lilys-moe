using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    /// <summary>
    /// 加载器的最小单位，用于加载最小的属性
    /// 例如：Page类
    /// class T
    ///     string title；
    ///     Anime anime；
    /// 作用就是用于单独加载title，anime属性的
    /// </summary>
    /// <typeparam name="Model">返回的类型</typeparam>
    public abstract class IModelLoader<Model>
    {
        public string[] PropertiesName { get; }

        private object[] Input { get; set; } = null;
        public IModelLoader(params string[] PropertiesName)
        {
            this.PropertiesName = PropertiesName;
        }
        /// <summary>
        /// 从数据库或文件系统中加载数据
        /// </summary>
        /// <returns></returns>
        public abstract Model LoadModel(params object[] Input);

        /// <summary>
        /// 加载数据之后进行处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract Model AfterProcess(Model model);

        public IModelLoader<Model> SetParams(params object[] Input)
        {
            this.Input = Input;
            return this;
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public Model BuildModel()
        {
            if (Input == null)
            {
                return AfterProcess(LoadModel());
            }
            else
            {
                return AfterProcess(LoadModel(Input));
            }
            
        }
    }
}
