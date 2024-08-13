using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class ResetPasswordVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string? KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Yeni şifre boş bırakılamaz.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Yeni şifre en az 5 karakter uzunluğunda olmalıdır.")]
        public string? YeniSifre { get; set; }

        [Required(ErrorMessage = "Şifre onayı boş bırakılamaz.")]
        [Compare(nameof(YeniSifre), ErrorMessage = "Şifre onayı şifre ile eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }
    }
}
