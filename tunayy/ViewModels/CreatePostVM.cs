using tunayy.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class CreatePostVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir.")]
        [MinLength(2, ErrorMessage = "Başlık en az 2 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        [MinLength(20, ErrorMessage = "Kısa açıklama en az 20 karakter uzunluğunda olmalıdır.")]
        [StringLength(250, ErrorMessage = "Kısa açıklama en fazla 250 karakter uzunluğunda olabilir.")]
        public string? KisaAciklama { get; set; }

        [Required(ErrorMessage = "Kullanıcı ID'si boş olamaz")]
        public string? KullaniciId { get; set; }

        [MinLength(50, ErrorMessage = "Açıklama en az 50 karakter uzunluğunda olmalıdır")]
        public string? Aciklama { get; set; }

        [Url(ErrorMessage = "Geçerli bir resim URL'si girin")]
        public string? Resim { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Resimm { get; set; }
    }
}
