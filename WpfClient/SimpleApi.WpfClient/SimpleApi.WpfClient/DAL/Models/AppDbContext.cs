using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.DAL.Models
{
    public class AppDbContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simpleApiWpfClient;Trusted_Connection=True;";

        public DbSet<Note> Notes { get; set; }
        public DbSet<Sending> Sendings { get; set; }

        public AppDbContext()
            : base(new DbContextOptions<AppDbContext>())
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreateNote(modelBuilder);
            CreateSending(modelBuilder);
        }

        private void CreateNote(ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Note>();
            model.HasMany(m => m.Sendings)
                .WithOne(m => m.Note)
                .HasForeignKey(m => m.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateSending(ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Sending>();
            model.HasOne(m => m.Note)
                .WithMany(m => m.Sendings)
                .HasForeignKey(m => m.NoteId);
            model.HasIndex(m => m.NoteId);
            model.HasIndex(m => m.Success);
            model.HasIndex(m => new { m.NoteId, m.Success });
        }
    }
}
