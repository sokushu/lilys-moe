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
using BangumiProject.Areas.Admin.Process;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //��ѯ��ʽ 	����
        //TermQuery 	��ȷ��ѯ
        //TermRangeQuery 	��ѯһ����Χ
        //PrefixQuery 	ǰ׺ƥ���ѯ
        //WildcardQuery 	ͨ�����ѯ
        //BooleanQuery 	��������ѯ
        //PhraseQuery 	�����ѯ
        //FuzzyQuery 	ģ����ѯ
        //Queryparser 	���ܲ�ѯ������Ķ��������������ѯ����
        //--------------------- 
        //���ߣ���쭵�yellowcong 
        //��Դ��CSDN 
        //ԭ�ģ�https://blog.csdn.net/yelllowcong/article/details/78698506 
        //��Ȩ����������Ϊ����ԭ�����£�ת���븽�ϲ������ӣ�
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

            ////�Ϸ��Ĵ����Ǵ�������

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
            AdminSettingWriteAndRead adminSettingWriteAndRead = new AdminSettingWriteAndRead();
            adminSettingWriteAndRead.Write(new BangumiProject.Areas.Admin.Models.AdminSetting { IsShowTopPic = true });

            adminSettingWriteAndRead.Read();
        }
    }
}