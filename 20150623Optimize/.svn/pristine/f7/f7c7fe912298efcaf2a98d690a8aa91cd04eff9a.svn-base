using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class ShopInfoMap : EntityTypeConfiguration<ShopInfo>
    {
        public ShopInfoMap()
        {
            ToTable("ShopInfo");
            HasKey(p => p.shopID);
            Property(p => p.shopID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.isSupportAccountsRound).IsOptional();

            HasRequired(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.cityID)
                .WillCascadeOnDelete(false);
        }
    }
}
