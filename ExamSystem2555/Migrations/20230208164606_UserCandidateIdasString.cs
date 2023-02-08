using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class UserCandidateIdasString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_UserCandidates_UserCandidateId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_UserCandidateId",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "UserCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserCandidateId",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCandidates_CandidateId",
                table: "UserCandidates",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCandidates_Candidates_CandidateId",
                table: "UserCandidates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCandidates_Candidates_CandidateId",
                table: "UserCandidates");

            migrationBuilder.DropIndex(
                name: "IX_UserCandidates_CandidateId",
                table: "UserCandidates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "UserCandidates");

            migrationBuilder.AlterColumn<int>(
                name: "UserCandidateId",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserCandidateId",
                table: "Candidates",
                column: "UserCandidateId",
                unique: true,
                filter: "[UserCandidateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_UserCandidates_UserCandidateId",
                table: "Candidates",
                column: "UserCandidateId",
                principalTable: "UserCandidates",
                principalColumn: "UserCandidateId");
        }
    }
}
