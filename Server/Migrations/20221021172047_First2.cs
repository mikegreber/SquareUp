using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquareUp.Server.Migrations
{
    public partial class First2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Groups_GroupDataId",
                table: "Participant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participant",
                table: "Participant");

            migrationBuilder.RenameTable(
                name: "Participant",
                newName: "Participants");

            migrationBuilder.RenameIndex(
                name: "IX_Participant_GroupDataId",
                table: "Participants",
                newName: "IX_Participants_GroupDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Groups_GroupDataId",
                table: "Participants",
                column: "GroupDataId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Groups_GroupDataId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "Participant");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_GroupDataId",
                table: "Participant",
                newName: "IX_Participant_GroupDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participant",
                table: "Participant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Groups_GroupDataId",
                table: "Participant",
                column: "GroupDataId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
