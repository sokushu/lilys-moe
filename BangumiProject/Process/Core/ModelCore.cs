using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public abstract class ModelCore<Model> : IModels<Model> where Model : class
    {
        private int HashCode { get; set; } = 0;
        public bool IsChange(object obj)
        {
            int MethodHashCode = obj == null ? 0 : obj.GetHashCode();
            if (HashCode != 0)
            {
                return HashCode != MethodHashCode;
            }
            HashCode = MethodHashCode;
            return false;
        }
        public void Clean()
        {
            HashCode = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        public ModelCore()
        {

        }

        public void Process(IProcess process, ref Model Model)
        {
            Process(process, null, ref Model);
        }

        public void Process(IProcess process, IProcessDoSome doSome, ref Model Model)
        {
            IsChange(Model);
            process.Process(ref Model);
            if (IsChange(Model))
            {
                if (doSome != null)
                {
                    doSome.Process();
                }
            }
        }

    }
}
