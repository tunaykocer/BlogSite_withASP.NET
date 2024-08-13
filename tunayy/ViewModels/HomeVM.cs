using System.ComponentModel.DataAnnotations;
using tunayy.Entities.Models.Concrete;
using X.PagedList;

namespace tunayy.ViewModels
{
    public class HomeVM
    {
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        [StringLength(250, ErrorMessage = "Kısa açıklama en fazla 250 karakter uzunluğunda olabilir.")]
        public string? KisaAciklama { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL girin.")]
        public string? Resim { get; set; }

        public IPagedList<Post>? Posts { get; set; }
    }
}
