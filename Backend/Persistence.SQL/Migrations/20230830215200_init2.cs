using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.SQL.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_AppointmentId",
                table: "Participant");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Participant",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Participant_AppointmentId",
                table: "Participant",
                newName: "IX_Participant_SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Participant",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Participant_SessionId",
                table: "Participant",
                newName: "IX_Participant_AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Sessions_AppointmentId",
                table: "Participant",
                column: "AppointmentId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
