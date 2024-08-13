using tunayy.Entities.Models.Abstract;

namespace tunayy.Entities.Models.Concrete
{
    public class Page:BaseEntity
    {
        
        public string? Baslik { get; set; }
        public string? KisaAciklama { get; set; }
        public string? Aciklama { get; set; }
        public string? Slug { get; set; }
        public string? Resim { get; set; }
    }
}
