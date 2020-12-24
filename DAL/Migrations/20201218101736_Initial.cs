using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "William", "Shakespeare" },
                    { 10, "Gilbert", "Patten" },
                    { 8, "Alexander", "Pushkin" },
                    { 7, "Corín", "Tellado" },
                    { 6, "Georges", "Simenon" },
                    { 9, "Gilbert", "Patten" },
                    { 4, "Kyotaro", "Nishimura" },
                    { 3, "Barbara", "Cartland" },
                    { 2, "Agatha", "Christie" },
                    { 5, "Danielle", "Steel" }
                });

            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "Id", "BookId", "ReaderId" },
                values: new object[,]
                {
                    { 26, 19, 5 },
                    { 21, 15, 4 },
                    { 22, 17, 4 },
                    { 23, 16, 5 },
                    { 24, 17, 5 },
                    { 25, 18, 5 },
                    { 27, 20, 5 },
                    { 32, 7, 6 },
                    { 29, 1, 6 },
                    { 30, 4, 6 },
                    { 31, 9, 6 },
                    { 20, 8, 4 },
                    { 33, 2, 6 },
                    { 34, 6, 6 },
                    { 28, 17, 5 },
                    { 19, 4, 4 },
                    { 18, 14, 3 },
                    { 4, 1, 1 },
                    { 1, 2, 1 },
                    { 2, 3, 1 },
                    { 3, 6, 1 },
                    { 17, 13, 3 },
                    { 5, 3, 1 },
                    { 7, 20, 2 },
                    { 8, 11, 2 },
                    { 6, 4, 1 },
                    { 10, 5, 3 },
                    { 11, 2, 3 },
                    { 12, 7, 3 },
                    { 13, 9, 3 },
                    { 14, 10, 3 },
                    { 15, 11, 3 }
                });

            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "Id", "BookId", "ReaderId" },
                values: new object[,]
                {
                    { 16, 12, 3 },
                    { 9, 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Poetry" },
                    { 2, "Fiction" },
                    { 3, "Mystery" },
                    { 4, "Detective" },
                    { 5, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "DoB", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 4, new DateTime(1969, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dale", "Pereira", "248-744-7065" },
                    { 3, new DateTime(1976, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Merri", "Rosborough", "423-473-0603" },
                    { 5, new DateTime(1971, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Larry", "208-530-9934" },
                    { 1, new DateTime(1965, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Catherine ", "McGinley", "719-557-7626" },
                    { 2, new DateTime(1984, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jim", "Hyde", "517-663-4353" },
                    { 6, new DateTime(1988, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Richard", "Taylor", "913-403-7491" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "GenreId", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, 5, "Shakespeare's Sonnets" },
                    { 8, 5, 5, 3, "Zoya" },
                    { 7, 3, 5, 4, "A Ghost in Monte Carlo" },
                    { 6, 3, 5, 3, "Romantic Royal Marriages" },
                    { 5, 3, 5, 0, "Duel of Hearts" },
                    { 13, 6, 4, 5, "The Strange Case of Peter the Lett" },
                    { 12, 6, 4, 3, "Maigret" },
                    { 4, 2, 4, 6, "Jane Marple" },
                    { 3, 2, 4, 1, "Hercule Poirot" },
                    { 11, 4, 3, 8, "Kisei Honsen Satsujin Jiken" },
                    { 10, 4, 3, 3, "The Mystery Train Disappears" },
                    { 20, 10, 2, 3, "The Oil Prince" },
                    { 19, 10, 2, 5, "Winnetou" },
                    { 18, 9, 2, 1, "Boltwood of Yale" },
                    { 17, 9, 2, 6, "Frank Merriwell" },
                    { 16, 8, 1, 4, "The Gypsies" },
                    { 15, 8, 1, 2, "Poltava" },
                    { 2, 1, 1, 2, "Shall I compare thee to a summer's day?" },
                    { 9, 5, 5, 1, "Sisters" },
                    { 14, 7, 5, 0, "Tu pasado me condena" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
