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
            DbPath = System.IO.Path.Join(path, "szrotifyDb.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            Console.WriteLine(DbPath);
            dbContextOptionsBuilder.UseSqlite($"DataSource={DbPath}");
        }
    }


    public class users
    {
        public int id_user { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
    }
    public class playlista
    {
        public int id_playlisty { get; set; }
        public int id_user { get; set; }
        public string nazwa_playlisty { get; set; }
        public int id_utworu { get; set; }
    }
    public class utwory
    {
        public int id_utworu { get; set; }
        public int id_wykonawcy { get; set; }
        public int id_albumu { get; set; }
        public string tytul { get; set; }
        public string czas_utworu { get; set; }
        public DateTime data_dodania { get; set; }
        public int lista_odtworzen { get; set; }
    }
    public class wykonawcy
    {
        public int id_wykonawcy { get; set; }
        public string wykonawca { get; set; }
        public DateTime rozpoczecie_kariery { get; set; }
        public int wydanych_albumow { get; set; }
    }
    public class albumy
    {
        public int id_albumu { get; set; }
        public string album { get; set; }
        public DateTime data_wydania { get; set; }
        public int liczba_utworow { get; set; }
    }
}
