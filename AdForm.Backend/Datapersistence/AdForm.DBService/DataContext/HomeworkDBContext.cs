using Microsoft.EntityFrameworkCore;

namespace AdForm.DBService
{
    public class HomeworkDBContext : DbContext
    {

        public HomeworkDBContext(DbContextOptions<HomeworkDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Labels> Labels { get; set; }
        public virtual DbSet<ToDoItems> ToDoItems { get; set; }
        public virtual DbSet<ToDoLists> ToDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Users>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Users>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Labels>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Labels>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ToDoItems>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ToDoItems>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ToDoLists>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ToDoLists>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Users>().HasData(new Users
            {
                FirstName = "Avay",
                LastName = "Azad",
                Password = "peEOLSECrwrx0wHlPWvRpe3xW9dNiqX8sOmBNNbcCdk=",
                UserName = "avay",
                Id = 1
            }, new Users
            {
                FirstName = "Amar",
                LastName = "kaushik",
                Password = "Ad9dObo9lv45yafXeZbhvtmUcep0AWa398OP+AqvNng=",
                UserName = "amar",
                Id = 2
            }, new Users
            {
                FirstName = "Azad",
                LastName = "Azad",
                Password = "ITm2Sn8hxDH17QWmvBoBzGebjwIraEgkC7Zah+NHIwo=",
                UserName = "azad",
                Id = 3
            });

           
        }
        }
}
