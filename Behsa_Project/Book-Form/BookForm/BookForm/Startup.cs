using BookForm.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BookForm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configure services here
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Entity Framework and SQL Server
            services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BookForm API",
                    Description = "A simple example ASP.NET Core Web API"
                });
            });
        }

        // Configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger-UI, specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookForm API v1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(@"C:\Users\missm\Documents\BehsaProjects\Behsa_Project\Book-Form\BookForm\BookForm\Delete"),
                RequestPath = "/staticFiles"
            });

            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
