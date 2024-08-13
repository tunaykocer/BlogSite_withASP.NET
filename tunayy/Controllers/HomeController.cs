using tunayy.Data; 
using tunayy.Entities.Models.Abstract; 
using tunayy.ViewModels; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using System.Diagnostics; 
using X.PagedList; 
using tunayy.Entities.Models.Concrete; 
namespace tunayy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Loglama için gerekli
        private readonly ApplicationDbContext _context; // Veritabanı bağlamı

        // Constructor: Bağımlılıkları alır ve sınıfın üyelerine atar
        public HomeController(ILogger<HomeController> logger,
                                ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Ana sayfa işlemi
        public async Task<IActionResult> Index(int? page)
        {
            var vm = new HomeVM(); // ViewModel oluşturur
            var setting = _context.Settings!.ToList(); // Ayarları veritabanından alır
            vm.Baslik = setting[0].Baslik; // İlk ayar kaydının başlığını alır
            vm.KisaAciklama = setting[0].KisaAciklama; // İlk ayar kaydının kısa açıklamasını alır
            vm.Resim = setting[0].Resim; // İlk ayar kaydının resmini alır

            // Sayfalama işlemleri için pageSize ve pageNumber belirler
            int pageSize = 4; // Sayfa başına gösterilecek post sayısı
            int pageNumber = (page ?? 1); // Varsayılan sayfa numarası 1

            // Postları tarih sırasına göre sıralar ve sayfalar
            vm.Posts = await _context.Posts!.Include(x => x.Kullanici)
                                              .OrderByDescending(x => x.CreatedDate)
                                              .ToPagedListAsync(pageNumber, pageSize);

            return View(vm); // ViewModel ile View'ı döndürür
        }

        // Gizlilik sayfası işlemi
        public IActionResult Privacy()
        {
            return View(); // Privacy View'ını döndürür
        }

        // Hata sayfası işlemi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); // Hata view'ını döndürür
        }
    }
}
