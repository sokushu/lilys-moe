using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class LoaderTest
    {
        public void MainRun()
        {

        }
    }

    public class Page : IPage<string>
    {
        public override string LoadPage(IModels models)
        {
            return "HelloModel";
        }
    }

    /// <summary>
    /// 页面处理程序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IPage<T>
    {
        /// <summary>
        /// 加载Model处理程序，返回处理后的Model
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public abstract T LoadPage(IModels models);

    }

    public interface IModels
    {
        
    }

    public class CorePageLoader
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IPage<T> Load<T>(IPage<T> page)
        {
            return page;
        }

        /// <summary>
        /// 生成Model数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static T GetTupleModels<T>(params object[] objs) where T : class
        {
            switch (objs.Length)
            {
                case 0:
                    return default(T);
                case 1:
                    return Tuple.Create(objs[0]) as T;
                case 2:
                    return Tuple.Create(objs[0], objs[1]) as T;
                case 3:
                    return Tuple.Create(objs[0], objs[1], objs[2]) as T;
                case 4:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3]) as T;
                case 5:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4]) as T;
                case 6:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4], objs[5]) as T;
                case 7:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4], objs[5], objs[6]) as T;
                default:
                    return default(T);
            }
        }
    }
}
