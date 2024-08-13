using System;
using System.ComponentModel.DataAnnotations;

namespace tunayy.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter uzunluğunda olabilir.")]
        public string? Baslik { get; set; }

        public string? YazanKisi { get; set; }

        [Required(ErrorMessage = "Oluşturulma tarihi boş bırakılamaz.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL girin.")]
        public string? Resim { get; set; }
    }
}
