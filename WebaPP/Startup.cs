using BAL.BusinessLogic.Helper;
using BAL.BusinessLogic.Interface;
using DAL;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WebaPP.Repository.Helper;
using WebaPP.Repository.Interface;

namespace WebaPP
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Configure logging
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configRoot.GetSection("Logging"));

            });

            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // services.AddRazorPages();

            services.AddSingleton<IEmployeeRepo, EmployeeRepository>();
            services.AddTransient<IEmplopyeeHelper, EmplopyeeHelper>();
            services.AddSingleton<IsqlDataHelper, SqlDataHelper>();
            
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
               
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
