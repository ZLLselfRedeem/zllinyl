using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;


namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class ViewAllocContext : DbContext
    {
        public ViewAllocContext(string connectionString)
            : base(connectionString)
        {
            //Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            DbInterception.Add(new DbCommandInterceptor());
        }

        public ViewAllocContext()
            : this(SqlHelper.ConnectionStringLocalTransaction)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//表名不为复数

            //modelBuilder.Configurations.Add(new ShopInfoMap());
            //modelBuilder.Configurations.Add(new PreOrder19dianInfoMap());
            //modelBuilder.Configurations.Add(new MenuInfoMap());
            //modelBuilder.Configurations.Add(new ImageInfoMap());
            //modelBuilder.Configurations.Add(new DishI18NMap());
            //modelBuilder.Configurations.Add(new FoodDiaryDefaultConfigDishMap());
            //modelBuilder.Configurations.Add(new FoodDiaryMap());
            //modelBuilder.Configurations.Add(new FoodDiaryDishMap());
            //modelBuilder.Configurations.Add(new CityMap());
            //modelBuilder.Configurations.Add(new DishPriceInfoMap());
            //modelBuilder.Configurations.Add(new TaskMap());
            //modelBuilder.Configurations.Add(new DishInfoMap());
            //modelBuilder.Configurations.Add(new CustomerInfoMap());
            //modelBuilder.Configurations.Add(new EmployeeInfoMap());

        }

        public DbSet<ShopInfo> ShopInfos { set; get; }
        public DbSet<PreOrder19dianInfo> PreOrder19DianInfos { set; get; }
        public DbSet<MenuInfo> MenuInfos { set; get; }
        public DbSet<ImageInfo> ImageInfos { set; get; }
        public DbSet<DishI18n> DishI18Ns { set; get; }
        public DbSet<FoodDiaryDefaultConfigDish> FoodDiariesDefaultConfigDishes { set; get; }
        public DbSet<FoodDiary> FoodDiaries { set; get; }
        public DbSet<FoodDiaryDish> FoodDiaryDishes { set; get; }
        public DbSet<City> Cities { set; get; }
        public DbSet<DishPriceInfo> DishPriceInfos { set; get; }
        //public DbSet<Task> Tasks { set; get; }
        public DbSet<MenuUpdateTask> MenuUpdateTasks { set; get; }
        public DbSet<DishInfo> DishInfos { set; get; }
        public DbSet<CustomerInfo> CustomerInfos { set; get; }
        public DbSet<EmployeeInfo> EmployeeInfos { set; get; }

        //public virtual void Commit()
        //{
        //    base.SaveChanges();
        //}
    }
}
