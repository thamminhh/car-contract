using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractContext : DbContext
    {
        public ContractContext()
        {
        }

        public ContractContext(DbContextOptions<ContractContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppraisalRecord> AppraisalRecords { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarFile> CarFiles { get; set; } = null!;
        public virtual DbSet<CarGenerallInfo> CarGenerallInfos { get; set; } = null!;
        public virtual DbSet<CarGeneration> CarGenerations { get; set; } = null!;
        public virtual DbSet<CarLoanInfo> CarLoanInfos { get; set; } = null!;
        public virtual DbSet<CarMaintenanceInfo> CarMaintenanceInfos { get; set; } = null!;
        public virtual DbSet<CarMake> CarMakes { get; set; } = null!;
        public virtual DbSet<CarModel> CarModels { get; set; } = null!;
        public virtual DbSet<CarRegistryInfo> CarRegistryInfos { get; set; } = null!;
        public virtual DbSet<CarSchedule> CarSchedules { get; set; } = null!;
        public virtual DbSet<CarSeries> CarSeries { get; set; } = null!;
        public virtual DbSet<CarState> CarStates { get; set; } = null!;
        public virtual DbSet<CarStatus> CarStatuses { get; set; } = null!;
        public virtual DbSet<CarTracking> CarTrackings { get; set; } = null!;
        public virtual DbSet<CarTrim> CarTrims { get; set; } = null!;
        public virtual DbSet<ContractGroup> ContractGroups { get; set; } = null!;
        public virtual DbSet<ContractGroupStatus> ContractGroupStatuses { get; set; } = null!;
        public virtual DbSet<ContractStatistic> ContractStatistics { get; set; } = null!;
        public virtual DbSet<ContractStatus> ContractStatuses { get; set; } = null!;
        public virtual DbSet<CustomerFile> CustomerFiles { get; set; } = null!;
        public virtual DbSet<CustomerInfo> CustomerInfos { get; set; } = null!;
        public virtual DbSet<ForControl> ForControls { get; set; } = null!;
        public virtual DbSet<ParkingLot> ParkingLots { get; set; } = null!;
        public virtual DbSet<ReceiveContract> ReceiveContracts { get; set; } = null!;
        public virtual DbSet<ReceiveContractFile> ReceiveContractFiles { get; set; } = null!;
        public virtual DbSet<RentContract> RentContracts { get; set; } = null!;
        public virtual DbSet<TransferContract> TransferContracts { get; set; } = null!;
        public virtual DbSet<TransferContractFile> TransferContractFiles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:carcontractv3.database.windows.net,1433;Initial Catalog=carcontract;Persist Security Info=False;User ID=admin123;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppraisalRecord>(entity =>
            {
                entity.ToTable("AppraisalRecord");

                entity.Property(e => e.DepositInfoDownPayment).HasColumnName("DepositInfo_DownPayment");

                entity.Property(e => e.ExpertiseDate).HasColumnType("datetime");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.ResultDescription).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithMany(p => p.AppraisalRecords)
                    .HasForeignKey(d => d.ContractGroupId)
                    .HasConstraintName("FK__Appraisal__Contr__2FCF1A8A");

                entity.HasOne(d => d.Expertiser)
                    .WithMany(p => p.AppraisalRecords)
                    .HasForeignKey(d => d.ExpertiserId)
                    .HasConstraintName("FK__Appraisal__Exper__30C33EC3");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarColor).HasMaxLength(255);

                entity.Property(e => e.CarFuel).HasMaxLength(255);

                entity.Property(e => e.CarLicensePlates).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.TankCapacity).HasColumnType("decimal(3, 1)");

                entity.HasOne(d => d.CarGeneration)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarGenerationId)
                    .HasConstraintName("FK__Car__CarGenerati__00200768");

                entity.HasOne(d => d.CarMake)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarMakeId)
                    .HasConstraintName("FK__Car__CarMakeId__7E37BEF6");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__Car__CarModelId__7F2BE32F");

                entity.HasOne(d => d.CarSeries)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarSeriesId)
                    .HasConstraintName("FK__Car__CarSeriesId__01142BA1");

                entity.HasOne(d => d.CarStatus)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarStatusId)
                    .HasConstraintName("FK__Car__CarStatusId__7D439ABD");

                entity.HasOne(d => d.CarTrim)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarTrimId)
                    .HasConstraintName("FK__Car__CarTrimId__02084FDA");

                entity.HasOne(d => d.ParkingLot)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ParkingLotId)
                    .HasConstraintName("FK__Car__ParkingLotI__7C4F7684");
            });

            modelBuilder.Entity<CarFile>(entity =>
            {
                entity.ToTable("CarFile");

                entity.HasIndex(e => e.CarId, "UQ__CarFile__68A0342F8D337DED")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarFile)
                    .HasForeignKey<CarFile>(d => d.CarId)
                    .HasConstraintName("FK__CarFile__CarId__05D8E0BE");
            });

            modelBuilder.Entity<CarGenerallInfo>(entity =>
            {
                entity.ToTable("CarGenerallInfo");

                entity.HasIndex(e => e.CarId, "UQ__CarGener__68A0342F06856D24")
                    .IsUnique();

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarGenerallInfo)
                    .HasForeignKey<CarGenerallInfo>(d => d.CarId)
                    .HasConstraintName("FK__CarGenera__CarId__1EA48E88");
            });

            modelBuilder.Entity<CarGeneration>(entity =>
            {
                entity.ToTable("CarGeneration");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarGenerations)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarGenera__CarMo__71D1E811");
            });

            modelBuilder.Entity<CarLoanInfo>(entity =>
            {
                entity.ToTable("CarLoanInfo");

                entity.HasIndex(e => e.CarId, "UQ__CarLoanI__68A0342F454B3E86")
                    .IsUnique();

                entity.Property(e => e.CarOwnerName).HasMaxLength(255);

                entity.Property(e => e.RentalDate).HasColumnType("date");

                entity.Property(e => e.RentalMethod).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarLoanInfo)
                    .HasForeignKey<CarLoanInfo>(d => d.CarId)
                    .HasConstraintName("FK__CarLoanIn__CarId__17036CC0");
            });

            modelBuilder.Entity<CarMaintenanceInfo>(entity =>
            {
                entity.ToTable("CarMaintenanceInfo");

                entity.Property(e => e.CarKmlastMaintenance).HasColumnName("CarKMLastMaintenance");

                entity.Property(e => e.MaintenanceDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarMaintenanceInfos)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarMainte__CarId__08B54D69");
            });

            modelBuilder.Entity<CarMake>(entity =>
            {
                entity.ToTable("CarMake");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CarMakeImg).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("CarModel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarMake)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.CarMakeId)
                    .HasConstraintName("FK__CarModel__CarMak__6EF57B66");
            });

            modelBuilder.Entity<CarRegistryInfo>(entity =>
            {
                entity.ToTable("CarRegistryInfo");

                entity.Property(e => e.RegistrationDeadline).HasColumnType("datetime");

                entity.Property(e => e.RegistryAddress).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRegistryInfos)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarRegist__CarId__0B91BA14");
            });

            modelBuilder.Entity<CarSchedule>(entity =>
            {
                entity.ToTable("CarSchedule");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarSchedules)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarSchedu__CarId__0E6E26BF");

                entity.HasOne(d => d.CarStatus)
                    .WithMany(p => p.CarSchedules)
                    .HasForeignKey(d => d.CarStatusId)
                    .HasConstraintName("FK__CarSchedu__CarSt__0F624AF8");
            });

            modelBuilder.Entity<CarSeries>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CarGenerationId).HasColumnName("CarGenerationID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarGeneration)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.CarGenerationId)
                    .HasConstraintName("FK__CarSeries__CarGe__75A278F5");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarSeries__CarMo__74AE54BC");
            });

            modelBuilder.Entity<CarState>(entity =>
            {
                entity.ToTable("CarState");

                entity.HasIndex(e => e.CarId, "UQ__CarState__68A0342F8A254B2D")
                    .IsUnique();

                entity.Property(e => e.CarStatusDescription).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarState)
                    .HasForeignKey<CarState>(d => d.CarId)
                    .HasConstraintName("FK__CarState__CarId__22751F6C");
            });

            modelBuilder.Entity<CarStatus>(entity =>
            {
                entity.ToTable("CarStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<CarTracking>(entity =>
            {
                entity.ToTable("CarTracking");

                entity.HasIndex(e => e.CarId, "UQ__CarTrack__68A0342FD560E688")
                    .IsUnique();

                entity.Property(e => e.Etcpassword)
                    .HasMaxLength(255)
                    .HasColumnName("ETCPassword");

                entity.Property(e => e.Etcusername)
                    .HasMaxLength(255)
                    .HasColumnName("ETCUsername");

                entity.Property(e => e.LinkTracking).HasMaxLength(255);

                entity.Property(e => e.TrackingPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingUsername).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarTracking)
                    .HasForeignKey<CarTracking>(d => d.CarId)
                    .HasConstraintName("FK__CarTracki__CarId__1AD3FDA4");
            });

            modelBuilder.Entity<CarTrim>(entity =>
            {
                entity.ToTable("CarTrim");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarTrim__CarMode__787EE5A0");

                entity.HasOne(d => d.CarSeries)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.CarSeriesId)
                    .HasConstraintName("FK__CarTrim__CarSeri__797309D9");
            });

            modelBuilder.Entity<ContractGroup>(entity =>
            {
                entity.ToTable("ContractGroup");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.RentFrom).HasColumnType("datetime");

                entity.Property(e => e.RentPurpose).HasMaxLength(255);

                entity.Property(e => e.RentTo).HasColumnType("datetime");

                entity.Property(e => e.RequireDescriptionInfoCarBrand)
                    .HasMaxLength(255)
                    .HasColumnName("RequireDescriptionInfo_CarBrand");

                entity.Property(e => e.RequireDescriptionInfoCarColor)
                    .HasMaxLength(255)
                    .HasColumnName("RequireDescriptionInfo_CarColor");

                entity.Property(e => e.RequireDescriptionInfoGearBox)
                    .HasMaxLength(255)
                    .HasColumnName("RequireDescriptionInfo_GearBox");

                entity.Property(e => e.RequireDescriptionInfoPriceForDay).HasColumnName("RequireDescriptionInfo_PriceForDay");

                entity.Property(e => e.RequireDescriptionInfoSeatNumber).HasColumnName("RequireDescriptionInfo_SeatNumber");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__ContractG__CarId__2BFE89A6");

                entity.HasOne(d => d.ContractGroupStatus)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.ContractGroupStatusId)
                    .HasConstraintName("FK__ContractG__Contr__2CF2ADDF");

                entity.HasOne(d => d.CustomerInfo)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.CustomerInfoId)
                    .HasConstraintName("FK__ContractG__Custo__2A164134");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ContractG__UserI__2B0A656D");
            });

            modelBuilder.Entity<ContractGroupStatus>(entity =>
            {
                entity.ToTable("ContractGroupStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ContractStatistic>(entity =>
            {
                entity.ToTable("ContractStatistic");

                entity.Property(e => e.ContractGroupId).HasColumnName("ContractGroupID");

                entity.Property(e => e.EtcmoneyUsing).HasColumnName("ETCMoneyUsing");

                entity.HasOne(d => d.ContractGroup)
                    .WithMany(p => p.ContractStatistics)
                    .HasForeignKey(d => d.ContractGroupId)
                    .HasConstraintName("FK__ContractS__Contr__5BAD9CC8");
            });

            modelBuilder.Entity<ContractStatus>(entity =>
            {
                entity.ToTable("ContractStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerFile>(entity =>
            {
                entity.ToTable("CustomerFile");

                entity.Property(e => e.DocumentDescription).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("Title ");

                entity.Property(e => e.TypeOfDocument).HasMaxLength(255);

                entity.HasOne(d => d.CustomerInfo)
                    .WithMany(p => p.CustomerFiles)
                    .HasForeignKey(d => d.CustomerInfoId)
                    .HasConstraintName("FK__CustomerF__Custo__2739D489");
            });

            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.ToTable("CustomerInfo");

                entity.Property(e => e.CitizenIdentificationInfoAddress)
                    .HasMaxLength(255)
                    .HasColumnName("CitizenIdentificationInfo_Address");

                entity.Property(e => e.CitizenIdentificationInfoDateReceive)
                    .HasColumnType("datetime")
                    .HasColumnName("CitizenIdentificationInfo_DateReceive");

                entity.Property(e => e.CitizenIdentificationInfoNumber)
                    .HasMaxLength(255)
                    .HasColumnName("CitizenIdentificationInfo_Number");

                entity.Property(e => e.CompanyInfo).HasMaxLength(255);

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(255)
                    .HasColumnName("CustomerAddress ");

                entity.Property(e => e.CustomerEmail).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.CustomerSocialInfoFacebook).HasColumnName("CustomerSocialInfo_Facebook");

                entity.Property(e => e.CustomerSocialInfoZalo).HasColumnName("CustomerSocialInfo_Zalo");

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.RelativeTel).HasMaxLength(255);
            });

            modelBuilder.Entity<ForControl>(entity =>
            {
                entity.ToTable("ForControl");

                entity.HasIndex(e => e.CarId, "UQ__ForContr__68A0342F41D91849")
                    .IsUnique();

                entity.Property(e => e.DayOfPayment).HasMaxLength(255);

                entity.Property(e => e.ForControlDay).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.ForControl)
                    .HasForeignKey<ForControl>(d => d.CarId)
                    .HasConstraintName("FK__ForContro__CarId__1332DBDC");
            });

            modelBuilder.Entity<ParkingLot>(entity =>
            {
                entity.ToTable("ParkingLot");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Latitude).HasMaxLength(255);

                entity.Property(e => e.Longitude).HasMaxLength(255);

                entity.Property(e => e.ManagerName).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<ReceiveContract>(entity =>
            {
                entity.ToTable("ReceiveContract");

                entity.HasIndex(e => e.ContractGroupId, "UQ__ReceiveC__BD73678F61863B88")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCarStateCarDamageDescription)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarDamageDescription");

                entity.Property(e => e.CurrentCarStateCarStatusDescription)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarStatusDescription");

                entity.Property(e => e.CurrentCarStateCurrentEtcAmount).HasColumnName("CurrentCarState_CurrentEtcAmount");

                entity.Property(e => e.CurrentCarStateFuelPercent).HasColumnName("CurrentCarState_FuelPercent");

                entity.Property(e => e.CurrentCarStateSpeedometerNumber).HasColumnName("CurrentCarState_SpeedometerNumber");

                entity.Property(e => e.DateReceive).HasColumnType("datetime");

                entity.Property(e => e.DepositItemDownPayment).HasColumnName("DepositItem_DownPayment");

                entity.Property(e => e.DetectedViolations).HasColumnName("DetectedViolations ");

                entity.Property(e => e.ForbiddenRoadViolationDescription).HasMaxLength(255);

                entity.Property(e => e.OriginalCondition).HasColumnName("OriginalCondition ");

                entity.Property(e => e.OrtherViolation).HasMaxLength(255);

                entity.Property(e => e.ReceiveAddress)
                    .HasMaxLength(255)
                    .HasColumnName("ReceiveAddress ");

                entity.Property(e => e.SpeedingViolationDescription).HasMaxLength(255);

                entity.Property(e => e.TrafficLightViolationDescription).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.ReceiveContract)
                    .HasForeignKey<ReceiveContract>(d => d.ContractGroupId)
                    .HasConstraintName("FK__ReceiveCo__Contr__503BEA1C");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.ReceiveContracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .HasConstraintName("FK__ReceiveCo__Contr__51300E55");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ReceiveContracts)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__ReceiveCo__Recei__4F47C5E3");
            });

            modelBuilder.Entity<ReceiveContractFile>(entity =>
            {
                entity.ToTable("ReceiveContractFile");

                entity.Property(e => e.DocumentDescription).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.ReceiveContract)
                    .WithMany(p => p.ReceiveContractFiles)
                    .HasForeignKey(d => d.ReceiveContractId)
                    .HasConstraintName("FK__ReceiveCo__Recei__540C7B00");
            });

            modelBuilder.Entity<RentContract>(entity =>
            {
                entity.ToTable("RentContract");

                entity.Property(e => e.CancelReason).HasMaxLength(255);

                entity.Property(e => e.CarGeneralInfoAtRentLimitedKmForMonth).HasColumnName("CarGeneralInfoAtRent_LimitedKmForMonth");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForMonth).HasColumnName("CarGeneralInfoAtRent_PriceForMonth");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForNormalDay).HasColumnName("CarGeneralInfoAtRent_PriceForNormalDay");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForWeekendDay).HasColumnName("CarGeneralInfoAtRent_PriceForWeekendDay");

                entity.Property(e => e.CarGeneralInfoAtRentPricePerHourExceed).HasColumnName("CarGeneralInfoAtRent_PricePerHourExceed");

                entity.Property(e => e.CarGeneralInfoAtRentPricePerKmExceed).HasColumnName("CarGeneralInfoAtRent_PricePerKmExceed");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerSignature).HasMaxLength(255);

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.DepositItemDownPayment).HasColumnName("DepositItem_DownPayment");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FileWithSignsPath).HasMaxLength(255);

                entity.Property(e => e.StaffSignature).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.ContractGroupId)
                    .HasConstraintName("FK__RentContr__Contr__339FAB6E");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .HasConstraintName("FK__RentContr__Contr__3587F3E0");

                entity.HasOne(d => d.Representative)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.RepresentativeId)
                    .HasConstraintName("FK__RentContr__Repre__3493CFA7");
            });

            modelBuilder.Entity<TransferContract>(entity =>
            {
                entity.ToTable("TransferContract");

                entity.HasIndex(e => e.ContractGroupId, "UQ__Transfer__BD73678F3E8E76B5")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCarStateCarStatusDescription)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarStatusDescription");

                entity.Property(e => e.CurrentCarStateCurrentEtcAmount).HasColumnName("CurrentCarState_CurrentEtcAmount");

                entity.Property(e => e.CurrentCarStateFuelPercent).HasColumnName("CurrentCarState_FuelPercent");

                entity.Property(e => e.CurrentCarStateSpeedometerNumber).HasColumnName("CurrentCarState_SpeedometerNumber");

                entity.Property(e => e.CustomerSignature).HasMaxLength(255);

                entity.Property(e => e.DateTransfer).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(255)
                    .HasColumnName("DeliveryAddress ");

                entity.Property(e => e.DepositItemDownPayment).HasColumnName("DepositItem_DownPayment");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FileWithSignsPath).HasMaxLength(255);

                entity.Property(e => e.StaffSignature).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.TransferContract)
                    .HasForeignKey<TransferContract>(d => d.ContractGroupId)
                    .HasConstraintName("FK__TransferC__Contr__3A4CA8FD");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.TransferContracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .HasConstraintName("FK__TransferC__Contr__3B40CD36");

                entity.HasOne(d => d.Transferer)
                    .WithMany(p => p.TransferContracts)
                    .HasForeignKey(d => d.TransfererId)
                    .HasConstraintName("FK__TransferC__Trans__395884C4");
            });

            modelBuilder.Entity<TransferContractFile>(entity =>
            {
                entity.ToTable("TransferContractFile");

                entity.Property(e => e.DocumentDescription).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("Title ");

                entity.HasOne(d => d.TransferContract)
                    .WithMany(p => p.TransferContractFiles)
                    .HasForeignKey(d => d.TransferContractId)
                    .HasConstraintName("FK__TransferC__Trans__3E1D39E1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.CitizenIdentificationInfoNumber, "UQ_CitizenIdentificationInfo_Number")
                    .IsUnique()
                    .HasFilter("([CitizenIdentificationInfo_Number] IS NOT NULL)");

                entity.HasIndex(e => e.PassportInfoNumber, "UQ_PassportInfo_Number")
                    .IsUnique()
                    .HasFilter("([PassportInfo_Number] IS NOT NULL)");

                entity.HasIndex(e => e.Email, "UQ__User__A9D1053427D5D78B")
                    .IsUnique();

                entity.Property(e => e.CitizenIdentificationInfoAddress)
                    .HasMaxLength(255)
                    .HasColumnName("CitizenIdentificationInfo_Address");

                entity.Property(e => e.CitizenIdentificationInfoDateReceive)
                    .HasColumnType("date")
                    .HasColumnName("CitizenIdentificationInfo_DateReceive");

                entity.Property(e => e.CitizenIdentificationInfoNumber)
                    .HasMaxLength(255)
                    .HasColumnName("CitizenIdentificationInfo_Number");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.CurrentAddress).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PassportInfoAddress)
                    .HasMaxLength(255)
                    .HasColumnName("PassportInfo_Address");

                entity.Property(e => e.PassportInfoDateReceive)
                    .HasColumnType("date")
                    .HasColumnName("PassportInfo_DateReceive");

                entity.Property(e => e.PassportInfoNumber)
                    .HasMaxLength(255)
                    .HasColumnName("PassportInfo_Number");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Role).HasMaxLength(255);

                entity.HasOne(d => d.ParkingLot)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ParkingLotId)
                    .HasConstraintName("FK__User__ParkingLot__5FB337D6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
