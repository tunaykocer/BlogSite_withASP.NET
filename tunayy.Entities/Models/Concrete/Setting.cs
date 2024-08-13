using tunayy.Entities.Models.Abstract;

namespace tunayy.Entities.Models.Concrete
{
    public class Setting:BaseEntity
    {
       
        public string? SiteAdi { get; set; }
        public string? Baslik { get; set; }
        public string? KisaAciklama { get; set; }
        public string? Resim { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? GithubUrl { get; set; }
    }
}
