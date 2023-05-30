using Face_Recognition.Models;
using Microsoft.EntityFrameworkCore;

namespace Face_Recognition.EntityStore
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<ImageStore> ImageStores { get; set; }
    }
}
