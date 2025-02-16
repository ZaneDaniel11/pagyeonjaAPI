using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using pagyeonjaAPI.Controllers;
using Microsoft.Extensions.FileProviders;
using Pagyeonja.Entities.Entities;
using PagyeonjaServices.Services;
using Pagyeonja.Repositories.Repositories;
using Pagyeonja.Services.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HitchContext>();
builder.Services.AddScoped<ICommuterService, CommuterService>();
builder.Services.AddScoped<ICommuterRepository, CommuterRepository>();
builder.Services.AddScoped<ApprovalService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<IDatabaseTransactionRepository, DatabaseTransactionRepository>();
builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
builder.Services.AddScoped<IRiderService, RiderService>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<RiderService>();
builder.Services.AddScoped<ISuspensionService, SuspensionService>();
builder.Services.AddScoped<ISuspensionRepository, SuspensionRepository>();
builder.Services.AddScoped<IUpdateSuspensionService, UpdateSuspensionService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRideHistoryService, RideHistoryService>();
builder.Services.AddScoped<IRideHistoryRepository, RideHistoryRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ITopupHistoryService, TopupHistoryService>();
builder.Services.AddScoped<ITopupHistoryRepository, TopupHistoryRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }
);

// Add static files service
builder.Services.AddDirectoryBrowser();

builder.Services.AddHostedService<BackgroundServiceSuspension>();

var app = builder.Build();

// Use static files middleware
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = ""
});

app.UseCors();

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
