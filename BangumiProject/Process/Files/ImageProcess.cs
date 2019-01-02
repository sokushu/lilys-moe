using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BangumiProject.Process.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageProcess : IPicProcess
    {
        private const string _Jpg = "image/jpeg";
        private const string _Gif = "image/gif";
        private const string _Bmp = "image/bmp";
        private const string _Png = "image/png";
        private readonly IDictionary<string, string> AllMimeType = new FileExtensionContentTypeProvider().Mappings;
        private IImageDecoder ImageDecoder { get; set; }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="SavePath"></param>
        /// <param name="ImageDecoder"></param>
        /// <returns></returns>
        public void CreateSmallPic200(Stream stream, string SavePath, IImageDecoder ImageDecoder)
        {
            CreateSmallPic(stream, SavePath, ImageDecoder, 200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="SavePath"></param>
        /// <param name="ImageDecoder"></param>
        public void CreateSmallPic400(Stream stream, string SavePath, IImageDecoder ImageDecoder)
        {
            CreateSmallPic(stream, SavePath, ImageDecoder, 400);
        }

        /// <summary>
        /// 图片缩略图尺寸调整
        /// </summary>
        /// <param name="image">需要处理的图片</param>
        /// <param name="Size">图片的最大尺寸</param>
        private void ReSize(ref Image<Rgba32> image, int Size)
        {
            double h = image.Height;
            double w = image.Width;

            // 生成缩略图
            if (h > w && h > Size)
            {
                // 计算比例
                double newH = Size;
                double B = newH / h;
                double NewW = w * B;

                image.Mutate(img => img.Resize((int)NewW, (int)newH));
            }
            else if (w > h && h > Size)
            {
                double newW = Size;
                double B = newW / w;
                double NewH = h * B;
                image.Mutate(img => img.Resize((int)newW, (int)NewH));
            }
            else if (h == w)
            {
                image.Mutate(img => img.Resize(Size, Size));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="SavePath"></param>
        private void SaveThumbPic(ref Image<Rgba32> image, string SavePath)
        {
            using (var ImageStream_Thumb = new FileStream(SavePath, FileMode.Create))// 创建一个缩略图文件
            {
                // 将缩略图保存为Jpeg文件
                image.SaveAsJpeg(ImageStream_Thumb, new JpegEncoder { Quality = 75 });
                // 刷新
                ImageStream_Thumb.Flush();
                ImageStream_Thumb.Close();
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IImageDecoder GetImageDecoder()
        {
            return ImageDecoder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public IImageDecoder GetImageDecoder(string ContentType)
        {
            switch (ContentType)
            {
                case _Png:              // PNG 格式上传会出错，原因未知，所以曲线救国（已经解决）
                    return new PngDecoder();
                case _Bmp:
                    return new BmpDecoder();
                case _Gif:
                    return new GifDecoder();
                case _Jpg:
                    return new JpegDecoder();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContentType"></param>
        public void SetDecoder(string ContentType)
        {
            IImageDecoder imageDecoder = null;
            switch (ContentType)
            {
                case _Png:              // PNG 格式上传会出错，原因未知，所以曲线救国（已经解决）
                    imageDecoder = new PngDecoder() { };
                    break;
                case _Bmp:
                    imageDecoder = new BmpDecoder();
                    break;
                case _Gif:
                    imageDecoder = new GifDecoder();
                    break;
                case _Jpg:
                    imageDecoder = new JpegDecoder();
                    break;
                default:    // 前面已经验证过，因此走不到这里，不需要处理
                    break;
            }
            ImageDecoder = imageDecoder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MimeType"></param>
        public void SetMIMEType(string MimeType)
        {
            if (AllMimeType[MimeType] != null)
            {
                this.MimeType.Add(MimeType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="SavePath"></param>
        /// <param name="ImageDecoder"></param>
        /// <param name="Size"></param>
        public void CreateSmallPic(Stream stream, string SavePath, IImageDecoder ImageDecoder, int Size)
        {
            Image<Rgba32> image = Image.Load(stream, ImageDecoder);
            ReSize(ref image, Size);
            SaveThumbPic(ref image, SavePath);
            image.Dispose();//销毁
        }
    }
}
