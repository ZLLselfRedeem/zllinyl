using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class DishI18NMap : EntityTypeConfiguration<DishI18n>
    {
        public DishI18NMap()
        {
            ToTable("DishI18n");
            HasKey(p => p.DishI18nID);
            Property(p => p.DishI18nID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
