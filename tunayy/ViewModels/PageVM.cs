using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace tunayy.ViewModels
{
    public class PageVM
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        [StringLength(250, ErrorMessage = "Kısa açıklama en fazla 250 karakter uzunluğunda olabilir.")]
        public string? KisaAciklama { get; set; }

        [StringLength(5000, ErrorMessage = "Açıklama en fazla 5000 karakter uzunluğunda olabilir.")]
        public string? Aciklama { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL girin.")]
        public string? Resim { get; set; }

        public IFormFile? Resimm { get; set; }
    }
}

