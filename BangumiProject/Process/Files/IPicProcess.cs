using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Files
{
    public interface IPicProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="SavePath"></param>
        /// <param name="ImageDecoder"></param>
        void CreateSmallPic400(Stream stream, string SavePath, IImageDecoder ImageDecoder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="SavePath"></param>
        /// <param name="ImageDecoder"></param>
        void CreateSmallPic200(Stream stream, string SavePath, IImageDecoder ImageDecoder);

        void CreateSmallPic(Stream stream, string SavePath, IImageDecoder ImageDecoder, int Size);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        string CreateFileName(string FileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        void SetDecoder(string ContentType);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IImageDecoder GetImageDecoder();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        IImageDecoder GetImageDecoder(string ContentType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        bool FileTypeCheck(string ContentType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MimeType"></param>
        void SetMIMEType(string MimeType);
    }
}
