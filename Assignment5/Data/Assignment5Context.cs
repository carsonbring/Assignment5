using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment5.Models;

namespace Assignment5.Data
{
    public class Assignment5Context : DbContext
    {
        public Assignment5Context (DbContextOptions<Assignment5Context> options)
            : base(options)
        {
        }

        public DbSet<Assignment5.Models.Artist> Artist { get; set; } = default!;

        public DbSet<Assignment5.Models.Song> Song { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment5.Models.Song>()
                .HasOne(s => s.Artist)
                 .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
