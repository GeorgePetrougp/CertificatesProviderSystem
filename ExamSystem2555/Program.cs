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
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("BackupConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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

            builder.Services.AddTransient<IAsyncGenericRepository<CandidateAddress>, CandidateAddressRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<Candidate>, CandidateRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CandidateExamination>, CandidateExaminationRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CandidateExaminationResult>, CandidateExaminationResultsRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<Certificate>, CertificateRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CertificateTopic>, CertificateTopicRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CertificateTopicQuestion>, CertificateTopicQuestionRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<Examination>, ExaminationRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CandidateExaminationAnswer>, CandidateExaminationAnswerRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<ExaminationQuestion>, ExaminationQuestionRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<CertificateLevel>, CertificateLevelRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<MarkerAssignedExam>, MarkerAssignedExamRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<Question>, QuestionRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<QuestionDifficulty>, QuestionDifficultyRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<QuestionPossibleAnswer>, QuestionPossibleAnswerRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<Topic>, TopicRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<TopicQuestion>, TopicQuestionRepository>();
            builder.Services.AddTransient<IAsyncGenericRepository<UserCandidate>, UserCandidateRepository>();



            builder.Services.AddTransient<ICandidateAddressService, CandidateAddressService>();
            builder.Services.AddTransient<ICandidateService, CandidateService>();
            builder.Services.AddTransient<ICandidateExaminationService, CandidateExaminationService>();
            builder.Services.AddTransient<ICandidateExaminationResultsService, CandidateExaminationResultsService>();
            builder.Services.AddTransient<ICertificateService, CertificateService>();
            builder.Services.AddTransient<ICertificateTopicService, CertificateTopicService>();
            builder.Services.AddTransient<ICertificateTopicQuestionService, CertificateTopicQuestionService>();
            builder.Services.AddTransient<IExaminationService, ExaminationService>();
            builder.Services.AddTransient<ICandidateExaminationAnswerService, CandidateExaminationAnswerService>();
            builder.Services.AddTransient<IExaminationQuestionService, ExaminationQuestionService>();
            builder.Services.AddTransient<ICertificateLevelService, CertificateLevelService>();
            builder.Services.AddTransient<IQuestionService, QuestionService>();
            builder.Services.AddTransient<IQuestionDifficultyService, QuestionDifficultyService>();
            builder.Services.AddTransient<IQuestionPossibleAnswerService, QuestionPossibleAnswerService>();
            builder.Services.AddTransient<ITopicService, TopicService>();
            builder.Services.AddTransient<ITopicQuestionService, TopicQuestionService>();
            builder.Services.AddTransient<IUserCandidateService, UserCandidateService>();


            builder.Services.AddTransient<IQuestionViewService, QuestionViewService>();
            builder.Services.AddTransient<ICertificateExaminationService, CertificateExaminationService>();
            builder.Services.AddTransient<IMarkerAssignedExamService, MarkerAssignedExamService>();

            builder.Services.AddTransient<ICandidateManagerService, CandidateManagerService>();
            builder.Services.AddTransient<ICertificateManagerService, CertificateManagerService>();
            builder.Services.AddTransient<ICandidateExaminationManagerService, CandidateExaminationManagerService>();
            builder.Services.AddTransient<IQuestionManagerService, QuestionManagerService>();
            builder.Services.AddTransient<IEShopService, EShopService>();
            builder.Services.AddTransient<IExaminationManagerService, ExaminationManagerService>();
            builder.Services.AddTransient<IAdministratorService, AdministratorService>();





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

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();


        }
    }
}