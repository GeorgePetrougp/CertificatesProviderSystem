using MyDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Data
{
    public class AppDBContext : DbContext
    {
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionDifficulty> QuestionDifficulties { get; set; }
        public virtual DbSet<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; }
        public virtual DbSet<TopicQuestion> TopicQuestions { get; set; }
        public virtual DbSet<CertificateTopic> CertificateTopics { get; set; }
        public virtual DbSet<CertificateTopicQuestion> CertificateTopicQuestions { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<CandidateExam> CandidateExams { get; set; }
        public virtual DbSet<ExamCandidateAnswer> ExamCandidateAnswers { get; set; }
        public virtual DbSet<ExaminationQuestion> ExamQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExaminationQuestion>()
                .HasKey(e => e.ExamQuestionId);

            modelBuilder.Entity<Examination>()
                .HasMany(e => e.ExamQuestions)
                .WithOne(eq => eq.Examination)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CertificateTopicQuestion>()
                .HasMany(ctq => ctq.ExamQuestions)
                .WithOne(eq => eq.CertificateTopicQuestion)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<CertificateTopicQuestion>()
                .HasKey(e => e.CertificateTopicQuestionId);

            modelBuilder.Entity<CertificateTopic>()
                .HasMany(ct=>ct.CertificateTopicQuestions)
                .WithOne(ctq=> ctq.CertificateTopic)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TopicQuestion>()
                .HasMany(tq => tq.CertificateTopicQuestions)
                .WithOne(ctq => ctq.TopicQuestion)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TopicQuestion>()
                .HasKey(e => e.TopicQuestionId);

            modelBuilder.Entity<Topic>()
                .HasMany(ct => ct.TopicQuestions)
                .WithOne(ctq => ctq.Topic)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .HasMany(tq => tq.TopicQuestions)
                .WithOne(ctq => ctq.Question)
                .OnDelete(DeleteBehavior.NoAction);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=pceplanner.anystream.eu,2555; Database=WebApp_Db4; User Id=sa;Password=SuperSecretPass2555;");
        }

    }
}
