using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamBuilder.Business;
using ExamBuilder.DataAccess.Context;
using ExamBuilder.DataAccess.Repositories;
using ExamBuilder.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExamBuilder.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<ExamBuilderDbContext>();
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(
                options =>
                    options.IdleTimeout = TimeSpan.FromMinutes(30));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IWiredBusiness, WiredBusiness>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExamBuilderDbContext, ExamBuilderDbContext>();

            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IExamBusiness, ExamBusiness>();
            services.AddScoped<IQuestionBusiness, QuestionBusiness>();
            services.AddScoped<IOptionBusiness, OptionBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope =
                        app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<ExamBuilderDbContext>())
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
