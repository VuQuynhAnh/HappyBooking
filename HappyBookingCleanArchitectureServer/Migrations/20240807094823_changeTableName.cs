using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyBookingCleanArchitectureServer.Migrations
{
    /// <inheritdoc />
    public partial class changeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageRepository",
                table: "MessageRepository");

            migrationBuilder.RenameTable(
                name: "MessageRepository",
                newName: "Message");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "MessageRepository");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageRepository",
                table: "MessageRepository",
                column: "MessageId");
        }
    }
}
