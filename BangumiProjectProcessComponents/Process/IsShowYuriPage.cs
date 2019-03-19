using BangumiProjectDBServices.Models;
using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangumiProjectProcessComponents.Process
{
    public class IsShowYuriPage : IProcess<bool, Anime>
    {
        private string YuriName { get; set; }
        public IsShowYuriPage(string YuriName)
        {
            this.YuriName = YuriName;
        }
        public bool Process(Anime input)
        {
            ICollection<AnimeTag> animeTags = input.Tags.NullEmpty();
            if (animeTags.Count == 0)
            {
                return Show();
            }
            else
            {
                var YuriTag = animeTags.FirstOrDefault(tag => tag.TagName == YuriName);
                if (YuriTag != null)
                {
                    return NotShow();
                }
                return Show();
            }
        }

        private bool Show()
        {
            return true;
        }
        private bool NotShow()
        {
            return false;
        }
    }
}
