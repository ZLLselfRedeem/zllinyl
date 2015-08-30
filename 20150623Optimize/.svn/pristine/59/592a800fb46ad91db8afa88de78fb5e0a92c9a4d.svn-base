using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class PreOrder19dianInfoMap : EntityTypeConfiguration<PreOrder19dianInfo>
    {
        public PreOrder19dianInfoMap()
        {
            ToTable("PreOrder19dian");
            HasKey(p => p.preOrder19dianId);
            Property(p => p.preOrder19dianId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.ShopInfo)
                .WithMany()
                .HasForeignKey(p => p.shopId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.MenuInfo)
                .WithMany()
                .HasForeignKey(p => p.menuId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.CustomerInfo)
                .WithMany()
                .HasForeignKey(p => p.customerId)
                .WillCascadeOnDelete(false);
        }
    }
}
