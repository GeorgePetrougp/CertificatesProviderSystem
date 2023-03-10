// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyDatabase.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateId"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CountryOfResidence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LandlineNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NativeLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PhotoIdIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoIdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoIdType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCandidateId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CandidateId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateAddress", b =>
                {
                    b.Property<int>("CandidateAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateAddressId"), 1L, 1);

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CandidateAddressId");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateAddresses");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExamination", b =>
                {
                    b.Property<int>("CandidateExaminationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateExaminationId"), 1L, 1);

                    b.Property<int?>("CandidateId")
                        .HasColumnType("int");

                    b.Property<string>("ExamCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExaminationId")
                        .HasColumnType("int");

                    b.HasKey("CandidateExaminationId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ExaminationId");

                    b.ToTable("CandidateExaminations");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExaminationAnswer", b =>
                {
                    b.Property<int>("CandidateExaminationAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateExaminationAnswerId"), 1L, 1);

                    b.Property<int?>("CandidateExaminationId")
                        .HasColumnType("int");

                    b.Property<int?>("CertificateTopicQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("CorrectAnswer")
                        .HasColumnType("int");

                    b.Property<int>("PointsAssignedAfterMarking")
                        .HasColumnType("int");

                    b.Property<int>("PointsAssignedDuringExamination")
                        .HasColumnType("int");

                    b.Property<int>("SelectedAnswer")
                        .HasColumnType("int");

                    b.HasKey("CandidateExaminationAnswerId");

                    b.HasIndex("CandidateExaminationId");

                    b.HasIndex("CertificateTopicQuestionId");

                    b.ToTable("CandidateExaminationAnswers");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExaminationResult", b =>
                {
                    b.Property<int>("CandidateExaminationResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateExaminationResultId"), 1L, 1);

                    b.Property<int>("CandidateExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("CandidateTotalScore")
                        .HasColumnType("int");

                    b.Property<string>("HasBeenRemarked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ResultIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResultLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CandidateExaminationResultId");

                    b.HasIndex("CandidateExaminationId")
                        .IsUnique();

                    b.ToTable("CandidateExamResults");
                });

            modelBuilder.Entity("MyDatabase.Models.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelCertificateLevelId")
                        .HasColumnType("int");

                    b.Property<int>("PassMark")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CertificateId");

                    b.HasIndex("LevelCertificateLevelId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateLevel", b =>
                {
                    b.Property<int>("CertificateLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateLevelId"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CertificateLevelId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopic", b =>
                {
                    b.Property<int>("CertificateTopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateTopicId"), 1L, 1);

                    b.Property<int>("CertificateId")
                        .HasColumnType("int");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("CertificateTopicId");

                    b.HasIndex("CertificateId");

                    b.HasIndex("TopicId");

                    b.ToTable("CertificateTopics");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopicQuestion", b =>
                {
                    b.Property<int>("CertificateTopicQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateTopicQuestionId"), 1L, 1);

                    b.Property<int?>("CertificateTopicId")
                        .HasColumnType("int");

                    b.Property<int?>("TopicQuestionId")
                        .HasColumnType("int");

                    b.HasKey("CertificateTopicQuestionId");

                    b.HasIndex("CertificateTopicId");

                    b.HasIndex("TopicQuestionId");

                    b.ToTable("CertificateTopicQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.Examination", b =>
                {
                    b.Property<int>("ExaminationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExaminationId"), 1L, 1);

                    b.Property<int>("CertificateId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExaminationId");

                    b.HasIndex("CertificateId");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("MyDatabase.Models.ExaminationQuestion", b =>
                {
                    b.Property<int>("ExaminationQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExaminationQuestionId"), 1L, 1);

                    b.Property<int>("CertificateTopicQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("int");

                    b.HasKey("ExaminationQuestionId");

                    b.HasIndex("CertificateTopicQuestionId");

                    b.HasIndex("ExaminationId");

                    b.ToTable("ExaminationQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"), 1L, 1);

                    b.Property<string>("Display")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionDifficultyId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionDifficultyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MyDatabase.Models.QuestionDifficulty", b =>
                {
                    b.Property<int>("QuestionDifficultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionDifficultyId"), 1L, 1);

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionDifficultyId");

                    b.ToTable("QuestionDifficulties");
                });

            modelBuilder.Entity("MyDatabase.Models.QuestionPossibleAnswer", b =>
                {
                    b.Property<int>("QuestionPossibleAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionPossibleAnswerId"), 1L, 1);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("PossibleAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("QuestionPossibleAnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionPossibleAnswers");
                });

            modelBuilder.Entity("MyDatabase.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("MyDatabase.Models.TopicQuestion", b =>
                {
                    b.Property<int>("TopicQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicQuestionId"), 1L, 1);

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("TopicQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicQuestions");
                });

            modelBuilder.Entity("WebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebApp.Models.MarkerAssignedExam", b =>
                {
                    b.Property<int>("MarkerAssignedExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MarkerAssignedExamId"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CandidateExaminationId")
                        .HasColumnType("int");

                    b.HasKey("MarkerAssignedExamId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CandidateExaminationId");

                    b.ToTable("MarkerAssignedExams");
                });

            modelBuilder.Entity("WebApp.Models.UserCandidate", b =>
                {
                    b.Property<int>("UserCandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserCandidateId"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.HasKey("UserCandidateId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CandidateId");

                    b.ToTable("UserCandidates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateAddress", b =>
                {
                    b.HasOne("MyDatabase.Models.Candidate", "Candidate")
                        .WithMany("Addresses")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExamination", b =>
                {
                    b.HasOne("MyDatabase.Models.Candidate", "Candidate")
                        .WithMany("CandidateExams")
                        .HasForeignKey("CandidateId");

                    b.HasOne("MyDatabase.Models.Examination", "Examination")
                        .WithMany("CandidateExams")
                        .HasForeignKey("ExaminationId");

                    b.Navigation("Candidate");

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExaminationAnswer", b =>
                {
                    b.HasOne("MyDatabase.Models.CandidateExamination", "CandidateExam")
                        .WithMany("ExamCandidateAnswers")
                        .HasForeignKey("CandidateExaminationId");

                    b.HasOne("MyDatabase.Models.CertificateTopicQuestion", "CertificateTopicQuestion")
                        .WithMany("ExamCandidateAnswers")
                        .HasForeignKey("CertificateTopicQuestionId");

                    b.Navigation("CandidateExam");

                    b.Navigation("CertificateTopicQuestion");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExaminationResult", b =>
                {
                    b.HasOne("MyDatabase.Models.CandidateExamination", "CandidateExam")
                        .WithOne("CandidateExamResults")
                        .HasForeignKey("MyDatabase.Models.CandidateExaminationResult", "CandidateExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CandidateExam");
                });

            modelBuilder.Entity("MyDatabase.Models.Certificate", b =>
                {
                    b.HasOne("MyDatabase.Models.CertificateLevel", "Level")
                        .WithMany("Certificates")
                        .HasForeignKey("LevelCertificateLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopic", b =>
                {
                    b.HasOne("MyDatabase.Models.Certificate", "Certificate")
                        .WithMany("CertificateTopics")
                        .HasForeignKey("CertificateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDatabase.Models.Topic", "Topic")
                        .WithMany("CertificateTopics")
                        .HasForeignKey("TopicId");

                    b.Navigation("Certificate");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopicQuestion", b =>
                {
                    b.HasOne("MyDatabase.Models.CertificateTopic", "CertificateTopic")
                        .WithMany("CertificateTopicQuestions")
                        .HasForeignKey("CertificateTopicId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MyDatabase.Models.TopicQuestion", "TopicQuestion")
                        .WithMany("CertificateTopicQuestions")
                        .HasForeignKey("TopicQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("CertificateTopic");

                    b.Navigation("TopicQuestion");
                });

            modelBuilder.Entity("MyDatabase.Models.Examination", b =>
                {
                    b.HasOne("MyDatabase.Models.Certificate", "Certificate")
                        .WithMany("Examinations")
                        .HasForeignKey("CertificateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Certificate");
                });

            modelBuilder.Entity("MyDatabase.Models.ExaminationQuestion", b =>
                {
                    b.HasOne("MyDatabase.Models.CertificateTopicQuestion", "CertificateTopicQuestion")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("CertificateTopicQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDatabase.Models.Examination", "Examination")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertificateTopicQuestion");

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("MyDatabase.Models.Question", b =>
                {
                    b.HasOne("MyDatabase.Models.QuestionDifficulty", "QuestionDifficulty")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionDifficultyId");

                    b.Navigation("QuestionDifficulty");
                });

            modelBuilder.Entity("MyDatabase.Models.QuestionPossibleAnswer", b =>
                {
                    b.HasOne("MyDatabase.Models.Question", "Question")
                        .WithMany("QuestionPossibleAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MyDatabase.Models.TopicQuestion", b =>
                {
                    b.HasOne("MyDatabase.Models.Question", "Question")
                        .WithMany("TopicQuestions")
                        .HasForeignKey("QuestionId");

                    b.HasOne("MyDatabase.Models.Topic", "Topic")
                        .WithMany("TopicQuestions")
                        .HasForeignKey("TopicId");

                    b.Navigation("Question");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("WebApp.Models.MarkerAssignedExam", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDatabase.Models.CandidateExamination", "CandidateExam")
                        .WithMany()
                        .HasForeignKey("CandidateExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("CandidateExam");
                });

            modelBuilder.Entity("WebApp.Models.UserCandidate", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDatabase.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("MyDatabase.Models.Candidate", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("CandidateExams");
                });

            modelBuilder.Entity("MyDatabase.Models.CandidateExamination", b =>
                {
                    b.Navigation("CandidateExamResults");

                    b.Navigation("ExamCandidateAnswers");
                });

            modelBuilder.Entity("MyDatabase.Models.Certificate", b =>
                {
                    b.Navigation("CertificateTopics");

                    b.Navigation("Examinations");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateLevel", b =>
                {
                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopic", b =>
                {
                    b.Navigation("CertificateTopicQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.CertificateTopicQuestion", b =>
                {
                    b.Navigation("ExamCandidateAnswers");

                    b.Navigation("ExamQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.Examination", b =>
                {
                    b.Navigation("CandidateExams");

                    b.Navigation("ExamQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.Question", b =>
                {
                    b.Navigation("QuestionPossibleAnswers");

                    b.Navigation("TopicQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.QuestionDifficulty", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("MyDatabase.Models.Topic", b =>
                {
                    b.Navigation("CertificateTopics");

                    b.Navigation("TopicQuestions");
                });

            modelBuilder.Entity("MyDatabase.Models.TopicQuestion", b =>
                {
                    b.Navigation("CertificateTopicQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
