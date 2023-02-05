using MyDatabase.Models;
using WebApp.Data;
using WebApp.Repositories;
using WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.MainServices;
using AutoMapper;
using WebApp.DTO_Models;

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

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAsyncGenericRepository<Question>, QuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<QuestionDifficulty>, QuestionDifficultyRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<QuestionPossibleAnswer>, QuestionPossibleAnswerRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Topic>, TopicRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<TopicQuestion>, TopicQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CertificateTopicQuestion>, CertificateTopicQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CertificateTopic>, CertificateTopicRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Certificate>, CertificateRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Candidate>, CandidateRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<ExamCandidateAnswer>, ExamCandidateAnswerRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<ExaminationQuestion>, ExaminationQuestionRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<Examination>, ExaminationRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CandidateExam>, CandidateExamRepository>();
            builder.Services.AddScoped<IAsyncGenericRepository<CandidateExamResults>, CandidateExamResultsRepository>();




            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionDifficultyService, QuestionDifficultyService>();
            builder.Services.AddScoped<IQuestionPossibleAnswerService, QuestionPossibleAnswerService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<ICertificateTopicService, CertificateTopicService>();
            builder.Services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            builder.Services.AddScoped<ICertificateTopicQuestionService, CertificateTopicQuestionService>();
            builder.Services.AddScoped<ICertificateService, CertificateService>();
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<IQuestionViewService, QuestionViewService>();
            builder.Services.AddScoped<IQuestionManagerService, QuestionManagerService>();
            builder.Services.AddScoped<IExaminationService, ExaminationService>();
            builder.Services.AddScoped<IExamCandidateAnswerService, ExamCandidateAnswerService>();
            builder.Services.AddScoped<IExaminationQuestionService, ExaminationQuestionService>();
            builder.Services.AddScoped<IExamManagerService, ExamManagerService>();
            builder.Services.AddScoped<ICandidateExamService, CandidateExamService>();
            builder.Services.AddScoped<ICandidateExamResultsService, CandidateExamResultsService>();



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