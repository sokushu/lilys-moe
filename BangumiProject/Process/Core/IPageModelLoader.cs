using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public interface IPageModelLoader
    {
        void SetModelStream<Model>(ModelStream<Model> modelStream);

        void SetModel<Model>(Model model);

        Tuple<T> BuildPageData<T>();
        Tuple<T, T1> BuildPageData<T, T1>();
        Tuple<T, T1, T2> BuildPageData<T, T1, T2>();
        Tuple<T, T1, T2, T3> BuildPageData<T, T1, T2, T3>();
        Tuple<T, T1, T2, T3, T4> BuildPageData<T, T1, T2, T3, T4>();
        Tuple<T, T1, T2, T3, T4, T5> BuildPageData<T, T1, T2, T3, T4, T5>();
        Tuple<T, T1, T2, T3, T4, T5, T6> BuildPageData<T, T1, T2, T3, T4, T5, T6>();
    }
}
