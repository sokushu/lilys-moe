using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Search
{
    /// <summary>
    /// 
    /// </summary>
    public class Tokenizer1 : Tokenizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textReader"></param>
        /// <param name="Mode"></param>
        public Tokenizer1(TextReader textReader, TokenizerMode Mode) : base(AttributeFactory.DEFAULT_ATTRIBUTE_FACTORY, textReader)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IncrementToken()
        {
            throw new Exception();
        }
    }
}
