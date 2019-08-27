using FiiiPay.Enterprise.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class NewsModels : PageModel
    {
        public int? NewsSetionId { get; set; }
    }

    public class SectionModel : PageModel
    {

    }

    public class SectionNewsModels : PageModel
    {
        public int NewsSetionId { get; set; }
    }

    public class NewsDetailModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class NewsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public byte Status { get; set; }
    }

    public class NewsSectionViewModel
    {
        [Required]
        public string Title { get; set; }

        //[Required]
        //public byte Status { get; set; }
    }

    public class NewsFlashViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public byte Status { get; set; }
    }

    public class GoodSelectModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int SelectStatus { get; set; }
    }

    public class PageViewAddModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Version { get; set; }
    }
}