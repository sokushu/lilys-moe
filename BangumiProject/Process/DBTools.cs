using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process
{
    /// <summary>
    /// 解决一下生产数据库和测试用数据库表不同步的问题
    /// </summary>
    public class DBTools
    {
        private readonly BangumiProjectContext _DB;
        public DBTools(BangumiProjectContext _DB)
        {
            this._DB = _DB;
        }

        public void Go()
        {

        }
    }
}
