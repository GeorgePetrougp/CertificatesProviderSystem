using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class UserCandidateIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UserCandidateId",
                table: "Candidates",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_UserCandidates_UserCandidateId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_UserCandidateId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "UserCandidateId",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "UserCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
