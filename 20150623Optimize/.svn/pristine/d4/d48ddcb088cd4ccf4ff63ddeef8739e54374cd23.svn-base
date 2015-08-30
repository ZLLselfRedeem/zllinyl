using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class DishPriceInfoMap : EntityTypeConfiguration<DishPriceInfo>
    {
        public DishPriceInfoMap()
        {
            ToTable("DishPriceInfo");
            HasKey(p => p.DishPriceID);
            Property(p => p.DishPriceID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasOptional(p => p.DishInfo)
            //    .WithMany()
            //    .HasForeignKey(p => p.DishID)
            //    .WillCascadeOnDelete(false);
        }
    }
}
