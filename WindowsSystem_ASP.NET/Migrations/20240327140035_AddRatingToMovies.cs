using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WindowsSystem_ASP.NET.Migrations
{
    public partial class AddRatingToMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RunTime",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "vote_average",
                table: "Movies",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vote_average",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "RunTime",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
