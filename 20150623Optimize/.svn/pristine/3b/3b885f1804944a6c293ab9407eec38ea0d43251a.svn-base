using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class FoodDiaryDefaultConfigDishMap : EntityTypeConfiguration<FoodDiaryDefaultConfigDish>
    {
        public FoodDiaryDefaultConfigDishMap()
        {
            ToTable("FoodDiaryDefaultConfigDish");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
