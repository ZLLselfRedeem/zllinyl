using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class FoodDiaryMap : EntityTypeConfiguration<FoodDiary>
    {
        public FoodDiaryMap()
        {
            ToTable("FoodDiary");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            HasMany(p => p.FoodDiaryDishes)
                .WithRequired()
                .HasForeignKey(p => p.FoodDiaryId)
                .WillCascadeOnDelete(false);
        }
    }
}
