using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_meme_generator.Migrations
{
    public partial class crdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albumy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    album = table.Column<string>(type: "TEXT", nullable: true),
                    data_wydania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    liczba_utworow = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(type: "TEXT", nullable: true),
                    haslo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wykonawcy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wykonawca = table.Column<string>(type: "TEXT", nullable: true),
                    rozpoczecie_kariery = table.Column<DateTime>(type: "TEXT", nullable: false),
                    wydanych_albumow = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wykonawcy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utwory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wykonawcyID = table.Column<int>(type: "INTEGER", nullable: true),
                    albumyID = table.Column<int>(type: "INTEGER", nullable: true),
                    tytul = table.Column<string>(type: "TEXT", nullable: true),
                    czas_utworu = table.Column<string>(type: "TEXT", nullable: true),
                    data_dodania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    lista_odtworzen = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utwory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utwory_Albumy_albumyID",
                        column: x => x.albumyID,
                        principalTable: "Albumy",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Utwory_Wykonawcy_wykonawcyID",
                        column: x => x.wykonawcyID,
                        principalTable: "Wykonawcy",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Playlista",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usersID = table.Column<int>(type: "INTEGER", nullable: true),
                    nazwa_playlisty = table.Column<string>(type: "TEXT", nullable: true),
                    utworID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlista", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Playlista_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Playlista_Utwory_utworID",
                        column: x => x.utworID,
                        principalTable: "Utwory",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlista_usersID",
                table: "Playlista",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_Playlista_utworID",
                table: "Playlista",
                column: "utworID");

            migrationBuilder.CreateIndex(
                name: "IX_Utwory_albumyID",
                table: "Utwory",
                column: "albumyID");

            migrationBuilder.CreateIndex(
                name: "IX_Utwory_wykonawcyID",
                table: "Utwory",
                column: "wykonawcyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlista");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Utwory");

            migrationBuilder.DropTable(
                name: "Albumy");

            migrationBuilder.DropTable(
                name: "Wykonawcy");
        }
    }
}
