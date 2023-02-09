using WebApp.DTO_Models.CandidateExaminations;

namespace WebApp.DTO_Models.Marking
{
    public class MarkingView
    {
        public int CandidateExaminationId { get; set; }
        public int CertificateTopicQuestionId { get; set; }
        public CandidateExaminationAnswerDTO CandidateExaminationAnswerDTO { get; set; }

    }
}
