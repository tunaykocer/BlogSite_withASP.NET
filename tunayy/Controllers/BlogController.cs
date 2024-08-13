using AspNetCoreHero.ToastNotification.Abstractions; 
using tunayy.Data; 
using tunayy.ViewModels; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace tunayy.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context; // Veritabanı bağlamı
        public INotyfService _notification { get; } // Toast bildirimleri için servis

        // Constructor: Bağımlılıkları alır ve sınıfın üyelerine atar
        public BlogController(ApplicationDbContext context, INotyfService notification)
        {
            _context = context;
            _notification = notification;
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

        // Belirli bir slug'a sahip blog postunu görüntüler
        [HttpGet("[controller]/{slug}")]
        public IActionResult Post(string slug)
        {
            if (string.IsNullOrEmpty(slug)) // Slug boşsa hata mesajı gösterir
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Post" }
                };
                _notification.Error(FormatMessage("{EntityName} bulunamadı", errorTokens)); // Hata mesajı gösterir
                return View(); // Boş ViewModel ile View'ı döndürür
            }

            // Sluga göre blog postunu veritabanından bulur ve kullanıcı bilgilerini de dahil eder
            var post = _context.Posts!.Include(x => x.Kullanici).FirstOrDefault(x => x.Slug == slug);
            if (post == null) // Post bulunamazsa hata mesajı gösterir
            {
                var errorTokens = new Dictionary<string, string>
                {
                    { "EntityName", "Post" }
                };
                _notification.Error(FormatMessage("{EntityName} bulunamadı", errorTokens)); // Hata mesajı gösterir
                return View(); // Boş ViewModel ile View'ı döndürür
            }

            // Blog postunu ViewModel'e dönüştürür
            var vm = new BlogPostVM()
            {
                Id = post.Id,
                Baslik = post.Baslik,
                YazanKisi = post.Kullanici!.Adi + " " + post.Kullanici.Soyadi, // Yazarın adını ve soyadını birleştirir
                CreatedDate = post.CreatedDate,
                Resim = post.Resim,
                Aciklama = post.Aciklama,
                KisaAciklama = post.KisaAciklama,
            };

            return View(vm); // ViewModel ile View'ı döndürür
        }
    }
}
