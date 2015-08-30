using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class CustomerInfoMap : EntityTypeConfiguration<CustomerInfo>
    {
        public CustomerInfoMap()
        {
            ToTable("CustomerInfo");
            HasKey(p => p.CustomerID);
            Property(p => p.CustomerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
