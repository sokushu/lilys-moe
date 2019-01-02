using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using SixLabors.ImageSharp.Formats;

namespace BangumiProject.Process.Files
{
    public class ImageProcessTwo : IPicProcess
    {
        private const string _Jpg = "image/jpeg";
        private const string _Gif = "image/gif";
        private const string _Bmp = "image/bmp";
        private const string _Png = "image/png";
        private IDictionary<string, string> AllMimeType = new FileExtensionContentTypeProvider().Mappings;
        private HashSet<string> MimeType = new HashSet<string>
        {
            _Jpg, _Gif, _Bmp, _Png
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string CreateFileName(string Name)
        {
            // 进行文件名处理
            string FileName = null;     // 处理之后的真的文件名
            string SFileName = string.Empty;    // 文件的扩展名

            var FileName1 = Name;     // 用户上传的文件名

            int Not = 0;
            if ((Not = FileName1.LastIndexOf('.')) > -1)    //看看用户是否上传了没有后缀名的奇葩文件
            {
                SFileName = FileName1.Substring(Not);
            }
            else
            {
                // 奇葩文件——要我命3000
                // 不管他了
            }
            // 防止重名用随机生成 
            var FileName0 = Guid.NewGuid().ToString("N");
            if (FileName0.Length + FileName1.Length > 200)  //文件名有点长
            {
                // 直接随即文件名 + 扩展名
                FileName = FileName0 + SFileName;
            }
            else
            {
                // 随机 + 原始文件名
                FileName = FileName0 + FileName1;
            }
            return FileName;
        }

        public void CreateSmallPic(Stream stream, string SavePath, IImageDecoder ImageDecoder, int Size)
        {
            
        }

        public void CreateSmallPic200(Stream stream, string SavePath, IImageDecoder ImageDecoder)
        {
            
        }

        public void CreateSmallPic400(Stream stream, string SavePath, IImageDecoder ImageDecoder)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public bool FileTypeCheck(string ContentType)
        {
            return MimeType.Contains(ContentType);
        }

        public IImageDecoder GetImageDecoder()
        {
            return null;
        }

        public IImageDecoder GetImageDecoder(string ContentType)
        {
            return null;
        }

        public void SetDecoder(string ContentType)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MimeType"></param>
        public void SetMIMEType(string MimeType)
        {
            if (AllMimeType[MimeType] !=  null)
            {
                this.MimeType.Add(MimeType);
            }
        }
    }
}
