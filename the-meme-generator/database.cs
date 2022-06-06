using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace the_meme_generator
{
    public class DatabaseContext : DbContext
    {
        public DbSet<users> Users { get; set; }
        public DbSet<playlista> Playlista { get; set; }
        public DbSet<utwory> Utwory { get; set; }
        public DbSet<wykonawcy> Wykonawcy { get; set; }
        public DbSet<albumy> Albumy { get; set; }

        public string DbPath { get; }
        public DatabaseContext ()
        {
            var szrotifyDb = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(szrotifyDb);
            DbPath = System.IO.Path.Join(path, "szrotifyDb.db");
        }
    }
}
