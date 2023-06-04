using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_amanda.Migrations
{
    /// <inheritdoc />
    public partial class btsTroubleShooting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CsvBtsDTO",
                table: "CsvBtsDTO");

            migrationBuilder.RenameTable(
                name: "CsvBtsDTO",
                newName: "BtsCoordiantes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BtsCoordiantes",
                table: "BtsCoordiantes",
                column: "cellid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BtsCoordiantes",
                table: "BtsCoordiantes");

            migrationBuilder.RenameTable(
                name: "BtsCoordiantes",
                newName: "CsvBtsDTO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CsvBtsDTO",
                table: "CsvBtsDTO",
                column: "cellid");
        }
    }
}
