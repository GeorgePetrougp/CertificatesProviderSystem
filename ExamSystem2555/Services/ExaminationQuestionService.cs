using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class ExaminationQuestionService : IExaminationQuestionService
    {
        private IAsyncGenericRepository<ExaminationQuestion> _examinationQuestionRepository;

        public ExaminationQuestionService(IAsyncGenericRepository<ExaminationQuestion> examinationQuestionRepository)
        {
            _examinationQuestionRepository = examinationQuestionRepository;
        }

        public async Task<ExaminationQuestion> GetExaminationQuestionByIdAsync(int? id)
        {
            return await _examinationQuestionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ExaminationQuestion>> GetAllExaminationQuestionsAsync()
        {
            return await _examinationQuestionRepository.GetAllAsync();
        }

        public async Task<ExaminationQuestion> AddExaminationQuestionAsync(ExaminationQuestion examinationQuestion)
        {
            return await _examinationQuestionRepository.AddAsync(examinationQuestion);
        }

        public async Task<ExaminationQuestion> UpdateExaminationQuestionAsync(ExaminationQuestion examinationQuestion)
        {
            return await (_examinationQuestionRepository.UpdateAsync(examinationQuestion));
        }

        public async Task DeleteExaminationQuestionAsync(int? id)
        {
            await _examinationQuestionRepository.DeleteAsync(id);
        }

        public string CheckNull(ExaminationQuestion examinationQuestion)
        {
            if (examinationQuestion != null)
            {
                return "OK";
            }

            return null;
        }
    }
}
