using PersonasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonasAPI.Tests
{
    public static class Setup
    {
        public static PersonasDbContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PersonasDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new PersonasDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
