using Microsoft.EntityFrameworkCore.Migrations;

namespace PetAdoption.Repository.Migrations
{
    public partial class AdopterDetailsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Adoptions");

            migrationBuilder.AddColumn<int>(
                name: "AdopterDetailId",
                table: "Adoptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPetId",
                table: "Adoptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdopterDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    GivenName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    AdoptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdopterDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdopterDetails_Adoptions_AdoptionId",
                        column: x => x.AdoptionId,
                        principalTable: "Adoptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdopterDetails_AdoptionId",
                table: "AdopterDetails",
                column: "AdoptionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdopterDetails");

            migrationBuilder.DropColumn(
                name: "AdopterDetailId",
                table: "Adoptions");

            migrationBuilder.DropColumn(
                name: "UserPetId",
                table: "Adoptions");

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Adoptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
