using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
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
    public abstract class ModelStream<PageModel>
    {
        private PageModel PageModels { get; set; }

        /// <summary>
        /// 存储SetModelLoader方法加载过的数据
        /// </summary>
        protected List<object> values = new List<object>();
        public ModelStream() { }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelLoader"></param>
        public virtual void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            object value = modelLoader.BuildModel();
            values.Add(value);
        }

        /// <summary>
        /// 用于定义构建方式
        /// </summary>
        /// <returns></returns>
        public abstract PageModel Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piont"></param>
        /// <returns></returns>
        protected T Get<T>(int piont)
        {
            if (piont < 0)
                return default(T);
            int len = values.Count - 1;
            if (piont <= len)
            {
                return (T)values[piont];
            }
            return default(T);
        }
    }
}
