﻿using MyDatabase.Models;

namespace WebApp.DTO_Models.CandidateExaminations
{
    public class CandidateExaminationResultsDTO
    {
        public int CandidateExaminationResultId { get; set; }
        public DateTime ResultIssueDate { get; set; }
        public string ResultLabel { get; set; }
        public int CandidateTotalScore { get; set; }

    }
}
