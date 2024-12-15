
using FinancesApi.Data;
using FinancesApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEntityFrameworkSqlServer()
               .AddDbContext<FinancesApiDbContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinancesApiContext"))
               );

            

            builder.Services.AddControllers();
  
            builder.Services.AddOpenApi();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

       
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "weather api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
