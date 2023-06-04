using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_amanda.Migrations
{
    /// <inheritdoc />
    public partial class btsTroubleShooting2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "BtsCoordinates",
                columns: table => new {
                    cellid = table.Column<string>(type: "text", nullable: false),
                    btsLat = table.Column<decimal>(type: "numeric", nullable: false),
                    btsLon = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BtsCoordinates", x => x.cellid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "BtsCoordinates");
        }
    }
}
