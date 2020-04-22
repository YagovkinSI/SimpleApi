using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApiServer.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public AppDbContext (DbContextOptions<AppDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
