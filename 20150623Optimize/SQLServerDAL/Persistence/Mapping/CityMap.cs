using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {

            ToTable("City");
            HasKey(p => p.cityID);
            Property(p => p.cityID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
