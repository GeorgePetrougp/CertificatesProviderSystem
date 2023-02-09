using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class ChangeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Levels_LevelId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamCandidateAnswers_CandidateExams_CandidateExamId",
                table: "ExamCandidateAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExamId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "CandidateExamId",
                table: "MarkerAssignedExams",
                newName: "CandidateExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_CandidateExamId",
                table: "MarkerAssignedExams",
                newName: "IX_MarkerAssignedExams_CandidateExaminationId");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Levels",
                newName: "CertificateLevelId");

            migrationBuilder.RenameColumn(
                name: "ExamQuestionId",
                table: "ExamQuestions",
                newName: "ExaminationQuestionId");

            migrationBuilder.RenameColumn(
                name: "CandidateExamId",
                table: "ExamCandidateAnswers",
                newName: "CandidateExaminationId");

            migrationBuilder.RenameColumn(
                name: "ExamCandidateAnswerId",
                table: "ExamCandidateAnswers",
                newName: "CandidateExaminationAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamCandidateAnswers_CandidateExamId",
                table: "ExamCandidateAnswers",
                newName: "IX_ExamCandidateAnswers_CandidateExaminationId");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Certificates",
                newName: "LevelCertificateLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_LevelId",
                table: "Certificates",
                newName: "IX_Certificates_LevelCertificateLevelId");

            migrationBuilder.RenameColumn(
                name: "CandidateExamId",
                table: "CandidateExams",
                newName: "CandidateExaminationId");

            migrationBuilder.RenameColumn(
                name: "CandidateExamResultsId",
                table: "CandidateExamResults",
                newName: "CandidateExaminationResultId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "CandidateAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Levels_LevelCertificateLevelId",
                table: "Certificates",
                column: "LevelCertificateLevelId",
                principalTable: "Levels",
                principalColumn: "CertificateLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCandidateAnswers_CandidateExams_CandidateExaminationId",
                table: "ExamCandidateAnswers",
                column: "CandidateExaminationId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExaminationId",
                table: "MarkerAssignedExams",
                column: "CandidateExaminationId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExaminationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Levels_LevelCertificateLevelId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamCandidateAnswers_CandidateExams_CandidateExaminationId",
                table: "ExamCandidateAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExaminationId",
                table: "MarkerAssignedExams");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationId",
                table: "MarkerAssignedExams",
                newName: "CandidateExamId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerAssignedExams_CandidateExaminationId",
                table: "MarkerAssignedExams",
                newName: "IX_MarkerAssignedExams_CandidateExamId");

            migrationBuilder.RenameColumn(
                name: "CertificateLevelId",
                table: "Levels",
                newName: "LevelId");

            migrationBuilder.RenameColumn(
                name: "ExaminationQuestionId",
                table: "ExamQuestions",
                newName: "ExamQuestionId");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationId",
                table: "ExamCandidateAnswers",
                newName: "CandidateExamId");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationAnswerId",
                table: "ExamCandidateAnswers",
                newName: "ExamCandidateAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamCandidateAnswers_CandidateExaminationId",
                table: "ExamCandidateAnswers",
                newName: "IX_ExamCandidateAnswers_CandidateExamId");

            migrationBuilder.RenameColumn(
                name: "LevelCertificateLevelId",
                table: "Certificates",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_LevelCertificateLevelId",
                table: "Certificates",
                newName: "IX_Certificates_LevelId");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationId",
                table: "CandidateExams",
                newName: "CandidateExamId");

            migrationBuilder.RenameColumn(
                name: "CandidateExaminationResultId",
                table: "CandidateExamResults",
                newName: "CandidateExamResultsId");

            migrationBuilder.RenameColumn(
                name: "CandidateAddressId",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Levels_LevelId",
                table: "Certificates",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCandidateAnswers_CandidateExams_CandidateExamId",
                table: "ExamCandidateAnswers",
                column: "CandidateExamId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerAssignedExams_CandidateExams_CandidateExamId",
                table: "MarkerAssignedExams",
                column: "CandidateExamId",
                principalTable: "CandidateExams",
                principalColumn: "CandidateExamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
