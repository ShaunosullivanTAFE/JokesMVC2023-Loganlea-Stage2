using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesMVC2023.Migrations
{
    public partial class FavouriteFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JokeQuestion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JokeAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteListId = table.Column<int>(type: "int", nullable: false),
                    JokeId = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteListItems_FavouriteLists_FavouriteListId",
                        column: x => x.FavouriteListId,
                        principalTable: "FavouriteLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteListItems_Jokes_JokeId",
                        column: x => x.JokeId,
                        principalTable: "Jokes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jokes",
                columns: new[] { "Id", "JokeAnswer", "JokeQuestion" },
                values: new object[,]
                {
                    { 1, "A regular expression.", "What do you get if you lock a monkey in a room with a typewriter for 8 hours?" },
                    { 2, "Put a Windows user in front of Vim and tell them to exit.", "How do you generate a random string?" },
                    { 3, "She had one-to-many relationships.", "Why did the database administrator leave his wife?" },
                    { 4, "None. It's a hardware problem.", "How many programmers does it take to screw in a light bulb?" },
                    { 5, "He keeps dropping the database.", "Why does no one like SQLrillex?" },
                    { 6, "They work below C-level.", "Why are Assembly programmers always soaking wet?" },
                    { 7, "Attire.", "What's the difference between a poorly dressed man on a unicycle and a well dressed man on a bicycle?" },
                    { 8, "Tooth hurt-y.", "What time did the man go to the dentist?" },
                    { 9, "It has an ex axis and a why axis.", "So I made a graph of all my past relationships." },
                    { 10, "They told me I wasn't putting in enough shifts.", "I just got fired from my job at the keyboard factory." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteListItems_FavouriteListId",
                table: "FavouriteListItems",
                column: "FavouriteListId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteListItems_JokeId",
                table: "FavouriteListItems",
                column: "JokeId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLists_UserId",
                table: "FavouriteLists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteListItems");

            migrationBuilder.DropTable(
                name: "FavouriteLists");

            migrationBuilder.DropTable(
                name: "Jokes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
