using Microsoft.AspNetCore.Identity;

namespace tunayy.Entities.Models.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string? Adi { get; set; }
        public string? Soyadi { get; set; }

        //relation
        public List<Post>? Posts { get; set; }
    }
}
