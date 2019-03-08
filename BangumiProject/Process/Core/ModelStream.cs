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

        protected List<object> values = new List<object>(); 
        public ModelStream() { }

        public virtual void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            object value = modelLoader.BuildModel();
            values.Add(value);
        }

        public abstract PageModel Build();
    }
}
