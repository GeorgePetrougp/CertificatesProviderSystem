using MyDatabase.Models;
using WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<UserCandidate> UserCandidates { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<MarkerAssignedExam> MarkerAssignedExams { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateAddress> Addresses { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateLevel> Levels { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionDifficulty> QuestionDifficulties { get; set; }
        public virtual DbSet<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; }
        public virtual DbSet<TopicQuestion> TopicQuestions { get; set; }
        public virtual DbSet<CertificateTopic> CertificateTopics { get; set; }
        public virtual DbSet<CertificateTopicQuestion> CertificateTopicQuestions { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<CandidateExamination> CandidateExams { get; set; }
        public virtual DbSet<CandidateExaminationAnswer> ExamCandidateAnswers { get; set; }
        public virtual DbSet<ExaminationQuestion> ExamQuestions { get; set; }
        public virtual DbSet<CandidateExaminationResult> CandidateExamResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ExaminationQuestion>()
                .HasKey(e => e.ExaminationQuestionId);

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

            modelBuilder.Entity<CertificateTopic>()
                .HasMany(ct => ct.CertificateTopicQuestions)
                .WithOne(ctq => ctq.CertificateTopic)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TopicQuestion>()
                .HasMany(tq => tq.CertificateTopicQuestions)
                .WithOne(ctq => ctq.TopicQuestion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateExamination>()
        .HasOne(a => a.CandidateExamResults)
        .WithOne(b => b.CandidateExam)
        .HasForeignKey<CandidateExaminationResult>(b => b.CandidateExamId);




            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
    }
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u=>u.FirstName).HasMaxLength(200);
            builder.Property(u=>u.LastName).HasMaxLength(200);
        }
    }
}