using tunayy.Data; 
using tunayy.Entities.Models.Abstract; 
using Microsoft.AspNetCore.Identity; 
using tunayy.Entities.Models.Concrete; 

namespace tunayy.Utilites
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context; // Veritabanı bağlamı
        private readonly UserManager<ApplicationUser> _userManager; // Kullanıcı yönetimi
        private readonly RoleManager<IdentityRole> _roleManager; // Rol yönetimi

        // Constructor: Bağımlılıkları alır ve sınıfın üyesine atar
        public DbInitializer(ApplicationDbContext context,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Veritabanını başlatır ve ilk verileri ekler
        public void Initialize()
        {
            // Eğer rol yoksa oluştur
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();

                // Yönetici kullanıcı oluştur
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Adi = "Super",
                    Soyadi = "Admin"
                }, "Admin@0011").Wait(); // sifre

                // Oluşturulan kullanıcıyı rol ekle
                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }

                // Sayfaları oluştur
                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Baslik = "Hakkımda",
                        Slug = "hakkımda"
                    },
                    new Page()
                    {
                        Baslik = "İletisim",
                        Slug = "iletisim"
                    },
                    new Page()
                    {
                        Baslik = "Site Politikasi",
                        Slug = "site politikasi"
                    }
                };

                _context.Pages!.AddRange(listOfPages);

                // Site ayarlarını oluştur
                var setting = new Setting
                {
                    SiteAdi = "TunayMeenBlog",
                    Baslik = "TunayMeenWEB",
                    KisaAciklama = "Sitenin Kısa Acıklamasi"
                };

                _context.Settings!.Add(setting);
                _context.SaveChanges(); // Veritabanı değişikliklerini kaydet
            }
        }
    }
}
