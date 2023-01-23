using MyDatabase.Models;
using WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CertificateTopicQuestion>()
                .HasMany(ctq => ctq.ExamQuestions)
                .WithOne(eq => eq.CertificateTopicQuestion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CertificateTopicQuestion>()
                .HasKey(e => e.CertificateTopicQuestionId);

            modelBuilder.Entity<Certificate>()
                .HasMany(ct => ct.CertificateTopicQuestions)
                .WithOne(ctq => ctq.Certificate)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TopicQuestion>()
                .HasMany(tq => tq.CertificateTopicQuestions)
                .WithOne(ctq => ctq.TopicQuestion)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}