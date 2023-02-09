using MyDatabase.Models;
using WebApp.Data;
using WebApp.Repositories;
using WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.MainServices;
using AutoMapper;
using WebApp.DTO_Models;
using WebApp.Models;
using WebApp.MainServices.Interfaces;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>().AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IAsyncGenericRepository<CandidateAddress>, AddressRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Candidate>, CandidateRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CandidateExamination>, CandidateExamRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CandidateExaminationResult>, CandidateExamResultsRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Certificate>, CertificateRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CertificateTopic>, CertificateTopicRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CertificateTopicQuestion>, CertificateTopicQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Examination>, ExaminationRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CandidateExaminationAnswer>, ExamCandidateAnswerRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<ExaminationQuestion>, ExaminationQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CertificateLevel>, LevelRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<MarkerAssignedExam>, MarkerAssignedExamRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Question>, QuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<QuestionDifficulty>, QuestionDifficultyRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<QuestionPossibleAnswer>, QuestionPossibleAnswerRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Topic>, TopicRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<TopicQuestion>, TopicQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<UserCandidate>, UserCandidateRepository>();



            builder.Services.AddScoped<ICandidateAddressService, CandidateAddressService>();
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<ICandidateExamService, CandidateExamService>();
            builder.Services.AddScoped<ICandidateExamResultsService, CandidateExamResultsService>();
            builder.Services.AddScoped<ICertificateService, CertificateService>();
            builder.Services.AddScoped<ICertificateTopicService, CertificateTopicService>();
            builder.Services.AddScoped<ICertificateTopicQuestionService, CertificateTopicQuestionService>();
            builder.Services.AddScoped<IExaminationService, ExaminationService>();
            builder.Services.AddScoped<IExamCandidateAnswerService, ExamCandidateAnswerService>();
            builder.Services.AddScoped<IExaminationQuestionService, ExaminationQuestionService>();
            builder.Services.AddScoped<ILevelService, LevelService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionDifficultyService, QuestionDifficultyService>();
            builder.Services.AddScoped<IQuestionPossibleAnswerService, QuestionPossibleAnswerService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            builder.Services.AddScoped<IUserCandidateService, UserCandidateService>();


            builder.Services.AddScoped<IQuestionViewService, QuestionViewService>();
            builder.Services.AddScoped<ICertificateExaminationService, CertificateExaminationService>();
            builder.Services.AddScoped<IMarkerAssignedExamService, MarkerAssignedExamService>();

            builder.Services.AddScoped<ICandidateManagerService, CandidateManagerService>();
            builder.Services.AddScoped<ICertificateManagerService, CertificateManagerService>();
            builder.Services.AddScoped<ICandidateExaminationManagerService, CandidateExaminationManagerService>();
            builder.Services.AddScoped<IQuestionManagerService, QuestionManagerService>();




            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();


        }
    }
}