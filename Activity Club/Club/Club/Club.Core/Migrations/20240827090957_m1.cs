using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club.Core.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Order);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Guide_Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Guide_Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MbileNumber = table.Column<int>(type: "int", nullable: true),
                    EmergencyNumber = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lookupOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                    table.ForeignKey(
                        name: "FK_Events_Lookups_lookupOrder",
                        column: x => x.lookupOrder,
                        principalTable: "Lookups",
                        principalColumn: "Order");
                });

            migrationBuilder.CreateTable(
                name: "AdminManageUser",
                columns: table => new
                {
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminManageUser", x => new { x.AdminEmail, x.UserEmail });
                    table.ForeignKey(
                        name: "FK_AdminManageUser_Users_AdminEmail",
                        column: x => x.AdminEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminManageUser_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "UserManageLookup",
                columns: table => new
                {
                    LookupOrder = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserManageLookup", x => new { x.LookupOrder, x.UserEmail });
                    table.ForeignKey(
                        name: "FK_UserManageLookup_Lookups_LookupOrder",
                        column: x => x.LookupOrder,
                        principalTable: "Lookups",
                        principalColumn: "Order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserManageLookup_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuideEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    GuideEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideEvent", x => new { x.EventId, x.GuideEmail });
                    table.ForeignKey(
                        name: "FK_GuideEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideEvent_Users_GuideEmail",
                        column: x => x.GuideEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEvent", x => new { x.EventId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MemberEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEvent_Users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserJoinEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJoinEvent", x => new { x.EventId, x.UserEmail });
                    table.ForeignKey(
                        name: "FK_UserJoinEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJoinEvent_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminManageUser_UserEmail",
                table: "AdminManageUser",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Events_lookupOrder",
                table: "Events",
                column: "lookupOrder");

            migrationBuilder.CreateIndex(
                name: "IX_GuideEvent_GuideEmail",
                table: "GuideEvent",
                column: "GuideEmail");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEvent_MemberId",
                table: "MemberEvent",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJoinEvent_UserEmail",
                table: "UserJoinEvent",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_UserManageLookup_UserEmail",
                table: "UserManageLookup",
                column: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminManageUser");

            migrationBuilder.DropTable(
                name: "GuideEvent");

            migrationBuilder.DropTable(
                name: "MemberEvent");

            migrationBuilder.DropTable(
                name: "UserJoinEvent");

            migrationBuilder.DropTable(
                name: "UserManageLookup");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lookups");
        }
    }
}
