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
        public DatabaseContext()
        {
            var szrotifyDb = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(szrotifyDb);
            DbPath = System.IO.Path.Join(path, "bazaDoSzrotify\\szrotifyDb.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            Console.WriteLine(DbPath);
            dbContextOptionsBuilder.UseSqlite($"DataSource={DbPath}");
        }
    }


    public class users
    {
        public int ID { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public ICollection<playlista> playlista { get; set; }
    }
    public class playlista
    {
        public int ID { get; set; }
        public users users { get; set; }
        public string nazwa_playlisty { get; set; }
        public utwory utwor { get; set; }
    }
    public class utwory
    {
        public int ID { get; set; }
        public wykonawcy wykonawcy { get; set; }
        public albumy albumy { get; set; }  
        public string tytul { get; set; }
        public string czas_utworu { get; set; }
        public DateTime data_dodania { get; set; }
        public int lista_odtworzen { get; set; }
    }
    public class wykonawcy
    {
        public int ID { get; set; }
        public string wykonawca { get; set; }
        public DateTime rozpoczecie_kariery { get; set; }
        public int wydanych_albumow { get; set; }
        public ICollection<utwory> utwory { get; set; }
    }
    public class albumy
    {
        public int ID { get; set; }
        public string album { get; set; }
        public DateTime data_wydania { get; set; }
        public int liczba_utworow { get; set; }
        public ICollection<utwory> utwory { get; set; }
    }
}
