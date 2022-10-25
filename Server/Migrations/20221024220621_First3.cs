using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquareUp.Server.Migrations
{
    public partial class First3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecondaryParticipantId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryParticipantId",
                table: "Transactions");
        }
    }
}
