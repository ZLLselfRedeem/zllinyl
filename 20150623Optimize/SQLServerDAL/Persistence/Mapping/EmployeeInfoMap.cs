using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class EmployeeInfoMap : EntityTypeConfiguration<EmployeeInfo>
    {
        public EmployeeInfoMap()
        {
            ToTable("EmployeeInfo");
            HasKey(p => p.EmployeeID);
            Property(p => p.EmployeeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
