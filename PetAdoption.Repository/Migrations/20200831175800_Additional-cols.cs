using Microsoft.EntityFrameworkCore.Migrations;

namespace PetAdoption.Repository.Migrations
{
    public partial class Additionalcols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalRequirements",
                table: "Adoptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BreedName",
                table: "Adoptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PetName",
                table: "Adoptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalRequirements",
                table: "Adoptions");

            migrationBuilder.DropColumn(
                name: "BreedName",
                table: "Adoptions");

            migrationBuilder.DropColumn(
                name: "PetName",
                table: "Adoptions");
        }
    }
}
