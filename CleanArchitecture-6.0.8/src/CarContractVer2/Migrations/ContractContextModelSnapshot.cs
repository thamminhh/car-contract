﻿// <auto-generated />
using System;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarContractVer2.Migrations
{
    [DbContext(typeof(ContractContext))]
    partial class ContractContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CarColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarFuel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarGeneration")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarLicensePlates")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarMake")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarModel")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarSeries")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarStatus")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CarTrim")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<int?>("ParkingLotId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Car__3214EC071F5DA62B");

                    b.HasIndex("ParkingLotId");

                    b.ToTable("Car", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackImg")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("FilePath")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("FrontImg")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LeftImg")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrtherImg")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RightImg")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__CarFile__3214EC0724AA6BE8");

                    b.HasIndex("CarId");

                    b.ToTable("CarFile", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarGenerallInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<double?>("LimitedKmForMonth")
                        .HasColumnType("float");

                    b.Property<double?>("OverLimitedMileage")
                        .HasColumnType("float");

                    b.Property<double?>("PriceForMonth")
                        .HasColumnType("float");

                    b.Property<double?>("PriceForNormalDay")
                        .HasColumnType("float");

                    b.Property<double?>("PriceForWeekendDay")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__CarGener__3214EC075697D240");

                    b.HasIndex("CarId");

                    b.ToTable("CarGenerallInfo", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarGeneration", b =>
                {
                    b.Property<int?>("CarModelId")
                        .HasColumnType("int");

                    b.Property<int?>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("YearBegin")
                        .HasColumnType("int");

                    b.Property<int?>("YearEnd")
                        .HasColumnType("int");

                    b.ToTable("CarGeneration", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarLoanInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarOwnerName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("Insurance")
                        .HasColumnType("bit");

                    b.Property<double?>("LimitedKmForMonth")
                        .HasColumnType("float");

                    b.Property<bool?>("Maintenance")
                        .HasColumnType("bit");

                    b.Property<double?>("OverLimitedMileage")
                        .HasColumnType("float");

                    b.Property<string>("PriceForDay")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double?>("PriceForMonth")
                        .HasColumnType("float");

                    b.Property<DateTime?>("RentalDate")
                        .HasColumnType("date");

                    b.Property<string>("RentalMethod")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double?>("SpeedometerNumber")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__CarLoanI__3214EC07A5D3C9FD");

                    b.HasIndex("CarId");

                    b.ToTable("CarLoanInfo", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarMake", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.ToTable("CarMake", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarModel", b =>
                {
                    b.Property<int?>("CarMakeId")
                        .HasColumnType("int");

                    b.Property<int?>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.ToTable("CarModel", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarSeries", b =>
                {
                    b.Property<int?>("CarGenerationId")
                        .HasColumnType("int")
                        .HasColumnName("CarGenerationID");

                    b.Property<int?>("CarModelId")
                        .HasColumnType("int");

                    b.Property<int?>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.ToTable("CarSeries");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarStatusDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("CurrentEtcAmount")
                        .HasColumnType("float");

                    b.Property<int?>("FuelPercent")
                        .HasColumnType("int");

                    b.Property<double?>("SpeedometerNumber")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__CarState__3214EC07170DB56A");

                    b.HasIndex("CarId");

                    b.ToTable("CarState", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarTracking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Etcpassword")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ETCPassword");

                    b.Property<string>("Etcusername")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ETCUsername");

                    b.Property<string>("LinkTracking")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TrackingPassword")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TrackingUsername")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__CarTrack__3214EC07AB21D9F1");

                    b.HasIndex("CarId");

                    b.ToTable("CarTracking", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarTrim", b =>
                {
                    b.Property<int?>("CarModelId")
                        .HasColumnType("int");

                    b.Property<int?>("CarSeriesId")
                        .HasColumnType("int");

                    b.Property<int?>("EndProduectYear")
                        .HasColumnType("int");

                    b.Property<int?>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("StartProductYear")
                        .HasColumnType("int");

                    b.ToTable("CarTrim", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ForControl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("DayOfPayment")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ForControlDay")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LinkForControl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__ForContr__3214EC0701B9CF38");

                    b.HasIndex("CarId");

                    b.ToTable("ForControl", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ParkingLot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id")
                        .HasName("PK__ParkingL__3214EC076A37F96D");

                    b.ToTable("ParkingLot", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CitizenIdentificationInfoAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("CitizenIdentificationInfo_Address");

                    b.Property<DateTime?>("CitizenIdentificationInfoDateReceive")
                        .HasColumnType("date")
                        .HasColumnName("CitizenIdentificationInfo_DateReceive");

                    b.Property<string>("CitizenIdentificationInfoNumber")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("CitizenIdentificationInfo_Number");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("CurrentAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Job")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PassportInfoAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PassportInfo_Address");

                    b.Property<DateTime?>("PassportInfoDateReceive")
                        .HasColumnType("date")
                        .HasColumnName("PassportInfo_DateReceive");

                    b.Property<string>("PassportInfoNumber")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("PassportInfo_Number");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id")
                        .HasName("PK__User__3214EC0766F7A7A3");

                    b.HasIndex(new[] { "CitizenIdentificationInfoNumber" }, "UQ_CitizenIdentificationInfo_Number")
                        .IsUnique()
                        .HasFilter("([CitizenIdentificationInfo_Number] IS NOT NULL)");

                    b.HasIndex(new[] { "PassportInfoNumber" }, "UQ_PassportInfo_Number")
                        .IsUnique()
                        .HasFilter("([PassportInfo_Number] IS NOT NULL)");

                    b.HasIndex(new[] { "Email" }, "UQ__User__A9D10534C08195BD")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Car", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.ParkingLot", "ParkingLot")
                        .WithMany("Cars")
                        .HasForeignKey("ParkingLotId")
                        .HasConstraintName("FK__Car__ParkingLotI__395884C4");

                    b.Navigation("ParkingLot");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarFile", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("CarFiles")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__CarFile__CarId__3C34F16F");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarGenerallInfo", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("CarGenerallInfos")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__CarGenera__CarId__47A6A41B");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarLoanInfo", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("CarLoanInfos")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__CarLoanIn__CarId__41EDCAC5");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarState", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("CarStates")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__CarState__CarId__4A8310C6");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CarTracking", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("CarTrackings")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__CarTracki__CarId__44CA3770");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ForControl", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Car", "Car")
                        .WithMany("ForControls")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK__ForContro__CarId__3F115E1A");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Car", b =>
                {
                    b.Navigation("CarFiles");

                    b.Navigation("CarGenerallInfos");

                    b.Navigation("CarLoanInfos");

                    b.Navigation("CarStates");

                    b.Navigation("CarTrackings");

                    b.Navigation("ForControls");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.ParkingLot", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
