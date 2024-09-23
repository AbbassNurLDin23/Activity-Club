using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club.Core.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guide_Photo",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MbileNumber",
                table: "Users",
                newName: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Users",
                newName: "MbileNumber");

            migrationBuilder.AddColumn<byte[]>(
                name: "Guide_Photo",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
