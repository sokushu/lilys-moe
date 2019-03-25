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
        /// <summary>
        /// 要生成的Model的名字
        /// </summary>
        public string PageModelName { get; }

        /// <summary>
        /// 
        /// </summary>
        private ISender Sender { get; set; }

        private bool Send { get; set; }

        /// <summary>
        /// 存储字段名，和数据的
        /// </summary>
        protected Dictionary<string, object> ValuePairs { get; } = new Dictionary<string, object>();

        /// <summary>
        /// 存储Model类中的字段信息
        /// </summary>
        private Dictionary<string, PropertyInfo> PropertyInfos { get; } = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="PageModelName">要生成的Model的名字</param>
        public ModelStream(string PageModelName)
        {
            if (PageModelName == null || PageModelName.Length == 0)
            {
                this.PageModelName = typeof(PageModel).Name;
            }
            else
            {
                this.PageModelName = PageModelName;
            }
            bool HasKey = CoreSettingAndData.PropertyInfos.ContainsKey(PageModelName);
            if (!HasKey)
            {
                BuildMap.Build<PageModel>();
            }
            foreach (var item in CoreSettingAndData.PropertyInfos[PageModelName])
            {
                PropertyInfos.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 不加载字段信息的初始化
        /// </summary>
        public ModelStream(){ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public void SetSender(ISender sender)
        {
            if (sender != null)
            {
                Sender = sender;
                Send = true;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 构建数据的方法
        /// </summary>
        /// <returns></returns>
        public abstract PageModel Build();

        /// <summary>
        /// 用于加载字段数据
        /// </summary>
        /// <typeparam name="ProModel"></typeparam>
        /// <param name="modelLoader"></param>
        public void SetModelLoader<ProModel>(IModelLoader<ProModel> modelLoader)
        {
            var Value = modelLoader.Load();
            Value = modelLoader.AfterProcess(Value);
            foreach (var item in modelLoader.PropertiesName)
            {
                ValuePairs[item] = Value;
            }
            if (Send)
            {
                Sender.Send(Value);
            }
        }
    }
}
