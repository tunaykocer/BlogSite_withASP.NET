using tunayy.Data; 
using tunayy.ViewModels;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace tunayy.Controllers
{
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context; // Veritabanı bağlamı

        // Constructor: Bağımlılığı alır ve sınıfın üyesine atar
        public PageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hakkımızda sayfası
        public async Task<IActionResult> About()
        {
            // "about" slug'ına sahip sayfayı veritabanından alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "hakkımda");

            // ViewModel oluşturur ve sayfa bilgilerini atar
            var vm = new PageVM()
            {
                Baslik = page!.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };

            return View(vm); // ViewModel ile View'ı döndürür
        }

        // İletişim sayfası
        public async Task<IActionResult> Contact()
        {
            // "contact" slug'ına sahip sayfayı veritabanından alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "iletisim");

            // ViewModel oluşturur ve sayfa bilgilerini atar
            var vm = new PageVM()
            {
                Baslik = page!.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };

            return View(vm); // ViewModel ile View'ı döndürür
        }

        // Gizlilik politikası sayfası
        public async Task<IActionResult> PrivacyPolicy()
        {
            // "privacy" slug'ına sahip sayfayı veritabanından alır
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "site politikasi");

            // ViewModel oluşturur ve sayfa bilgilerini atar
            var vm = new PageVM()
            {
                Baslik = page!.Baslik,
                KisaAciklama = page.KisaAciklama,
                Aciklama = page.Aciklama,
                Resim = page.Resim,
            };

            return View(vm); // ViewModel ile View'ı döndürür
        }
    }
}
