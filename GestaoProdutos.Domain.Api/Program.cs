using Microsoft.EntityFrameworkCore;
using GestaoProdutos.Domain.Api.Middlewares;
using GestaoProdutos.Domain.Api.Configurations;
using GestaoProdutos.Domain.IoC;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using GestaoProdutos.Application.Validators;
using GestaoProdutos.Application;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder
    .Services
    .AddFluentValidationAutoValidation(opt =>
    {
        opt.OverrideDefaultResultFactoryWith<CustomResultFactory>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddDatabaseConfiguration(configuration);
builder.Services.AddAutoMapperConfiguration();

builder.Services.AddRepositoriesCollection();
builder.Services.AddValidatorsCollection();
builder.Services.AddHandlersCollection();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MigrationInitialisation();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
