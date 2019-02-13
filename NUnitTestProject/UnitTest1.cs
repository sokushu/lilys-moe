using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using NUnit.Framework;
using static Lucene.Net.Documents.Field;
using System;
using Lucene.Net.Analysis.Miscellaneous;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Directory directory = FSDirectory.Open(@"E:\programme\test");
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
            IndexWriterConfig config = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
            IndexWriter indexWriter = new IndexWriter(directory, config);
            Document document = new Document();
            Field fileNameField = new TextField("fileName", "TEST", Store.YES);
            Field fileSizeField = new DoubleField("T", 33.65, Store.YES);
            document.Add(fileNameField);
            document.Add(fileSizeField);
            indexWriter.AddDocument(document);
            indexWriter.Commit();

            IndexReader indexReader = DirectoryReader.Open(directory);
            IndexSearcher indexSearcher = new IndexSearcher(indexReader);
            PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(analyzer);
            //QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Name", wrapper);
            //Query query = new Queryparser();
            TopDocs topDocs = indexSearcher.Search(query, 10);
            Console.WriteLine(topDocs.TotalHits);
            foreach (var item in topDocs.ScoreDocs)
            {
                Document fields = indexSearcher.Doc(item.Doc);
            }
            
        }
    }
}