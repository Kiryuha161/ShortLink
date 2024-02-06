using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLink.Models
{
    public class Link
    {
        [ForeignKey("LinkId")]
        public int Id { get; set; }
        [Display(Name = "Оригинальная ссылка")]
        public string OriginalUrl { get; set; }
        [Display(Name = "Короткая ссылка")]
        public string ShortUrl { get; set; }
    }
}
