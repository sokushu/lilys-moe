using System;
using System.Collections.Generic;
using System.Text;

namespace MoeMushi.Core.Save
{
    public class FileSave
    {

        private string SavePath { get; set; }

        public FileSave(string Path)
        {
            this.SavePath = Path;
        }

        public void Save(string Html)
        {

        }

        public void Save(byte[] file)
        {

        }
    }
}
