using BookForm;
using BookForm.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);

        // Add services to the container.
        startup.ConfigureServices(builder.Services);
        builder.Services.AddControllers();


        //Registry DbContext
        builder.Services.AddHttpClient("BookForm",
            (x =>
        {
            x.BaseAddress = new Uri("https://localhost:44370");
        }));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        startup.Configure(app, app.Environment);
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
            options.DocumentTitle = "My swagger";
        });

        app.Run();
    }
}
