using AspNetCoreHero.ToastNotification.Abstractions;
using tunayy.Data;
using tunayy.Entities.Models.Concrete;
using tunayy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace tunayy.Areas.Admin.Controllers
{
    // Admin alanında bulunan sayfalar için yetkilendirilmiş bir controller
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcıların erişimine izin verir
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public INotyfService _notification { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor ile bağımlılıkları alır
        public PageController(ApplicationDbContext context,
                              INotyfService notification,
                              IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notification = notification;
            _webHostEnvironment = webHostEnvironment;
        }

        // Mesaj şablonunu belirli bir şablonla biçimlendirir
        private string FormatMessage(string message, Dictionary<string, string> tokens)
        {
            foreach (var token in tokens)
            {
                message = message.Replace($"{{{token.Key}}}", token.Value); // Yer tutucuları belirtilen değerlerle değiştirir
            }
            return message;
        }

        // Hakkında sayfasını GET isteği ile alır ve görüntüler
        [HttpGet]
        public async Task<IActionResult> About()
        {
            // Veritabanından "about" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "hakkımda");

            // ViewModel nesnesini oluşturur ve sayfa verilerini doldurur
            var vm = new PageVM()
            {
                Id = page!.Id,
                Baslik = page.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };
            return View(vm); // View'a ViewModel'i gönderir
        }

        // Hakkında sayfasını POST isteği ile günceller
        [HttpPost]
        public async Task<IActionResult> About(PageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); // Model geçerli değilse formu yeniden gösterir
            }

            // Veritabanından "about" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "hakkımda");

            // Sayfa bulunamazsa hata mesajı gösterir
            if (page == null)
            {
                var tokens = new Dictionary<string, string>
                {
                    { "PageName", "Hakkında" }
                };
                _notification.Error(FormatMessage("{PageName} sayfası bulunamadı", tokens));
                return View();
            }

            // Sayfa bilgilerini günceller
            page.Baslik = vm.Baslik;
            page.KisaAciklama = vm.KisaAciklama;
            page.Aciklama = vm.Aciklama;

            // Resim dosyası varsa yükler ve sayfaya ekler
            if (vm.Resimm != null)
            {
                page.Resim = UploadImage(vm.Resimm);
            }

            // Veritabanı değişikliklerini kaydeder
            await _context.SaveChangesAsync();

            var successTokens = new Dictionary<string, string>
            {
                { "PageName", "Hakkında" },
                { "Action", "güncellemesi" }
            };
            // Başarı mesajı gösterir
            _notification.Success(FormatMessage("{PageName} sayfası {Action} başarılı", successTokens));
            return RedirectToAction("About", "Page", new { area = "Admin" });
        }

        // İletişim sayfasını GET isteği ile alır ve görüntüler
        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            // Veritabanından "contact" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "iletisim");

            // ViewModel nesnesini oluşturur ve sayfa verilerini doldurur
            var vm = new PageVM()
            {
                Id = page!.Id,
                Baslik = page.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };
            return View(vm); // View'a ViewModel'i gönderir
        }

        // İletişim sayfasını POST isteği ile günceller
        [HttpPost]
        public async Task<IActionResult> Contact(PageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); // Model geçerli değilse formu yeniden gösterir
            }

            // Veritabanından "contact" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "iletisim");

            // Sayfa bulunamazsa hata mesajı gösterir
            if (page == null)
            {
                var tokens = new Dictionary<string, string>
                {
                    { "PageName", "İletişim" }
                };
                _notification.Error(FormatMessage("{PageName} sayfası bulunamadı", tokens));
                return View();
            }

            // Sayfa bilgilerini günceller
            page.Baslik = vm.Baslik;
            page.KisaAciklama = vm.KisaAciklama;
            page.Aciklama = vm.Aciklama;

            // Resim dosyası varsa yükler ve sayfaya ekler
            if (vm.Resimm != null)
            {
                page.Resim = UploadImage(vm.Resimm);
            }

            // Veritabanı değişikliklerini kaydeder
            await _context.SaveChangesAsync();

            var successTokens = new Dictionary<string, string>
            {
                { "PageName", "İletişim" },
                { "Action", "güncellemesi" }
            };
            // Başarı mesajı gösterir
            _notification.Success(FormatMessage("{PageName} sayfası {Action} başarılı", successTokens));
            return RedirectToAction("Contact", "Page", new { area = "Admin" });
        }

        // Gizlilik sayfasını GET isteği ile alır ve görüntüler
        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            // Veritabanından "sitecalisma" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "site politikasi");

            // ViewModel nesnesini oluşturur ve sayfa verilerini doldurur
            var vm = new PageVM()
            {
                Id = page!.Id,
                Baslik = page.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };
            return View(vm); // View'a ViewModel'i gönderir
        }

        // Gizlilik sayfasını POST isteği ile günceller
        [HttpPost]
        public async Task<IActionResult> Privacy(PageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); // Model geçerli değilse formu yeniden gösterir
            }

            // Veritabanından "sitecalisma" slug'ına sahip sayfayı alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "site politikasi");

            // Sayfa bulunamazsa hata mesajı gösterir
            if (page == null)
            {
                var tokens = new Dictionary<string, string>
                {
                    { "PageName", "Gizlilik" }
                };
                _notification.Error(FormatMessage("{PageName} sayfası bulunamadı", tokens));
                return View();
            }

            // Sayfa bilgilerini günceller
            page.Baslik = vm.Baslik;
            page.KisaAciklama = vm.KisaAciklama;
            page.Aciklama = vm.Aciklama;

            // Resim dosyası varsa yükler ve sayfaya ekler
            if (vm.Resimm != null)
            {
                page.Resim = UploadImage(vm.Resimm);
            }

            // Veritabanı değişikliklerini kaydeder
            await _context.SaveChangesAsync();

            var successTokens = new Dictionary<string, string>
            {
                { "PageName", "Gizlilik" },
                { "Action", "güncellemesi" }
            };
            // Başarı mesajı gösterir
            _notification.Success(FormatMessage("{PageName} sayfası {Action} başarılı", successTokens));
            return RedirectToAction("Privacy", "Page", new { area = "Admin" });
        }

        // Resim dosyasını yükler ve benzersiz bir dosya adı döndürür
        private string UploadImage(IFormFile file)
        {
            string uniqueFileName = ""; // Benzersiz dosya adını saklayacak değişken
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "f"); // Yüklenen dosyaların kaydedileceği klasör yolu
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName; // Benzersiz dosya adı oluşturur
            var filePath = Path.Combine(folderPath, uniqueFileName); // Dosya yolunu oluşturur
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream); // Dosyayı belirtilen yola kopyalar
            }
            return uniqueFileName; // Yüklenen dosyanın benzersiz adını döndürür
        }
    }
}
