using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class DishInfoMap : EntityTypeConfiguration<DishInfo>
    {
        public DishInfoMap()
        {
            ToTable("DishInfo");
            HasKey(p => p.DishID);
            Property(p => p.DishID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasMany(p => p.ImageInfos)
                .WithRequired()
                .HasForeignKey(p => p.DishID)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.DishI18N)
                .WithRequiredDependent();

        }
    }
}
