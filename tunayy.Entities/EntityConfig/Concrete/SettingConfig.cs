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
    public class SettingConfig:BaseConfig<Setting>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            // Baslik özelliği için konfigürasyon
            builder.Property(p => p.Baslik)
                .HasMaxLength(255)    // 'Baslik' özelliğinin en fazla 255 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // 'Baslik' özelliğinin zorunlu olmadığını belirtir. Yani bu alan boş bırakılabilir.

            // KisaAciklama özelliği için konfigürasyon
            builder.Property(p => p.KisaAciklama)
                .HasMaxLength(500)    // 'KisaAciklama' özelliğinin en fazla 500 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // 'KisaAciklama' özelliğinin zorunlu olmadığını belirtir. Yani bu alan boş bırakılabilir.

            // Resim özelliği için konfigürasyon
            builder.Property(p => p.Resim)
                .HasMaxLength(255)    // 'Resim' özelliğinin en fazla 255 karakter uzunluğunda olmasını sağlar.
                .IsRequired(false);   // 'Resim' özelliğinin zorunlu olmadığını belirtir. Yani bu alan boş bırakılabilir.


        }
    }
}
