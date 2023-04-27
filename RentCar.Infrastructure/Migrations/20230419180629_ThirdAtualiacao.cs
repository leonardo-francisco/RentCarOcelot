using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Infrastructure.Migrations
{
    public partial class ThirdAtualiacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Carros",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Carros");
        }
    }
}
