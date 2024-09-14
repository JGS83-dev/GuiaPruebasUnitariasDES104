using Microsoft.EntityFrameworkCore;

namespace LibrosAPI.Models
{
    public class LibrosDbContext : DbContext
    {
        public LibrosDbContext(DbContextOptions<LibrosDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
    }
}
