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
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IGenericRepository<Question>, QuestionRepository>();
            builder.Services.AddScoped<IGenericRepository<QuestionDifficulty>, QuestionDifficultyRepository>();
            builder.Services.AddScoped<IGenericRepository<QuestionPossibleAnswer>, QuestionPossibleAnswerRepository>();
            builder.Services.AddScoped<IGenericRepository<Topic>, TopicRepository>();
            builder.Services.AddScoped<IGenericRepository<TopicQuestion>, TopicQuestionRepository>();
            builder.Services.AddScoped<IGenericRepository<CertificateTopicQuestion>, CertificateTopicQuestionRepository>();
            builder.Services.AddScoped<IGenericRepository<Certificate>, CertificateRepository>();
            builder.Services.AddScoped<IGenericRepository<ExamCandidateAnswer>, ExamCandidateAnswerRepository>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionDifficultyService, QuestionDifficultyService>();
            builder.Services.AddScoped<IQuestionPossibleAnswerService, QuestionPossibleAnswerService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            builder.Services.AddScoped<ICertificateTopicQuestionService, CertificateTopicQuestionService>();
            builder.Services.AddScoped<ICertificateService, CertificateService>();
            builder.Services.AddScoped<IQuestionViewService, QuestionViewService>();
            builder.Services.AddScoped<IQuestionManagerService, QuestionManagerService>();
            builder.Services.AddScoped<IExamCandidateAnswerService, ExamCandidateAnswerService>();

          

            builder.Services.AddScoped<IExamManagerService, ExamManagerService>();





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