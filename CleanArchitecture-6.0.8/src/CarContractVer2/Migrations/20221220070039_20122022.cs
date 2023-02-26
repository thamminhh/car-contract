using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarContractVer2.Migrations
{
    /// <inheritdoc />
    public partial class _20122022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarGeneration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    CarModelId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    YearBegin = table.Column<int>(type: "int", nullable: true),
                    YearEnd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CarMake",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    CarMakeId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CarSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    CarModelId = table.Column<int>(type: "int", nullable: true),
                    CarGenerationID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CarTrim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    CarModelId = table.Column<int>(type: "int", nullable: true),
                    CarSeriesId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartProductYear = table.Column<int>(type: "int", nullable: true),
                    EndProduectYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ParkingLot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ParkingL__3214EC076A37F96D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Job = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CurrentAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CitizenIdentificationInfoNumber = table.Column<string>(name: "CitizenIdentificationInfo_Number", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CitizenIdentificationInfoAddress = table.Column<string>(name: "CitizenIdentificationInfo_Address", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CitizenIdentificationInfoDateReceive = table.Column<DateTime>(name: "CitizenIdentificationInfo_DateReceive", type: "date", nullable: true),
                    PassportInfoNumber = table.Column<string>(name: "PassportInfo_Number", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PassportInfoAddress = table.Column<string>(name: "PassportInfo_Address", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PassportInfoDateReceive = table.Column<DateTime>(name: "PassportInfo_DateReceive", type: "date", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3214EC0766F7A7A3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingLotId = table.Column<int>(type: "int", nullable: true),
                    CarStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarLicensePlates = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarMake = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarModel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarGeneration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarSeries = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CarTrim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CarColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarFuel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Car__3214EC071F5DA62B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Car__ParkingLotI__395884C4",
                        column: x => x.ParkingLotId,
                        principalTable: "ParkingLot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    FrontImg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BackImg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LeftImg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RightImg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrtherImg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarFile__3214EC0724AA6BE8", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CarFile__CarId__3C34F16F",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarGenerallInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    PriceForNormalDay = table.Column<double>(type: "float", nullable: true),
                    PriceForWeekendDay = table.Column<double>(type: "float", nullable: true),
                    PriceForMonth = table.Column<double>(type: "float", nullable: true),
                    LimitedKmForMonth = table.Column<double>(type: "float", nullable: true),
                    OverLimitedMileage = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarGener__3214EC075697D240", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CarGenera__CarId__47A6A41B",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarLoanInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    CarOwnerName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RentalMethod = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RentalDate = table.Column<DateTime>(type: "date", nullable: true),
                    SpeedometerNumber = table.Column<double>(type: "float", nullable: true),
                    PriceForDay = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PriceForMonth = table.Column<double>(type: "float", nullable: true),
                    Insurance = table.Column<bool>(type: "bit", nullable: true),
                    Maintenance = table.Column<bool>(type: "bit", nullable: true),
                    LimitedKmForMonth = table.Column<double>(type: "float", nullable: true),
                    OverLimitedMileage = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarLoanI__3214EC07A5D3C9FD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CarLoanIn__CarId__41EDCAC5",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    CarStatusDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrentEtcAmount = table.Column<double>(type: "float", nullable: true),
                    FuelPercent = table.Column<int>(type: "int", nullable: true),
                    SpeedometerNumber = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarState__3214EC07170DB56A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CarState__CarId__4A8310C6",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarTracking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    LinkTracking = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TrackingUsername = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TrackingPassword = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ETCUsername = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ETCPassword = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarTrack__3214EC07AB21D9F1", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CarTracki__CarId__44CA3770",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ForControl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    LinkForControl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ForControlDay = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DayOfPayment = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ForContr__3214EC0701B9CF38", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ForContro__CarId__3F115E1A",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_ParkingLotId",
                table: "Car",
                column: "ParkingLotId");

            migrationBuilder.CreateIndex(
                name: "IX_CarFile_CarId",
                table: "CarFile",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarGenerallInfo_CarId",
                table: "CarGenerallInfo",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarLoanInfo_CarId",
                table: "CarLoanInfo",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarState_CarId",
                table: "CarState",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTracking_CarId",
                table: "CarTracking",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ForControl_CarId",
                table: "ForControl",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D10534C08195BD",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CitizenIdentificationInfo_Number",
                table: "User",
                column: "CitizenIdentificationInfo_Number",
                unique: true,
                filter: "([CitizenIdentificationInfo_Number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ_PassportInfo_Number",
                table: "User",
                column: "PassportInfo_Number",
                unique: true,
                filter: "([PassportInfo_Number] IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarFile");

            migrationBuilder.DropTable(
                name: "CarGenerallInfo");

            migrationBuilder.DropTable(
                name: "CarGeneration");

            migrationBuilder.DropTable(
                name: "CarLoanInfo");

            migrationBuilder.DropTable(
                name: "CarMake");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarSeries");

            migrationBuilder.DropTable(
                name: "CarState");

            migrationBuilder.DropTable(
                name: "CarTracking");

            migrationBuilder.DropTable(
                name: "CarTrim");

            migrationBuilder.DropTable(
                name: "ForControl");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "ParkingLot");
        }
    }
}
