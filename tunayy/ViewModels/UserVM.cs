using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class UserVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Ad maksimum 50 karakter uzunluğunda olabilir.")]
        public string? Adi { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Soyad maksimum 50 karakter uzunluğunda olabilir.")]
        public string? Soyadi { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı maksimum 50 karakter uzunluğunda olabilir.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+$", ErrorMessage = "Kullanıcı adı geçerli bir formatta olmalıdır.")]
        public string? KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        public string? Gorevi { get; set; }
    }
}
