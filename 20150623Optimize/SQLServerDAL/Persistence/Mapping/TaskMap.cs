using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Mapping
{
    class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            Map<MenuUpdateTask>(map => map.ToTable("MenuUpdateTask"));
        }
    }
}
