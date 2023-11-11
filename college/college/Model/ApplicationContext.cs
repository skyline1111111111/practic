using college.Model;
using Microsoft.EntityFrameworkCore;

namespace test_practica.Model
{
    public partial class ApplicationContext: DbContext
    {
        //"Server=DESKTOP-QAIRGIH\\SQLEXPRESS;Database=College;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True; Max Pool Size = 1;"

        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Specialisation> Specialisations { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer($"Server={Environment.MachineName}\\SQLEXPRESS;Database=College;Encrypt=False;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
