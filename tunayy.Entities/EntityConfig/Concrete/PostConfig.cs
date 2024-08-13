using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tunayy.Entities.EntityConfig.Abstract;
using tunayy.Entities.Models.Concrete;

namespace tunayy.Entities.EntityConfig.Concrete
{
    public class PostConfig : BaseConfig<Post>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {

            // Baslik özelliği için konfigürasyon
            builder.Property(p => p.Baslik)
                .HasMaxLength(255)    // Baslik özelliğinin en fazla 255 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // Baslik özelliğinin zorunlu olmadığını belirtir, yani bu alan boş bırakılabilir.

            // KisaAciklama özelliği için konfigürasyon
            builder.Property(p => p.KisaAciklama)
                .HasMaxLength(500)    // KisaAciklama özelliğinin en fazla 500 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // KisaAciklama özelliğinin zorunlu olmadığını belirtir, yani bu alan boş bırakılabilir.

            // Aciklama özelliği için konfigürasyon
            builder.Property(p => p.Aciklama)
                .IsRequired(false);   // Aciklama özelliğinin zorunlu olmadığını belirtir, yani bu alan boş bırakılabilir.
                                      // Uzunluk kısıtlaması belirtilmemiş, dolayısıyla metin uzunluğu veritabanı türüne göre ayarlanır (örneğin TEXT ya da VARCHAR(MAX)).

            // Slug özelliği için konfigürasyon
            builder.Property(p => p.Slug)
                .HasMaxLength(100)    // Slug özelliğinin en fazla 100 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // Slug özelliğinin zorunlu olmadığını belirtir, yani bu alan boş bırakılabilir.

            // Resim özelliği için konfigürasyon
            builder.Property(p => p.Resim)
                .HasMaxLength(255)    // Resim özelliğinin en fazla 255 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // Resim özelliğinin zorunlu olmadığını belirtir, yani bu alan boş bırakılabilir.

            // Slug özelliği üzerinde benzersiz bir indeks oluşturur
            builder.HasIndex(p => p.Slug)
                .IsUnique();          // Slug değerlerinin benzersiz olmasını sağlar, yani aynı Slug değeri birden fazla kez kullanılamaz.
        }
    }
}
