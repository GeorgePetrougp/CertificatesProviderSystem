using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExamResults_CandidateExams_CandidateExamId",
                table: "CandidateExamResults");

            migrationBuilder.RenameColumn(
                name: "CandidateExamId",
                table: "CandidateExamResults",
                newName: "CandidateExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExamResults_CandidateExamId",
                table: "CandidateExamResults",
                newName: "IX_CandidateExamResults_CandidateExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExamResults_CandidateExams_CandidateExaminationId",
                table: "CandidateExamResults",
                column: "CandidateExaminationId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExaminationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExamResults_CandidateExams_CandidateExaminationId",
                table: "CandidateExamResults");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationId",
                table: "CandidateExamResults",
                newName: "CandidateExamId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExamResults_CandidateExaminationId",
                table: "CandidateExamResults",
                newName: "IX_CandidateExamResults_CandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExamResults_CandidateExams_CandidateExamId",
                table: "CandidateExamResults",
                column: "CandidateExamId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExaminationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
