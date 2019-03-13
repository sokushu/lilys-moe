using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using NUnit.Framework;
using System;
using Lucene.Net.Analysis.Miscellaneous;
using static Lucene.Net.Documents.Field;
using Lucene.Net;
using Lucene;
using Lucene.Net.Search.Payloads;
using Lucene.Net.Search.Similarities;
using Lucene.Net.Search.Spans;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //查询方式 	意襾E
        //TermQuery 	精确查询
        //TermRangeQuery 	查询一个范围
        //PrefixQuery 	前缀匹配查询
        //WildcardQuery 	通配符查询
        //BooleanQuery 	多条件查询
        //PhraseQuery 	短觼E檠�
        //FuzzyQuery 	模糊查询
        //Queryparser 	万能查询（上面的都可以用这个来查询到）
        //--------------------- 
        //作者：狂飙的yellowcong 
        //来源：CSDN 
        //原文：https://blog.csdn.net/yelllowcong/article/details/78698506 
        //版权声明：本文为博主原创文章，转载莵E缴喜┪牧唇樱�
        [Test]
        public void Test1()
        {
            //Directory directory = FSDirectory.Open(@"E:\programme\test");
            //Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
            //IndexWriterConfig config = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
            //IndexWriter indexWriter = new IndexWriter(directory, config);
            //Document document = new Document();
            //Field fileNameField = new TextField("fileName", "TEST", Store.YES);
            //Field fileSizeField = new DoubleField("T", 33.65, Store.YES);
            //document.Add(fileNameField);
            //document.Add(fileSizeField);
            //indexWriter.AddDocument(document);
            //indexWriter.Commit();

            ////上方的代聛E谴唇ㄋ饕�

            //IndexReader indexReader = DirectoryReader.Open(directory);
            //IndexSearcher indexSearcher = new IndexSearcher(indexReader);
            //PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(analyzer);
            //Query query;
            //query = new WildcardQuery(new Term(""));
            //query = new PhraseQuery();
            //query = new PrefixQuery(new Term(""));
            //query = new MultiPhraseQuery();
            //query = new FuzzyQuery(new Term(""));
            //query = new RegexpQuery(new Term(""));
            //query = new TermRangeQuery(null,null,null,true,true);
            //query = new ConstantScoreQuery(query);
            //query = new DisjunctionMaxQuery(3);
            //TopDocs topDocs = indexSearcher.Search(query, 10);
            //Console.WriteLine(topDocs.TotalHits);
            //foreach (var item in topDocs.ScoreDocs)
            //{
            //    Document fields = indexSearcher.Doc(item.Doc);
            //}
            
        }

        [Test]
        public void Test2()
        {
            //AdminSettingWriteAndRead adminSettingWriteAndRead = new AdminSettingWriteAndRead();
            //adminSettingWriteAndRead.Write(new BangumiProject.Areas.Admin.Models.AdminSetting { IsShowTopPic = true });

            //adminSettingWriteAndRead.Read();
        }
    }
}