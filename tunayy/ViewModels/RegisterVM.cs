using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter uzunluğunda olabilir.")]
        public string? Adi { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter uzunluğunda olabilir.")]
        public string? Soyadi { get; set; }

        [Required(ErrorMessage = "E-posta adresi boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Kullanıcı adı en az 4 ve en fazla 50 karakter uzunluğunda olmalıdır.")]
        public string? KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.")]
        public string? Sifre { get; set; }

        public bool AdminKontrol { get; set; }
    }
}
