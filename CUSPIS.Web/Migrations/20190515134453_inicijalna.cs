using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSPIS.Web.Migrations
{
    public partial class inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mjesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjesta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KontaktOosobe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    MjestoId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    JMBG = table.Column<string>(nullable: true),
                    TajniBroj = table.Column<string>(nullable: true),
                    PIB = table.Column<string>(nullable: true),
                    MaticniBroj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KontaktOosobe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KontaktOosobe_Mjesta_MjestoId",
                        column: x => x.MjestoId,
                        principalTable: "Mjesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KontaktOosobe_MjestoId",
                table: "KontaktOosobe",
                column: "MjestoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KontaktOosobe");

            migrationBuilder.DropTable(
                name: "Mjesta");
        }
    }
}
