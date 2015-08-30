using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class ImageInfoMap : EntityTypeConfiguration<ImageInfo>
    {
        public ImageInfoMap()
        {
            ToTable("ImageInfo");
            HasKey(p => p.ImageID);
            Property(p => p.ImageID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
