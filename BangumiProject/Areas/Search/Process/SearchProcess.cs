using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;

namespace BangumiProject.Areas.Search.Process
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <see cref="https://www.cnblogs.com/zjoch/p/4467909.html"/>
    public class SearchProcess
    {
        public void Run()
        {
            Directory directory = FSDirectory.Open(@"");
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);


        }
    }
}
