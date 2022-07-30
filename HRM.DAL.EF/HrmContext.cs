

using Microsoft.EntityFrameworkCore;

namespace HRM.DAL.EF
{
    public class HrmContext : DbContext
    {
        public HrmContext() : base()
        {

        }

        public HrmContext(DbContextOptions<HrmContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VacationEntity> Vacations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HRM;Integrated Security=True");
        }
    }
}