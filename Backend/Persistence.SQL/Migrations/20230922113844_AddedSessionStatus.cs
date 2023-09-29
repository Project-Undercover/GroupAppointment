using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.SQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedSessionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Sessions",
                newName: "isVisible");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "isVisible",
                table: "Sessions",
                newName: "IsAvailable");
        }
    }
}
