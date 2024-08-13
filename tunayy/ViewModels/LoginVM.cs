using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class LoginVM
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Kullanıcı adı geçerli bir Gmail adresi olmalıdır.")]
        public string? KullaniciAdi { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string? Sifre { get; set; }

        public bool HatirlaBeni { get; set; }
    }
}