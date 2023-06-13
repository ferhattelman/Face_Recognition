using Face_Recognition.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Face_Recognition.EntityStore
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<ImageStore> ImageStores { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LineerCebir> LineerCebirs { get; set; }
        public DbSet<Iktisat> Iktisat { get; set;}
        public DbSet<Programlama> Programlama { get; set; }
    }
}
