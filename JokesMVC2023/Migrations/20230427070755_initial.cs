using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesMVC2023.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JokeQuestion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JokeAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jokes");
        }
    }
}
