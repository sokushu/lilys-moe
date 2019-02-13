using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Search.Process
{
    /// <summary>
    /// 
    /// </summary>
    public class Tokenizer1 : Tokenizer
    {
        private static readonly object _LockObj = new object();
        private static readonly bool _Inited = false;
        private List<JiebaNet.Segmenter.Token> _WordList = new List<JiebaNet.Segmenter.Token>();
        private string _InputText { get; set; }
        private bool _OriginalResult { get; set; } = false;

        private ICharTermAttribute termAtt;
        private IOffsetAttribute offsetAtt;
        private IPositionIncrementAttribute posIncrAtt;
        private ITypeAttribute typeAtt;

        private List<string> stopWords = new List<string>();
        private readonly string stopUrl = "./stopwords.txt";
        private JiebaSegmenter segmenter;

        private IEnumerator<JiebaNet.Segmenter.Token> iter;
        private int start = 0;

        private TokenizerMode mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textReader"></param>
        /// <param name="Mode"></param>
        public Tokenizer1(TextReader textReader, TokenizerMode Mode) : base(AttributeFactory.DEFAULT_ATTRIBUTE_FACTORY, textReader)
        {
            segmenter = new JiebaSegmenter();
            mode = Mode;
            StreamReader rd = File.OpenText(stopUrl);
            string s = "";
            while ((s = rd.ReadLine()) != null)
            {
                stopWords.Add(s);
            }

            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            termAtt = AddAttribute<ICharTermAttribute>();
            offsetAtt = AddAttribute<IOffsetAttribute>();
            posIncrAtt = AddAttribute<IPositionIncrementAttribute>();
            typeAtt = AddAttribute<ITypeAttribute>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ReadToEnd(TextReader input)
        {
            return input.ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IncrementToken()
        {
            ClearAttributes();

            Lucene.Net.Analysis.Token word = Next();
            if (word != null)
            {
                var buffer = word.ToString();
                termAtt.SetEmpty().Append(buffer);
                offsetAtt.SetOffset(CorrectOffset(word.StartOffset), CorrectOffset(word.EndOffset));
                typeAtt.Type = word.Type;
                return true;
            }
            End();
            this.Dispose();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Lucene.Net.Analysis.Token Next()
        {

            int length = 0;
            bool res = iter.MoveNext();
            Lucene.Net.Analysis.Token token;
            if (res)
            {
                JiebaNet.Segmenter.Token word = iter.Current;

                token = new Lucene.Net.Analysis.Token(word.Word, word.StartIndex, word.EndIndex);
                // Console.WriteLine("xxxxxxxxxxxxxxxx分词："+word.Word+"xxxxxxxxxxx起始位置："+word.StartIndex+"xxxxxxxxxx结束位置"+word.EndIndex);
                start += length;
                return token;

            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            _InputText = ReadToEnd(base.m_input);
            RemoveStopWords(segmenter.Tokenize(_InputText, mode));


            start = 0;
            iter = _WordList.GetEnumerator();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="words"></param>
        public void RemoveStopWords(System.Collections.Generic.IEnumerable<JiebaNet.Segmenter.Token> words)
        {
            _WordList.Clear();
            foreach (var x in words)
            {
                if (stopWords.IndexOf(x.Word) == -1)
                {
                    _WordList.Add(x);
                }
            }
        }
    }
}
