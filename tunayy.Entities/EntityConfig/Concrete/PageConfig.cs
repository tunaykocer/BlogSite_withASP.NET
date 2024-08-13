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
    public class PageConfig:BaseConfig<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {


            // Baslik özelliği için konfigürasyon
            builder.Property(p => p.Baslik)
                .HasMaxLength(255) // Maksimum uzunluğu 255 karakter
                .IsRequired(false); // Bu alanın zorunlu olmadığını belirtir

            // KisaAciklama özelliği için konfigürasyon
            builder.Property(p => p.KisaAciklama)
                .HasMaxLength(500) // Maksimum uzunluğu 500 karakter
                .IsRequired(false); // Bu alanın zorunlu olmadığını belirtir

            // Aciklama özelliği için konfigürasyon
            builder.Property(p => p.Aciklama)
                .IsRequired(false); // Bu alanın zorunlu olmadığını belirtir

            // Slug özelliği için konfigürasyon
            builder.Property(p => p.Slug)
                .HasMaxLength(100) // Maksimum uzunluğu 100 karakter
                .IsRequired(false); // Bu alanın zorunlu olmadığını belirtir

            // Resim özelliği için konfigürasyon
            builder.Property(p => p.Resim)
                .HasMaxLength(255) // Maksimum uzunluğu 255 karakter
                .IsRequired(false); // Bu alanın zorunlu olmadığını belirtir

            // Slug özelliği üzerinde bir indeks oluşturur ve bu indeksin benzersiz olmasını sağlar
            builder.HasIndex(p => p.Slug)
                .IsUnique(); // 'Slug' değerlerinin benzersiz olduğunu belirtir
        }
    }
}

