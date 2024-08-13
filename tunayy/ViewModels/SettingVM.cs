using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace tunayy.ViewModels
{
    public class SettingVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Site adı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Site adı maksimum 100 karakter uzunluğunda olabilir.")]
        public string? SiteAdi { get; set; }

        [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Başlık maksimum 150 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        [StringLength(500, ErrorMessage = "Kısa açıklama maksimum 500 karakter uzunluğunda olabilir.")]
        public string? KisaAciklama { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        public string? GithubUrl { get; set; }

        public string? Resim { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Geçerli bir resim dosyası seçiniz (jpg, jpeg, png).")]
        public IFormFile? Resimm { get; set; }
    }
}
