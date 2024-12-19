
using FinancesApi.Data;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinancesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string secret = "3b2c2d6b-bec0-48f8-a93f-c4e41f18e103";

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEntityFrameworkSqlServer()
               .AddDbContext<FinancesApiDbContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinancesApiContext"))
               );

            

            builder.Services.AddControllers();
  
            builder.Services.AddOpenApi();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITransitionRepository, TransitionRepository>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "finances_op",
                    ValidAudience = "finances_app",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                }; 
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
