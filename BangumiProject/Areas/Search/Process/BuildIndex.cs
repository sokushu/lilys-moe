using Anime = BangumiProject.Areas.Bangumi.Models.Anime;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.DBModels;

namespace BangumiProject.Areas.Search.Process
{
    public class BuildIndex
    {
        private ICollection<Anime> Animes { get; set; }
        private ICollection<MusicAlbum> Albums { get; set; }

        public BuildIndex(ICollection<Anime> Animes)
        {
            this.Animes = Animes;
        }
        public BuildIndex(ICollection<MusicAlbum> albums)
        {
            Albums = albums;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Init(params ProcessType[] processTyps)
        {
            //创建分词器
            Analyzer1 analyzer = new Analyzer1(JiebaNet.Segmenter.TokenizerMode.Search);

            //创建写入索引
            Directory directory = FSDirectory.Open(Final.Search_Index);
            IndexWriterConfig writerConfig = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
            IndexWriter indexWriter = new IndexWriter(directory, writerConfig);

            //准备将数据写入到索引库中
            Document document;
            foreach (ProcessType Process in processTyps)
            {
                switch (Process)
                {
                    case ProcessType.Anime:
                        //数据写入
                        foreach (Anime item in Animes)
                        {
                            document = new Document();
                            Field AnimeName = new TextField("AnimeName", item.AnimeName, Field.Store.YES);//动画的名称
                            Field AnimeNum = new Int32Field("AnimeNum", item.AnimeNum, Field.Store.YES);//动画的集数
                            Field AnimeInfo = new TextField("AnimeInfo", item.AnimeInfo, Field.Store.YES);//动画的一些信息
                            document.Add(AnimeName);
                            document.Add(AnimeNum);
                            document.Add(AnimeInfo);

                            indexWriter.AddDocument(document);
                        }
                        break;
                    case ProcessType.Music:
                        //写入数据
                        foreach (MusicAlbum item in Albums)
                        {
                            document = new Document();


                            indexWriter.AddDocument(document);
                        }
                        break;
                    default:
                        throw new Exception($"enum 类型范围外 ProcessType 传入值：{(int)Process}");
                }
            }

            //提交数据
            indexWriter.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ProcessType
        {
            Anime,
            Music,
        }
    }
}
