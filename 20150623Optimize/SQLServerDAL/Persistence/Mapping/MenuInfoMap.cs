using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class MenuInfoMap : EntityTypeConfiguration<MenuInfo>
    {
        public MenuInfoMap()
        {
            ToTable("MenuInfo");
            HasKey(p => p.MenuID);
            Property(p => p.MenuID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
