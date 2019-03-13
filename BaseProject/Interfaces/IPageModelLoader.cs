using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface IPageModelLoader
    {
        void SetModelStream<Model>(IModelStream<Model> modelStream) where Model : new();

        void SetModel<Model>(Model model);

        Tuple<T> Build<T>();
        Tuple<T, T1> Build<T, T1>();
        Tuple<T, T1, T2> Build<T, T1, T2>();
        Tuple<T, T1, T2, T3> Build<T, T1, T2, T3>();
        Tuple<T, T1, T2, T3, T4> Build<T, T1, T2, T3, T4>();
        Tuple<T, T1, T2, T3, T4, T5> Build<T, T1, T2, T3, T4, T5>();
        Tuple<T, T1, T2, T3, T4, T5, T6> Build<T, T1, T2, T3, T4, T5, T6>();
    }
}
