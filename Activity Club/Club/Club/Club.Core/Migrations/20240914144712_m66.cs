using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club.Core.Migrations
{
    /// <inheritdoc />
    public partial class m66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Lookups_lookupOrder",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_lookupOrder",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "lookupOrder",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Events",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "Events",
                newName: "Cost");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Category",
                table: "Events",
                column: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Lookups_Category",
                table: "Events",
                column: "Category",
                principalTable: "Lookups",
                principalColumn: "Order",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Lookups_Category",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Category",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Events",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Events",
                newName: "cost");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "lookupOrder",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_lookupOrder",
                table: "Events",
                column: "lookupOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Lookups_lookupOrder",
                table: "Events",
                column: "lookupOrder",
                principalTable: "Lookups",
                principalColumn: "Order");
        }
    }
}
