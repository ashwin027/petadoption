using Microsoft.EntityFrameworkCore.Migrations;

namespace PetAdoption.Repository.Migrations
{
    public partial class Minorchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdopterDetailId",
                table: "Adoptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdopterDetailId",
                table: "Adoptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
