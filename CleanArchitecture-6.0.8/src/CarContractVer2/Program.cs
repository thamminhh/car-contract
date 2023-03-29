
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Application.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using CleanArchitecture.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarGenerallInfoRepository, CarGenerallInfoRepository>();
builder.Services.AddScoped<ICarLoanInfoRepository, CarLoanInfoRepository>();
builder.Services.AddScoped<ICarTrackingRepository, CarTrackingRepository>();
builder.Services.AddScoped<IForControlRepository, ForControlRepository>();
builder.Services.AddScoped<ICarStateRepository, CarStateRepository>();
builder.Services.AddScoped<ICarFileRepository, CarFileRepository>();
builder.Services.AddScoped<ICarMakeRepository, CarMakeRepository>();
builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<ICarGenerationRepository, CarGenerationRepository>();
builder.Services.AddScoped<ICarSeriesRepository, CarSeriesRepository>();
builder.Services.AddScoped<ICarTrimRepository, CarTrimRepository>();
builder.Services.AddScoped<IParkingLotRepository, ParkingLotRepository>();
builder.Services.AddScoped<ICustomerInfoRepository, CustomerInfoRepository>();
builder.Services.AddScoped<IContractGroupRepository, ContractGroupRepository>();
builder.Services.AddScoped<IAppraisalRecordRepository, AppraisalRecordRepository>();
//builder.Services.AddScoped<IContractFileRepository, ContractFileRepository>();
builder.Services.AddScoped<IRentContractRepository, RentContractRepository>();
builder.Services.AddScoped<ITransferContractRepository, TransferContractRepository>();
//builder.Services.AddScoped<IReceiveContractRepository, ReceiveContractRepository>();
builder.Services.AddScoped<ICarStatusRepository, CarStatusRepository>();
builder.Services.AddScoped<IContractGroupStatusRepository, ContractGroupStatusRepository>();
builder.Services.AddScoped<ICarScheduleRepository, CarScheduleRepository>();
builder.Services.AddScoped<ICarMaintenanceInfoRepository, CarMaintenanceInfoRepository>();
builder.Services.AddScoped<ICarRegistryInfoRepository, CarRegistryInfoRepository>();
builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<ICustomerFileRepository, CustomerFileRepository>();
builder.Services.AddScoped<ITransferContractFileRepository, TransferContractFileRepository>();
builder.Services.AddScoped<ContractGroupHub>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standar Authorization header using Bearer sheme (\"beaser {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey

    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//connection string 
builder.Services.AddDbContext<ContractContext>(otp => otp.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();



// Configure the HTTP requestSystem.ArgumentException: 'Cannot instantiate implementation type 'CleanArchitecture.Domain.Interface.ICustomerFileRepository' for service type 'CleanArchitecture.Domain.Interface.ICustomerFileRepository'.' pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
        app.UseCors("AllowAllOrigins");

        app.UseRouting();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
