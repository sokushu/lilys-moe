using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class CacheKey
    {
        /// <summary>
        /// 根据得到一个动画
        /// 包含动画下的所有数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Anime_One(int ID)
        {
            return $"Anime{ID}";
        }
        public static string Blog_One_ByAnimeID(int AnimeID)
        {
            return $"BlogsByAnimeID{AnimeID}";
        }
        /// <summary>
        /// 所有的动画
        /// 不包含字段的list数据，就是外键
        /// </summary>
        /// <returns></returns>
        public static string Anime_All()
        {
            return "AnimeAll";
        }
        /// <summary>
        /// 所有的年份
        /// </summary>
        /// <returns></returns>
        public static string Anime_AllYear()
        {
            return "AnimeAllYear";
        }
        /// <summary>
        /// 所有的标签
        /// </summary>
        /// <returns></returns>
        public static string Anime_AllTags()
        {
            return "AnimeAllTags";
        }
        /// <summary>
        /// 得到首页的4个最新动画
        /// </summary>
        /// <returns></returns>
        public static string Anime_New4()
        {
            return "AnimeNew4";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Anime_New4_Yuri()
        {
            return nameof(Anime_New4_Yuri);
        }
        /// <summary>
        /// 得到没有完结的全部动画
        /// </summary>
        /// <returns></returns>
        public static string Anime_NotEnd()
        {
            return "AnimeNotEnd";
        }
        /// <summary>
        /// 得到某一部动画全部的评论
        /// </summary>
        /// <param name="AnimeID"></param>
        /// <returns></returns>
        public static string Anime_Comments(int AnimeID)
        {
            return $"AnimeComments{AnimeID}";
        }
        /// <summary>
        /// 根据ID得到一篇博客
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Blog_One(int ID)
        {
            return $"Blog{ID}";
        }
        /// <summary>
        /// 不包含字段的list数据，就是外键
        /// </summary>
        /// <returns></returns>
        public static string Blog_All()
        {
            return "Blog_All";
        }
        /// <summary>
        /// 得到动画用户的对应关系
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="AnimeID"></param>
        /// <returns></returns>
        public static string Anime_User_Info(string UID, int AnimeID)
        {
            return $"AnimeUserInfo{UID}{AnimeID}";
        }
        /// <summary>
        /// 得到某个用户的信息
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static string User_UID(string UID)
        {
            return $"User_UID{UID}";
        }
    }
}
