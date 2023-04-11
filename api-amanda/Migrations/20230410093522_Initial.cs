using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_amanda.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CsvFiles",
                columns: table => new
                {
                    csvFileId = table.Column<string>(type: "text", nullable: false),
                    csvFileName = table.Column<string>(type: "text", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: false),
                    uploadDate = table.Column<string>(type: "text", nullable: false),
                    firstTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    lastTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    fileTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsvFiles", x => x.csvFileId);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    recordId = table.Column<string>(type: "text", nullable: false),
                    csvFileId = table.Column<string>(type: "text", nullable: false),
                    mcc = table.Column<string>(type: "text", nullable: false),
                    mnc = table.Column<string>(type: "text", nullable: false),
                    lac = table.Column<string>(type: "text", nullable: false),
                    cellid = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<decimal>(type: "numeric", nullable: false),
                    lon = table.Column<decimal>(type: "numeric", nullable: false),
                    signal = table.Column<string>(type: "text", nullable: false),
                    measured_at = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<string>(type: "text", nullable: false),
                    speed = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    act = table.Column<string>(type: "text", nullable: false),
                    ta = table.Column<string>(type: "text", nullable: false),
                    psc = table.Column<string>(type: "text", nullable: false),
                    tac = table.Column<string>(type: "text", nullable: false),
                    pci = table.Column<string>(type: "text", nullable: false),
                    sid = table.Column<string>(type: "text", nullable: false),
                    nid = table.Column<string>(type: "text", nullable: false),
                    bid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.recordId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CsvFiles");

            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
