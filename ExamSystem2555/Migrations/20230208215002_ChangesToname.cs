using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class ChangesToname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams",
                newName: "CandidateExamId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams",
                newName: "IX_MarkerAssignedExams_CandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExamId",
                table: "MarkerAssignedExams",
                column: "CandidateExamId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExamId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "CandidateExamId",
                table: "MarkerAssignedExams",
                newName: "CandidateExaminationCandidateExamId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_CandidateExamId",
                table: "MarkerAssignedExams",
                newName: "IX_MarkerAssignedExams_CandidateExaminationCandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams",
                column: "CandidateExaminationCandidateExamId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
