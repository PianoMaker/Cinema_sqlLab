using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cinema4.Models;

namespace Cinema4.Data
{
    public class Cinema4Context : DbContext
    {
        public Cinema4Context (DbContextOptions<Cinema4Context> options)
            : base(options)
        {
        }

        public DbSet<Cinema4.Models.Cinema> Cinemas { get; set; } = default!;
        public DbSet<Cinema4.Models.City> Cities { get; set; } = default!;
        public DbSet<Cinema4.Models.Director> Directors { get; set; } = default!;
        public DbSet<Cinema4.Models.Film> Films { get; set; } = default!;
        public DbSet<Cinema4.Models.Genre> Genres { get; set; } = default!;
        public DbSet<Cinema4.Models.Show> Shows { get; set; } = default!;
        public DbSet<Cinema4.Models.Studio> Studios { get; set; } = default!;
    }
}
