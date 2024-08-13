using tunayy.Entities.Models.Abstract;

namespace tunayy.Entities.Models.Concrete
{
    public class Post:BaseEntity
    {
       
        public string? Baslik { get; set; }
        public string? KisaAciklama { get; set; }
        //relation
        public string? KullaniciId { get; set; }
        public ApplicationUser? Kullanici { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Aciklama { get; set; }
        public string? Slug { get; set; }
        public string? Resim { get; set; }
    }
}
