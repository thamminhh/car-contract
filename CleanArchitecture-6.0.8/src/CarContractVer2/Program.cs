
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Application.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors;
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
builder.Services.AddScoped<IContractFileRepository, ContractFileRepository>();
builder.Services.AddScoped<IExpertiseContractRepository, ExpertiseContractRepository>();
builder.Services.AddScoped<IRentContractRepository, RentContractRepository>();
builder.Services.AddScoped<ITransferContractRepository, TransferContractRepository>();
builder.Services.AddScoped<IReceiveContractRepository, ReceiveContractRepository>();
builder.Services.AddScoped<ICarStatusRepository, CarStatusRepository>();
builder.Services.AddScoped<IContractGroupStatusRepository, ContractGroupStatusRepository>();

//connection string 
builder.Services.AddDbContext<ContractContext>(otp => otp.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

        app.UseStaticFiles();

        app.UseCors("AllowAllOrigins");

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
