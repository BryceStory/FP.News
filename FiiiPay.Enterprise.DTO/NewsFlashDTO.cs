using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.DTO
{
    public class NewsFlashListOM
    {
        public int Id { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string CNTitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// 中文正文
        /// </summary>
        public string CNContent { get; set; }
        /// <summary>
        /// 英文正文
        /// </summary>
        public string ENContent { get; set; }
        /// <summary>
        /// 中文来源
        /// </summary>
        public string CNSource { get; set; }
        /// <summary>
        /// 英文版来源
        /// </summary>
        public string ENSource { get; set; }
        /// <summary>
        /// 图片文件
        /// </summary>
         public List< NewsFlashFile> NewsFlashFiles { get; set; }

        public List<string> CNCoverPictureUrl { get; set; }

        public List<string> ENCoverPictureUrl { get; set; }

        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 利好()
        /// </summary>
        public int Good { get; set; }
        /// <summary>
        /// 利空（）
        /// </summary>
        public int Bad { get; set; }
      

    }

    public class NewsFlashFile
    {
        public string CoverPictureUrl { get; set; }
        public int Version { get; set; }
    }

    public class NewsFlashOM
    {
        public int Id { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string CNTitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// 中文正文
        /// </summary>
        public string CNContent { get; set; }
        /// <summary>
        /// 英文正文
        /// </summary>
        public string ENContent { get; set; }
        /// <summary>
        /// 中文来源
        /// </summary>
        public string CNSource { get; set; }
        /// <summary>
        /// 英文版来源
        /// </summary>
        public string ENSource { get; set; }

        public string[] CNCoverPictureUrl { get; set; }

        public string[] ENCoverPictureUrl { get; set; }
        /// <summary>
        /// 图片文件
        /// </summary>
        //  public List<NewsFlashFileInfo> NewsFlashFiles { get; set; }

        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 利好()
        /// </summary>
        public int Good { get; set; }
        /// <summary>
        /// 利空（）
        /// </summary>
        public int Bad { get; set; }
       
    }

    public class GoodAndBadInfoOM
    {
        public int Good { get; set; }

        public int Bad { get; set; }
    }
}
