using jieba.NET;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.TokenAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.Search
{
    /// <summary>
    /// 建立一个分词类
    /// 
    /// 继承Analyzer类，扩展CreateComponents方法。
    /// 
    /// 进行分词处理
    /// </summary>
    /// <see cref="https://www.cnblogs.com/Leo_wl/p/8439869.html"/>
    public class AnalyzerMy : Analyzer
    {
        /// <summary>
        /// 
        /// </summary>
        public TokenizerMode Mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mode">只有两种模式，一般来说写入用Search，查询用默认</param>
        public AnalyzerMy(TokenizerMode Mode) : base()
        {
            this.Mode = Mode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            var token = new JieBaTokenizer(reader, Mode);

            var tokenStream = new LowerCaseFilter(Lucene.Net.Util.LuceneVersion.LUCENE_48, token);

            tokenStream.AddAttribute<ICharTermAttribute>();
            tokenStream.AddAttribute<IOffsetAttribute>();

            return new TokenStreamComponents(token, tokenStream);
        }
    }
}
