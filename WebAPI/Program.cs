using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Infrastructure.Memory.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebAPI.Validators;
using Infrastructure.EF;
using Infrastructure.Memory;
using Infrastructure.Services;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
            .AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddScoped<IValidator<QuizItem>, QuizItemValidator>();
            builder.Services.AddTransient<IGenericGenerator<int>, IntGenerator>();
            builder.Services.AddDbContext<QuizDbContext>();
            builder.Services.AddTransient<IQuizUserService, QuizUserServiceEF>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}