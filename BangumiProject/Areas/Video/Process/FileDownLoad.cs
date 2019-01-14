using BangumiProject.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MoeUtilsBox.Utils;

namespace BangumiProject.Areas.Video.Process
{
    /// <summary>
    /// 使用shell命令调用aria2c下载文件
    /// </summary>
    public class FileDownLoad
    {
        private bool Has = !Directory.Exists(Final.FilePath_DownLoad);
        public string ToolPath { get; set; } = "aria2c";
        public string TorrentFile { get; set; }
        public string SavePath { get; set; }
        private LinkedList<string> Vs = new LinkedList<string>();
        private void Aria2(DataReceivedEventHandler output)
        {
            using (var P = new System.Diagnostics.Process())
            {
                P.StartInfo.FileName = ToolPath;
                P.StartInfo.Arguments = $" -T {TorrentFile} -d {SavePath} --seed-time=0";
                P.StartInfo.UseShellExecute = false;
                P.StartInfo.CreateNoWindow = true;
                P.StartInfo.RedirectStandardOutput = true;
                P.StartInfo.RedirectStandardError = true;

                P.OutputDataReceived += output;
                P.ErrorDataReceived += output;

                P.Start();
                P.BeginOutputReadLine();
                P.BeginErrorReadLine();
                P.WaitForExit();
            }
            File.AppendAllLines($"{Final.FilePath_DownLoad_Log}{TorrentFile.GetFileName()}.log", Vs.ToArray());
        }
        private void Show(string b)
        {
            if (Has)
            {
                Has = false;
                Directory.CreateDirectory(Final.FilePath_DownLoad);
                if (!Directory.Exists(Final.FilePath_DownLoad_Log))
                {
                    Directory.CreateDirectory(Final.FilePath_DownLoad_Log);
                }
            }
            Write(b);
        }
        private void Write(string str)
        {
            Vs.AddLast(str);
        }
        public void Start()
        {
            Aria2((a, b) => Show(b.Data));
        }
    }
}
