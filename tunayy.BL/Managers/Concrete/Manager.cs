using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tunayy.BL.Managers.Abstract;
using tunayy.Data;
using tunayy.Entities.Models.Concrete;
using tunayy.Utilites;


namespace tunayy.BL.Managers.Concrete
{
    public class Manager : DbInitializer, Imanager
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manager(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, userManager, roleManager) // DbInitializer'ın kurucusunu çağırıyoruz
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void ValidatePost(Post input)
        {
            // Başlık kontrolü
            if (string.IsNullOrEmpty(input.Baslik))
            {
                throw new ArgumentException("Başlık boş olamaz.");
            }

            if (input.Baslik.Trim().Length < 2)
            {
                throw new ArgumentException("Başlık en az 2 karakter uzunluğunda olmalıdır.");
            }

            // Kısa Açıklama kontrolü
            if (!string.IsNullOrEmpty(input.KisaAciklama) && input.KisaAciklama.Length < 50)
            {
                throw new ArgumentException("Kısa açıklama en az 20 karakter uzunluğunda olmalıdır.");
            }

            // Kullanıcı ID kontrolü
            if (string.IsNullOrEmpty(input.KullaniciId))
            {
                throw new ArgumentException("Kullanıcı ID'si boş olamaz.");
            }

            // Açıklama kontrolü
            if (!string.IsNullOrEmpty(input.Aciklama) && input.Aciklama.Length < 50)
            {
                throw new ArgumentException("Açıklama en az 50 karakter uzunluğunda olmalıdır.");
            }

            // Slug kontrolü
            if (!string.IsNullOrEmpty(input.Slug) && input.Slug.Length < 10)
            {
                throw new ArgumentException("Slug en az 10 karakter uzunluğunda olmalıdır.");
            }

            // Resim URL kontrolü
            if (!string.IsNullOrEmpty(input.Resim))
            {
                if (!Uri.IsWellFormedUriString(input.Resim, UriKind.Absolute))
                {
                    throw new ArgumentException("Geçerli bir resim URL'si girin.");
                }
            }

            // Diğer iş kuralları burada eklenebilir
        }

    }
}
