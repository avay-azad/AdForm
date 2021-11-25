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
        public virtual DbSet<LabelToDoItem> LabelToDoItems { get; set; }
        public virtual DbSet<LabelToDoList> LabelToDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Users>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Users>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Labels>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Labels>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ToDoItems>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ToDoItems>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ToDoLists>()
          .Property(b => b.CreatedDate)
          .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ToDoLists>()
           .Property(b => b.UpdatedDate)
           .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Users>().HasData(new Users
            {
                FirstName = "Avay",
                LastName = "Azad",
                Password = "peEOLSECrwrx0wHlPWvRpe3xW9dNiqX8sOmBNNbcCdk=",
                UserName = "avay",
                UserId = 1
            }, new Users
            {
                FirstName = "Amar",
                LastName = "kaushik",
                Password = "Ad9dObo9lv45yafXeZbhvtmUcep0AWa398OP+AqvNng=",
                UserName = "amar",
                UserId = 2
            }, new Users
            {
                FirstName = "Azad",
                LastName = "Azad",
                Password = "ITm2Sn8hxDH17QWmvBoBzGebjwIraEgkC7Zah+NHIwo=",
                UserName = "azad",
                UserId = 3
            });

            modelBuilder.Entity<LabelToDoItem>().HasKey(LT => new { LT.LabelId, LT.ToDoItemId });

            modelBuilder.Entity<LabelToDoItem>()
                .HasOne<Labels>(LT => LT.Label)
                .WithMany(L => L.LabelToDoItems)
                .HasForeignKey(LT => LT.LabelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabelToDoItem>()
                .HasOne<ToDoItems>(LT=>LT.ToDoItem)
                .WithMany(I => I.LabelToDoItems)
                .HasForeignKey(LT => LT.ToDoItemId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<LabelToDoList>().HasKey(LT => new { LT.LabelId, LT.ToDoListId });

            modelBuilder.Entity<LabelToDoList>()
                .HasOne<Labels>(LT => LT.Label)
                .WithMany(L => L.LabelToDoLists)
                .HasForeignKey(LT => LT.LabelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabelToDoList>()
                .HasOne<ToDoLists>(LT => LT.ToDoList)
                .WithMany(L => L.LabelToDoLists)
                .HasForeignKey(LT => LT.ToDoListId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoItems>()
                .HasOne<Users>(u=>u.Users)
                .WithMany(ti=>ti.ToDoItems)
                .HasForeignKey(u=>u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        }
}
