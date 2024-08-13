using AspNetCoreHero.ToastNotification.Abstractions;
using tunayy.Data;
using tunayy.Entities.Models.Concrete;
using tunayy.Utilites;
using tunayy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Linq;

namespace tunayy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(ApplicationDbContext context,
                                INotyfService notyfService,
                                IWebHostEnvironment webHostEnvironment,
                                UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notification = notyfService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);
        }

        private async Task<string> GetLoggedInUserRoleAsync()
        {
            var user = await GetLoggedInUserAsync();
            var roles = await _userManager.GetRolesAsync(user!);
            return roles.FirstOrDefault() ?? "";
        }

        private string FormatMessage(string message, Dictionary<string, string> tokens)
        {
            foreach (var token in tokens)
            {
                message = message.Replace($"{{{token.Key}}}", token.Value);
            }
            return message;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var userRole = await GetLoggedInUserRoleAsync();

            // Filtering based on role and user ID
            var query = _context.Posts!
                .Where(x => userRole == WebsiteRoles.WebsiteAdmin || x.KullaniciId == loggedInUser!.Id)
                .Select(x => new PostVM
                {
                    Id = x.Id,
                    Baslik = x.Baslik,
                    CreatedDate = x.CreatedDate,
                    Resim = x.Resim,
                    YazanKisi = x.Kullanici!.Adi + " " + x.Kullanici.Soyadi // Navigation property accessed directly
                })
                .OrderByDescending(x => x.CreatedDate);

            // Using PagedList
            var listOfPostsVM = await query.ToPagedListAsync(page ?? 1, 5);

            return View(listOfPostsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreatePostVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var loggedInUser = await GetLoggedInUserAsync();

            var post = new Post
            {
                Baslik = vm.Baslik,
                Aciklama = vm.Aciklama,
                KisaAciklama = vm.KisaAciklama,
                KullaniciId = loggedInUser!.Id,
                Slug = vm.Baslik != null ? $"{vm.Baslik.Trim().Replace(" ", "-")}-{Guid.NewGuid()}" : null,
                Resim = vm.Resimm != null ? UploadImage(vm.Resimm) : null
            };

            await _context.Posts!.AddAsync(post);
            await _context.SaveChangesAsync();

            var successTokens = new Dictionary<string, string> { { "Action", "oluşturuldu" } };
            _notification.Success(FormatMessage("Gönderi başarıyla {Action}", successTokens));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts!.FindAsync(id);
            if (post == null) return NotFound();

            var loggedInUser = await GetLoggedInUserAsync();
            var userRole = await GetLoggedInUserRoleAsync();

            if (userRole == WebsiteRoles.WebsiteAdmin || loggedInUser!.Id == post.KullaniciId)
            {
                _context.Posts!.Remove(post);
                await _context.SaveChangesAsync();

                var successTokens = new Dictionary<string, string> { { "Action", "silindi" } };
                _notification.Success(FormatMessage("Gönderi başarıyla {Action}", successTokens));

                return RedirectToAction("Index");
            }

            return Forbid();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.Posts!.FindAsync(id);
            if (post == null)
            {
                var errorTokens = new Dictionary<string, string> { { "EntityName", "Gönderi" } };
                _notification.Error(FormatMessage("{EntityName} bulunamadı", errorTokens));
                return RedirectToAction("Index");
            }

            var loggedInUser = await GetLoggedInUserAsync();
            var userRole = await GetLoggedInUserRoleAsync();

            if (userRole != WebsiteRoles.WebsiteAdmin && loggedInUser!.Id != post.KullaniciId)
            {
                var unauthorizedTokens = new Dictionary<string, string> { { "Action", "yetkiniz yok" } };
                _notification.Error(FormatMessage("{Action}", unauthorizedTokens));
                return RedirectToAction("Index");
            }

            var vm = new CreatePostVM
            {
                Id = post.Id,
                Baslik = post.Baslik,
                KisaAciklama = post.KisaAciklama,
                Aciklama = post.Aciklama,
                Resim = post.Resim
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreatePostVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var post = await _context.Posts!.FindAsync(vm.Id);
            if (post == null)
            {
                var errorTokens = new Dictionary<string, string> { { "EntityName", "Gönderi" } };
                _notification.Error(FormatMessage("{EntityName} bulunamadı", errorTokens));
                return View(vm);
            }

            post.Baslik = vm.Baslik;
            post.KisaAciklama = vm.KisaAciklama;
            post.Aciklama = vm.Aciklama;
            post.Resim = vm.Resimm != null ? UploadImage(vm.Resimm) : post.Resim;

            await _context.SaveChangesAsync();

            var successTokens = new Dictionary<string, string> { { "Action", "güncellendi" } };
            _notification.Success(FormatMessage("Gönderi başarıyla {Action}", successTokens));

            return RedirectToAction("Index");
        }

        private string UploadImage(IFormFile file)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
