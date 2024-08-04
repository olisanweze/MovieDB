using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDB.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actors",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "director",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "genre",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "actors",
                table: "Movies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director",
                table: "Movies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "genre",
                table: "Movies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
