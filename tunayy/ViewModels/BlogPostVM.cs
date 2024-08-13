using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class BlogPostVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        [StringLength(100, ErrorMessage = "Yazan kişi en fazla 100 karakter uzunluğunda olabilir.")]
        public string? YazanKisi { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL girin.")]
        public string? Resim { get; set; }

        [StringLength(250, ErrorMessage = "Kısa açıklama en fazla 250 karakter uzunluğunda olabilir.")]
        public string? KisaAciklama { get; set; }

        [StringLength(2000, ErrorMessage = "Açıklama en fazla 2000 karakter uzunluğunda olabilir.")]
        public string? Aciklama { get; set; }
    }
}
