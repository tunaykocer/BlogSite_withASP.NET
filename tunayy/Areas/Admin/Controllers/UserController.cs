using AspNetCoreHero.ToastNotification.Abstractions; 
using tunayy.Utilites;
using tunayy.ViewModels; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 
using System.Linq; 
using System.Collections.Generic; 
using tunayy.Entities.Models.Concrete; 

namespace tunayy.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu controller'ın Admin alanında olduğunu belirtir
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; // Kullanıcı yönetimi için UserManager
        private readonly SignInManager<ApplicationUser> _signInManager; // Kullanıcı giriş işlemleri için SignInManager
        public INotyfService _notification { get; } // Toast bildirimleri için servis

        // Constructor: Bağımlılıkları alır ve sınıfın üyelerine atar
        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notification = notyfService;
        }

        // Mesaj şablonundaki yer tutucuları belirtilen değerlerle değiştirir
        private string FormatMessage(string message, Dictionary<string, string> tokens)
        {
            foreach (var token in tokens)
            {
                message = message.Replace($"{{{token.Key}}}", token.Value); // Yer tutucuları belirtilen değerlerle değiştirir
            }
            return message; // Formatlanmış mesajı döndürür
        }

        // GET isteği ile kullanıcı listesini gösterir
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync(); // Tüm kullanıcıları alır

            var vm = users.Select(x => new UserVM()
            {
                Id = x.Id,
                Adi = x.Adi,
                Soyadi = x.Soyadi,
                KullaniciAdi = x.UserName,
                Email = x.Email,
            }).ToList();

            // Kullanıcı rolleri atama
            foreach (var user in vm)
            {
                var singleUser = await _userManager.FindByIdAsync(user.Id);
                var role = await _userManager.GetRolesAsync(singleUser);
                user.Gorevi = role.FirstOrDefault(); // Kullanıcının rolünü atar
            }

            return View(vm); // ViewModel ile View'ı döndürür
        }

        // Kullanıcı şifresini sıfırlama sayfasını gösterir
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id); // Kullanıcıyı ID ile bulur
            if (existingUser == null)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Kullanıcı" }
                };
                _notification.Error(FormatMessage("{EntityName} mevcut değil", errorTokens)); // Hata mesajı gösterir
                return View(); // Boş ViewModel ile View'ı döndürür
            }
            var vm = new ResetPasswordVM()
            {
                Id = existingUser.Id,
                KullaniciAdi = existingUser.UserName
            };
            return View(vm); // ViewModel ile View'ı döndürür
        }

        // Şifre sıfırlama işlemini gerçekleştirir
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); } // Model geçerli değilse formu yeniden gösterir
            var existingUser = await _userManager.FindByIdAsync(vm.Id); // Kullanıcıyı ID ile bulur
            if (existingUser == null)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Kullanıcı" }
                };
                _notification.Error(FormatMessage("{EntityName} mevcut değil", errorTokens)); // Hata mesajı gösterir
                return View(vm); // Formu yeniden gösterir
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser); // Şifre sıfırlama token'ı oluşturur
            var result = await _userManager.ResetPasswordAsync(existingUser, token, vm.YeniSifre); // Şifreyi sıfırlar
            if (result.Succeeded)
            {
                var successTokens = new Dictionary<string, string>
                {
                    { "Action", "sıfırlandı" }
                };
                _notification.Success(FormatMessage("Şifre başarıyla {Action}", successTokens)); // Başarı mesajı gösterir
                return RedirectToAction(nameof(Index)); // Index sayfasına yönlendirir
            }
            return View(vm); // Formu yeniden gösterir
        }

        // Kullanıcı kayıt sayfasını gösterir
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM()); // Boş ViewModel ile View'ı döndürür
        }

        // Yeni kullanıcı kaydı işlemini gerçekleştirir
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); } // Model geçerli değilse formu yeniden gösterir
            var checkUserByEmail = await _userManager.FindByEmailAsync(vm.Email); // Email ile kullanıcıyı kontrol eder
            if (checkUserByEmail != null)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Email" }
                };
                _notification.Error(FormatMessage("{EntityName} zaten mevcut", errorTokens)); // Hata mesajı gösterir
                return View(vm); // Formu yeniden gösterir
            }
            var checkUserByUsername = await _userManager.FindByNameAsync(vm.KullaniciAdi); // Kullanıcı adı ile kullanıcıyı kontrol eder
            if (checkUserByUsername != null)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Kullanıcı adı" }
                };
                _notification.Error(FormatMessage("{EntityName} zaten mevcut", errorTokens)); // Hata mesajı gösterir
                return View(vm); // Formu yeniden gösterir
            }

            var applicationUser = new ApplicationUser()
            {
                Email = vm.Email,
                UserName = vm.KullaniciAdi,
                Adi = vm.Adi,
                Soyadi = vm.Soyadi
            };

            var result = await _userManager.CreateAsync(applicationUser, vm.Sifre); // Yeni kullanıcıyı oluşturur
            if (result.Succeeded)
            {
                // Kullanıcıyı role atar
                if (vm.AdminKontrol)
                {
                    await _userManager.AddToRoleAsync(applicationUser, WebsiteRoles.WebsiteAdmin);
                }
                else
                {
                    await _userManager.AddToRoleAsync(applicationUser, WebsiteRoles.WebsiteAuthor);
                }
                var successTokens = new Dictionary<string, string>
                {
                    { "Action", "kayıt edildi" }
                };
                _notification.Success(FormatMessage("Kullanıcı başarıyla {Action}", successTokens)); // Başarı mesajı gösterir
                return RedirectToAction("Index", "User", new { area = "Admin" }); // Kullanıcılar sayfasına yönlendirir
            }
            return View(vm); // Formu yeniden gösterir
        }

        // Kullanıcı giriş sayfasını gösterir
        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity!.IsAuthenticated)
            {
                return View(new LoginVM()); // Giriş yapmamış kullanıcılar için boş ViewModel ile View'ı döndürür
            }
            return RedirectToAction("Index", "Post", new { area = "Admin" }); // Zaten giriş yapmışsa Post Index sayfasına yönlendirir
        }

        // Kullanıcı giriş işlemini gerçekleştirir
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); } // Model geçerli değilse formu yeniden gösterir
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.KullaniciAdi); // Kullanıcı adı ile kullanıcıyı bulur
            if (existingUser == null)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Kullanıcı adı" }
                };
                _notification.Error(FormatMessage("{EntityName} mevcut değil", errorTokens)); // Hata mesajı gösterir
                return View(vm); // Formu yeniden gösterir
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, vm.Sifre); // Şifreyi doğrular
            if (!verifyPassword)
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Şifre" }
                };
                _notification.Error(FormatMessage("{EntityName} eşleşmiyor", errorTokens)); // Hata mesajı gösterir
                return View(vm); // Formu yeniden gösterir
            }
            await _signInManager.PasswordSignInAsync(vm.KullaniciAdi, vm.Sifre, vm.HatirlaBeni, true); // Kullanıcıyı giriş yapar
            var successTokens = new Dictionary<string, string>
            {
                { "Action", "başarılı" }
            };
            _notification.Success(FormatMessage("Giriş {Action}", successTokens)); // Başarı mesajı gösterir
            return RedirectToAction("Index", "Post", new { area = "Admin" }); // Post Index sayfasına yönlendirir
        }

        // Kullanıcı çıkış işlemini gerçekleştirir
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Kullanıcıyı çıkış yapar
            var successTokens = new Dictionary<string, string>
            {
                { "Action", "çıkış yapıldı" }
            };
            _notification.Success(FormatMessage("Başarıyla {Action}", successTokens)); // Başarı mesajı gösterir
            return RedirectToAction("Index", "Home", new { area = "" }); // Ana sayfaya yönlendirir
        }

        // Erişim reddedildi sayfasını gösterir
        [HttpGet("AccessDenied")]
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View(); // Erişim reddedildi View'ını döndürür
        }

        // Kullanıcı silme işlemini gerçekleştirir
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // Kullanıcıyı ID ile bulur
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user); // Kullanıcıyı siler
                if (result.Succeeded)
                {
                    _notification.Success("Kullanıcı başarıyla silindi"); // Başarı mesajı gösterir
                    return RedirectToAction("Index"); // Kullanıcılar sayfasına yönlendirir
                }
                else
                {
                    _notification.Error("Kullanıcı silinemedi"); // Hata mesajı gösterir
                }
            }
            else
            {
                _notification.Error("Kullanıcı bulunamadı"); // Hata mesajı gösterir
            }
            return RedirectToAction("Index"); // Kullanıcılar sayfasına yönlendirir
        }
    }
}
