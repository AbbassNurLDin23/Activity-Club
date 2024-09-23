using Microsoft.EntityFrameworkCore;

namespace Club.Core.DataModels
{
    public class MyDataContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Guide> Guides { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<Event> Events { get; set; }


        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //User-User
            // Configure the DOB property
            //modelBuilder.Entity<User>()
            //    .Property(u => u.DOB)
            //    .HasColumnType("datetime"); // Adjust the column type as needed
            //modelBuilder.Entity<User>()
            //.HasMany(s => s.Users)
            //.WithMany(c => c.Admins)
            //.UsingEntity<Dictionary<string, object>>(
            //    "AdminManageUser",
            //    j => j.HasOne<User>().WithMany().HasForeignKey("UserEmail"),
            //    j => j.HasOne<User>().WithMany().HasForeignKey("AdminEmail"));

            //User-Event
            modelBuilder.Entity<User>()
            .HasMany(s => s.Events)
            .WithMany(c => c.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserJoinEvent",
                j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserEmail"));

            //User-Lookup
            modelBuilder.Entity<User>()
            .HasMany(s => s.Lookups)
            .WithMany(c => c.Admins)
            .UsingEntity<Dictionary<string, object>>(
                "UserManageLookup",
                j => j.HasOne<Lookup>().WithMany().HasForeignKey("LookupOrder"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserEmail"));

            //Guide-Event
            modelBuilder.Entity<Guide>()
            .HasMany(s => s.GuideEvents)
            .WithMany(c => c.Guides)
            .UsingEntity<Dictionary<string, object>>(
                "GuideEvent",
                j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                j => j.HasOne<Guide>().WithMany().HasForeignKey("GuideEmail"));


            //Member-Event
            modelBuilder.Entity<Member>()
            .HasMany(e => e.MemberEvents)
            .WithMany(m => m.Members)
            .UsingEntity<Dictionary<string, object>>(
                "MemberEvent",
                j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                j => j.HasOne<Member>().WithMany().HasForeignKey("MemberId"));


            //Event-Lookup
            modelBuilder.Entity<Lookup>()
            .HasMany(p => p.Events)
            .WithOne(c => c.lookup)
            .HasForeignKey(c => c.Category)
            .OnDelete(DeleteBehavior.Cascade); // Set the cascade delete behavior

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=NOURLDIN-105614\\SQLEXPRESS;Initial Catalog=ActivityClubProject;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}
