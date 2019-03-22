using BaseProject.Interfaces;
using BaseProject.Process;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BaseProject.Test
{
    public abstract class ModelStream<PageModel> : IModelStream<PageModel>
    {
        protected Dictionary<string, PropertyInfo> PropertyInfos { get; } = new Dictionary<string, PropertyInfo>();

        private string ModelClassName { get; set; }
        /// <summary>
        /// 存储SetModelLoader方法加载过的数据
        /// </summary>
        protected Dictionary<string, object> values = new Dictionary<string, object>();

        private ISender Sender { get; set; } = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ModelClassName">想要构建的实体类类名，方便读取</param>
        public ModelStream(string ModelClassName)
        {
            Init(ModelClassName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void SetSender(ISender sender)
        {
            this.Sender = sender;
        }

        /// <summary>
        /// 初始化加载PageModel的信息
        /// </summary>
        private void InitPropertyInfo()
        {
            foreach (var item in CoreSettingAndData.PropertyInfos[ModelClassName])
            {
                PropertyInfos.Add(item.Name, item);
            }
        }
        private void Init(string ModelClassName)
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

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelLoader"></param>
        public virtual void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            T value = modelLoader.Load();
            value = modelLoader.AfterProcess(value);
            foreach (var item in modelLoader.PropertiesName)
            {
                values[item] = value;
            }
        }

        /// <summary>
        /// 加载数据后，
        /// 进行一定的处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Return">要返回的类型</typeparam>
        /// <param name="modelLoader"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public virtual Return SetModelLoader<T, Return>(IModelLoader<T> modelLoader, IProcess<Return, T> process)
        {
            T value = modelLoader.Load();
            value = modelLoader.AfterProcess(value);
            Return ReturnValue = process.Process(value);
            if (Sender != null)
            {
                Sender.Send(ReturnValue);
            }
            foreach (var item in modelLoader.PropertiesName)
            {
                values[item] = value;
            }
            return ReturnValue;
        }

        /// <summary>
        /// 用于定义构建方式
        /// </summary>
        /// <returns></returns>
        public abstract PageModel Build();
    }
}
