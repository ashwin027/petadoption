using Microsoft.EntityFrameworkCore.Migrations;

namespace UserPetInfo.Repository.Migrations
{
    public partial class Addedgendercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserPets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvatar",
                table: "Images",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserPets");

            migrationBuilder.DropColumn(
                name: "IsAvatar",
                table: "Images");
        }
    }
}
