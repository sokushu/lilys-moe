using BaseProject.Common.Models;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jieba.NET;
using jieba;

namespace BangumiProject.Areas.Search.Process
{
    public class Search
    {
        /// <summary>
        /// 
        /// </summary>
        private static IndexSearcher indexSearcher;

        /// <summary>
        /// 
        /// </summary>
        public Search()
        {
            if (indexSearcher == null)
            {
                lock (this)
                {
                    if (indexSearcher == null)
                    {
                        //创建Directory
                        Directory directory = FSDirectory.Open(Final.Search_Index);
                        DirectoryReader directoryReader = DirectoryReader.Open(directory);
                        indexSearcher = new IndexSearcher(directoryReader);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyWord"></param>
        public void SearchAnime(string KeyWord)
        {
            //使用分词器
            Analyzer1 analyzer = new Analyzer1(JiebaNet.Segmenter.TokenizerMode.Search);
            Query query = new FuzzyQuery(new Term("AnimeName", KeyWord));

            TopDocs topDocs = indexSearcher.Search(query, 50);

            int a = topDocs.TotalHits;

            foreach (var item in topDocs.ScoreDocs)
            {
                Document doc = indexSearcher.Doc(item.Doc);

                string AnimeName = doc.Get("AnimeName");
            }
        }
    }
}
