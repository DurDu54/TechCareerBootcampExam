using Microsoft.EntityFrameworkCore;

namespace TechCarreerBootcampExam.Models.ORM
{

    public class DBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Exam;Trusted_Connection=True; TrustServerCertificate=True");
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
