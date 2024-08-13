using AspNetCoreHero.ToastNotification.Abstractions; 
using tunayy.Data;
using tunayy.Entities.Models.Concrete; 
using tunayy.ViewModels; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Hosting;

namespace tunayy.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu controller'ın Admin alanında olduğunu belirtir
    [Authorize(Roles = "Admin")] // Bu controller'a sadece Admin rolüne sahip kullanıcıların erişmesine izin verir
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _context; // Veritabanı bağlamı
        public INotyfService _notification { get; } // Toast bildirimleri için servis
        private readonly IWebHostEnvironment _webHostEnvironment; // Web ortamı için hizmet

        // Constructor: Bağımlılıkları alır ve sınıfın üyelerine atar
        public SettingController(ApplicationDbContext context,
                                INotyfService notyfService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notification = notyfService;
            _webHostEnvironment = webHostEnvironment;
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

        // GET isteği ile ayarları gösterir
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = _context.Settings!.ToList(); // Ayarları alır
            if (settings.Count > 0)
            {
                // Eğer ayar varsa, mevcut ayarları ViewModel'e aktarır
                var vm = new SettingVM()
                {
                    Id = settings[0].Id,
                    SiteAdi = settings[0].SiteAdi,
                    Baslik = settings[0].Baslik,
                    KisaAciklama = settings[0].KisaAciklama,
                    Resim = settings[0].Resim,
                    GithubUrl = settings[0].GithubUrl,
                };
                return View(vm); // ViewModel ile View'ı döndürür
            }
            // Eğer ayar yoksa, varsayılan bir ayar oluşturur
            var setting = new Setting()
            {
                SiteAdi = "Demo Name", // Varsayılan site adı
            };
            await _context.Settings!.AddAsync(setting); // Yeni ayarı veritabanına ekler
            await _context.SaveChangesAsync(); // Değişiklikleri kaydeder
            var createdSettings = _context.Settings!.ToList(); // Yeni eklenen ayarları alır
            var createdVm = new SettingVM()
            {
                Id = createdSettings[0].Id,
                SiteAdi = createdSettings[0].SiteAdi,
                Baslik = createdSettings[0].Baslik,
                KisaAciklama = createdSettings[0].KisaAciklama,
                Resim = createdSettings[0].Resim,
                GithubUrl = createdSettings[0].GithubUrl,
            };
            return View(createdVm); // Yeni ayarları ViewModel ile View'ı döndürür
        }

        // POST isteği ile ayarları günceller
        [HttpPost]
        public async Task<IActionResult> Index(SettingVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); } // Model geçerli değilse formu yeniden gösterir
            var setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == vm.Id); // Güncellenecek ayarı alır
            if (setting == null)
            {
                // Ayar bulunamazsa hata mesajı gösterir
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Ayarlar" }
                };
                _notification.Error(FormatMessage("{EntityName} bulunamadı", errorTokens));
                return View(vm); // Formu yeniden gösterir
            }
            // Ayarları günceller
            setting.SiteAdi = vm.SiteAdi;
            setting.Baslik = vm.Baslik;
            setting.KisaAciklama = vm.KisaAciklama;
            setting.GithubUrl = vm.GithubUrl;

            // Yeni bir resim yüklenmişse, resmi günceller
            if (vm.Resimm != null)
            {
                setting.Resim = UploadImage(vm.Resimm);
            }
            await _context.SaveChangesAsync(); // Değişiklikleri kaydeder

            // Başarı mesajı gösterir
            var successTokens = new Dictionary<string, string>
            {
                { "Action", "güncellendi" }
            };
            _notification.Success(FormatMessage("Ayar başarıyla {Action}", successTokens));
            return RedirectToAction("Index", "Setting", new { area = "Admin" }); // Index sayfasına yönlendirir
        }

        // Yüklenen resmi sunucuya kaydeder ve benzersiz dosya adı döndürür
        private string UploadImage(IFormFile file)
        {
            string uniqueFileName = ""; // Benzersiz dosya adı
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails"); // Dosyaların kaydedileceği klasör yolu
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName; // Benzersiz dosya adı oluşturur
            var filePath = Path.Combine(folderPath, uniqueFileName); // Dosya yolunu oluşturur
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream); // Dosyayı belirtilen yola kaydeder
            }
            return uniqueFileName; // Benzersiz dosya adını döndürür
        }
    }
}
