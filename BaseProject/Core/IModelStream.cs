using BaseProject.Process;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BaseProject.Core
{
    /// <summary>
    /// 用于构建Page类
    /// 例如：Page类
    /// class T
    ///     string title；
    ///     Anime anime；
    /// 作用就是用于获取ModelLoader类的字段数据，并构建Page类
    /// </summary>
    /// <typeparam name="PageModel"></typeparam>
    public abstract class IModelStream<PageModel> where PageModel : new()
    {
        protected Dictionary<string, PropertyInfo> PropertyInfos { get; } = new Dictionary<string, PropertyInfo>();

        private string ModelClassName { get; }
        /// <summary>
        /// 存储SetModelLoader方法加载过的数据
        /// </summary>
        protected Dictionary<string, object> values = new Dictionary<string, object>();

        protected HashSet<string> StopString { get; } = new HashSet<string>();

        public IModelStream(string ModelClassName)
        {
            this.ModelClassName = ModelClassName;
            bool HasKey = CoreSettingAndData.PropertyInfos.ContainsKey(ModelClassName);
            if (HasKey)
            {
                InitPropertyInfo();
            }
            else
            {
                BuildMap.Build<PageModel>();
                InitPropertyInfo();
            }
        }
        private void InitPropertyInfo()
        {
            foreach (var item in CoreSettingAndData.PropertyInfos[ModelClassName])
            {
                PropertyInfos.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelLoader"></param>
        public virtual void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            T value = modelLoader.BuildModel();
            values.Add(modelLoader.PropertiesName, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void BuildRulu();

        protected void Stop(string StopProperties)
        {
            StopString.Add(StopProperties);
        }
        protected void Stop()
        {
            foreach (var item in PropertyInfos.Keys)
            {
                StopString.Add(item);
            }
        }
        protected void Open(string OpenProperties)
        {
            bool HasKey = StopString.Contains(OpenProperties);
            if (HasKey)
            {
                StopString.Remove(OpenProperties);
            }
        }
        protected void Open()
        {
            StopString.Clear();
        }

        /// <summary>
        /// 用于定义构建方式
        /// </summary>
        /// <returns></returns>
        public PageModel Build()
        {
            BuildRulu();
            PageModel model = new PageModel();
            foreach (var item in values)
            {
                if (!StopString.Contains(item.Key))
                {
                    var ProInfo = PropertyInfos[item.Key];
                    if (ProInfo != null)
                    {
                        ProInfo.SetValue(model, item.Value);
                    }
                }
            }
            return model;
        }
    }
}
