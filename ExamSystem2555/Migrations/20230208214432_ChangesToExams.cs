using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class ChangesToExams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_Examinations_ExaminationId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "ExaminationId",
                table: "MarkerAssignedExams",
                newName: "CandidateExaminationCandidateExamId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_ExaminationId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams",
                newName: "ExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_CandidateExaminationCandidateExamId",
                table: "MarkerAssignedExams",
                newName: "IX_MarkerAssignedExams_ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerAssignedExams_Examinations_ExaminationId",
                table: "MarkerAssignedExams",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "ExaminationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
