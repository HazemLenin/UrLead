using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrLead.Data.Migrations
{
    public partial class AddLeadCategoryAndLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadCategory",
                columns: table => new
                {
                    LeadCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCategory", x => x.LeadCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    LeadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Probability = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.LeadId);
                    table.ForeignKey(
                        name: "FK_Lead_AspNetUsers_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lead_LeadCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "LeadCategory",
                        principalColumn: "LeadCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lead_CategoryId",
                table: "Lead",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_OrganizationId",
                table: "Lead",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lead");

            migrationBuilder.DropTable(
                name: "LeadCategory");
        }
    }
}
