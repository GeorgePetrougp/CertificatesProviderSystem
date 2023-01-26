﻿using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class CertificateTopicQuestionService : ICertificateTopicQuestionService
    {
        private IAsyncGenericRepository<CertificateTopicQuestion> _certificateTopicQuestionRepository;

        public CertificateTopicQuestionService(IAsyncGenericRepository<CertificateTopicQuestion> certificateTopicQuestionRepository)
        {
            _certificateTopicQuestionRepository = certificateTopicQuestionRepository;
        }

        public async Task<CertificateTopicQuestion> GetCertificateTopicQuestionByIdAsync(int? id)
        {
            return await _certificateTopicQuestionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CertificateTopicQuestion>> GetAllCertificateTopicQuestionsAsync()
        {
            return await _certificateTopicQuestionRepository.GetAllAsync();
        }

        public async Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            return await _certificateTopicQuestionRepository.AddAsync(certificateTopicQuestion);
        }

        public async Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            return await (_certificateTopicQuestionRepository.UpdateAsync(certificateTopicQuestion));
        }

        public async Task DeleteCertificateTopicQuestionAsync(int? id)
        {
            await _certificateTopicQuestionRepository.DeleteAsync(id);
        }

        public string CheckNull(CertificateTopicQuestion certificateTopicQuestion)
        {
            if (certificateTopicQuestion != null)
            {
                return "OK";
            }

            return null;
        }
    }
}