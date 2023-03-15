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
        public virtual DbSet<ContractFile> ContractFiles { get; set; } = null!;
        public virtual DbSet<ContractGroup> ContractGroups { get; set; } = null!;
        public virtual DbSet<ContractGroupStatus> ContractGroupStatuses { get; set; } = null!;
        public virtual DbSet<ContractStatus> ContractStatuses { get; set; } = null!;
        public virtual DbSet<CustomerInfo> CustomerInfos { get; set; } = null!;
        public virtual DbSet<ForControl> ForControls { get; set; } = null!;
        public virtual DbSet<ParkingLot> ParkingLots { get; set; } = null!;
        public virtual DbSet<ReceiveContract> ReceiveContracts { get; set; } = null!;
        public virtual DbSet<RentContract> RentContracts { get; set; } = null!;
        public virtual DbSet<TransferContract> TransferContracts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:contractsqlserverdb.database.windows.net,1433;Initial Catalog=carcontract;Persist Security Info=False;User ID=admin123;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppraisalRecord>(entity =>
            {
                entity.ToTable("AppraisalRecord");

                entity.Property(e => e.DepositInfoAsset)
                    .HasMaxLength(255)
                    .HasColumnName("DepositInfo_Asset");

                entity.Property(e => e.DepositInfoDescription)
                    .HasMaxLength(255)
                    .HasColumnName("DepositInfo_Description");

                entity.Property(e => e.DepositInfoDownPayment).HasColumnName("DepositInfo_DownPayment");

                entity.Property(e => e.ExpertiseDate).HasColumnType("datetime");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.ResultDescription).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithMany(p => p.AppraisalRecords)
                    .HasForeignKey(d => d.ContractGroupId)
                    .HasConstraintName("FK__Appraisal__Contr__66EA454A");

                entity.HasOne(d => d.Expertiser)
                    .WithMany(p => p.AppraisalRecords)
                    .HasForeignKey(d => d.ExpertiserId)
                    .HasConstraintName("FK__Appraisal__Exper__67DE6983");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarColor).HasMaxLength(255);

                entity.Property(e => e.CarFuel).HasMaxLength(255);

                entity.Property(e => e.CarLicensePlates).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.HasOne(d => d.CarGeneration)
                    .WithMany(p => p.CarCarGenerations)
                    .HasForeignKey(d => d.CarGenerationId)
                    .HasConstraintName("FK__Car__CarGenerati__3BFFE745");

                entity.HasOne(d => d.CarMake)
                    .WithMany(p => p.CarCarMakes)
                    .HasForeignKey(d => d.CarMakeId)
                    .HasConstraintName("FK__Car__CarMakeId__3A179ED3");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarCarModels)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__Car__CarModelId__3B0BC30C");

                entity.HasOne(d => d.CarSeries)
                    .WithMany(p => p.CarCarSeries)
                    .HasForeignKey(d => d.CarSeriesId)
                    .HasConstraintName("FK__Car__CarSeriesId__3CF40B7E");

                entity.HasOne(d => d.CarStatus)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarStatusId)
                    .HasConstraintName("FK__Car__CarStatusId__68487DD7");

                entity.HasOne(d => d.CarTrim)
                    .WithMany(p => p.CarCarTrims)
                    .HasForeignKey(d => d.CarTrimId)
                    .HasConstraintName("FK__Car__CarTrimId__3DE82FB7");

                entity.HasOne(d => d.ParkingLot)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ParkingLotId)
                    .HasConstraintName("FK__Car__ParkingLotI__6754599E");
            });

            modelBuilder.Entity<CarFile>(entity =>
            {
                entity.ToTable("CarFile");

                entity.HasIndex(e => e.CarId, "UQ__CarFile__68A0342FB7405E59")
                    .IsUnique();

                entity.Property(e => e.BackImg).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FrontImg).HasMaxLength(255);

                entity.Property(e => e.LeftImg).HasMaxLength(255);

                entity.Property(e => e.OrtherImg).HasMaxLength(255);

                entity.Property(e => e.RightImg).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarFile)
                    .HasForeignKey<CarFile>(d => d.CarId)
                    .HasConstraintName("FK__CarFile__CarId__2EDAF651");
            });

            modelBuilder.Entity<CarGenerallInfo>(entity =>
            {
                entity.ToTable("CarGenerallInfo");

                entity.HasIndex(e => e.CarId, "UQ__CarGener__68A0342F566E01F0")
                    .IsUnique();

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarGenerallInfo)
                    .HasForeignKey<CarGenerallInfo>(d => d.CarId)
                    .HasConstraintName("FK__CarGenera__CarId__3F115E1A");
            });

            modelBuilder.Entity<CarGeneration>(entity =>
            {
                entity.ToTable("CarGeneration");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarGenerations)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarGenera__CarMo__1EA48E88");
            });

            modelBuilder.Entity<CarLoanInfo>(entity =>
            {
                entity.ToTable("CarLoanInfo");

                entity.HasIndex(e => e.CarId, "UQ__CarLoanI__68A0342F46EC7C94")
                    .IsUnique();

                entity.Property(e => e.CarOwnerName).HasMaxLength(255);

                entity.Property(e => e.PriceForDayReceive).HasMaxLength(255);

                entity.Property(e => e.RentalDate).HasColumnType("date");

                entity.Property(e => e.RentalMethod).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarLoanInfo)
                    .HasForeignKey<CarLoanInfo>(d => d.CarId)
                    .HasConstraintName("FK__CarLoanIn__CarId__367C1819");
            });

            modelBuilder.Entity<CarMaintenanceInfo>(entity =>
            {
                entity.ToTable("CarMaintenanceInfo");

                entity.Property(e => e.CarKmlastMaintenance).HasColumnName("CarKMLastMaintenance");

                entity.Property(e => e.MaintenanceDate).HasColumnType("datetime");

                entity.Property(e => e.MaintenanceInvoice).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarMaintenanceInfos)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarMainte__CarId__6ABAD62E");
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
                    .HasConstraintName("FK__CarModel__CarMak__1BC821DD");
            });

            modelBuilder.Entity<CarRegistryInfo>(entity =>
            {
                entity.ToTable("CarRegistryInfo");

                entity.Property(e => e.LastRegistryDate).HasColumnType("datetime");

                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.Property(e => e.RegistryInvoice).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRegistryInfos)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarRegist__CarId__6D9742D9");
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
                    .HasConstraintName("FK__CarSchedu__CarId__32767D0B");

                entity.HasOne(d => d.CarStatus)
                    .WithMany(p => p.CarSchedules)
                    .HasForeignKey(d => d.CarStatusId)
                    .HasConstraintName("FK__CarSchedu__CarSt__336AA144");
            });

            modelBuilder.Entity<CarSeries>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CarGenerationId).HasColumnName("CarGenerationID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarGeneration)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.CarGenerationId)
                    .HasConstraintName("FK__CarSeries__CarGe__3552E9B6");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarSeries__CarMo__345EC57D");
            });

            modelBuilder.Entity<CarState>(entity =>
            {
                entity.ToTable("CarState");

                entity.HasIndex(e => e.CarId, "UQ__CarState__68A0342F4F330BA6")
                    .IsUnique();

                entity.Property(e => e.CarStatusDescription).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.CarState)
                    .HasForeignKey<CarState>(d => d.CarId)
                    .HasConstraintName("FK__CarState__CarId__47A6A41B");
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

                entity.HasIndex(e => e.CarId, "UQ__CarTrack__68A0342F39C1AA50")
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
                    .HasConstraintName("FK__CarTracki__CarId__3B40CD36");
            });

            modelBuilder.Entity<CarTrim>(entity =>
            {
                entity.ToTable("CarTrim");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.CarModelId)
                    .HasConstraintName("FK__CarTrim__CarMode__36470DEF");
            });

            modelBuilder.Entity<ContractFile>(entity =>
            {
                entity.ToTable("ContractFile");

                entity.HasIndex(e => e.ContractGroupId, "UQ__Contract__BD73678FE48DAF14")
                    .IsUnique();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.ContractFile)
                    .HasForeignKey<ContractFile>(d => d.ContractGroupId)
                    .HasConstraintName("FK__ContractF__Contr__4B422AD5");
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

                entity.Property(e => e.RequireDescriptionInfoCarClass).HasColumnName("RequireDescriptionInfo_CarClass");

                entity.Property(e => e.RequireDescriptionInfoCarColor)
                    .HasMaxLength(255)
                    .HasColumnName("RequireDescriptionInfo_CarColor");

                entity.Property(e => e.RequireDescriptionInfoSeatNumber).HasColumnName("RequireDescriptionInfo_SeatNumber");

                entity.Property(e => e.RequireDescriptionInfoYearCreate).HasColumnName("RequireDescriptionInfo_YearCreate");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__ContractG__CarId__467D75B8");

                entity.HasOne(d => d.ContractGroupStatus)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.ContractGroupStatusId)
                    .HasConstraintName("FK__ContractG__Contr__477199F1");

                entity.HasOne(d => d.CustomerInfo)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.CustomerInfoId)
                    .HasConstraintName("FK__ContractG__Custo__44952D46");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContractGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ContractG__UserI__4589517F");
            });

            modelBuilder.Entity<ContractGroupStatus>(entity =>
            {
                entity.ToTable("ContractGroupStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ContractStatus>(entity =>
            {
                entity.ToTable("ContractStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.ToTable("CustomerInfo");

                entity.Property(e => e.AddtionalInfo).HasMaxLength(255);

                entity.Property(e => e.CompanyInfo).HasMaxLength(255);

                entity.Property(e => e.CustomerAddress).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.CustomerSocialInfoFacebook)
                    .HasMaxLength(255)
                    .HasColumnName("CustomerSocialInfo_Facebook");

                entity.Property(e => e.CustomerSocialInfoLinkedin)
                    .HasMaxLength(255)
                    .HasColumnName("CustomerSocialInfo_Linkedin");

                entity.Property(e => e.CustomerSocialInfoOther)
                    .HasMaxLength(255)
                    .HasColumnName("CustomerSocialInfo_Other");

                entity.Property(e => e.CustomerSocialInfoZalo)
                    .HasMaxLength(255)
                    .HasColumnName("CustomerSocialInfo_Zalo");

                entity.Property(e => e.ExpertiseInfoIsFirstTimeRent).HasColumnName("ExpertiseInfo_isFirstTimeRent");

                entity.Property(e => e.ExpertiseInfoTrustLevel)
                    .HasMaxLength(255)
                    .HasColumnName("ExpertiseInfo_TrustLevel");

                entity.Property(e => e.PhoneNumber).HasMaxLength(13);

                entity.Property(e => e.RelativeTel).HasMaxLength(13);
            });

            modelBuilder.Entity<ForControl>(entity =>
            {
                entity.ToTable("ForControl");

                entity.HasIndex(e => e.CarId, "UQ__ForContr__68A0342F6EC46E87")
                    .IsUnique();

                entity.Property(e => e.DayOfPayment).HasMaxLength(255);

                entity.Property(e => e.ForControlDay).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod).HasMaxLength(255);

                entity.HasOne(d => d.Car)
                    .WithOne(p => p.ForControl)
                    .HasForeignKey<ForControl>(d => d.CarId)
                    .HasConstraintName("FK__ForContro__CarId__32AB8735");
            });

            modelBuilder.Entity<ParkingLot>(entity =>
            {
                entity.ToTable("ParkingLot");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Latitude).HasMaxLength(255);

                entity.Property(e => e.Longitude).HasMaxLength(255);

                entity.Property(e => e.ManagerName).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParkingLotImg).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<ReceiveContract>(entity =>
            {
                entity.ToTable("ReceiveContract");

                entity.HasIndex(e => e.ContractGroupId, "UQ__ReceiveC__BD73678FF8E9433B")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCarStateCarBackImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarBackImg");

                entity.Property(e => e.CurrentCarStateCarBackSeatImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarBackSeatImg");

                entity.Property(e => e.CurrentCarStateCarFrontImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarFrontImg");

                entity.Property(e => e.CurrentCarStateCarInteriorImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarInteriorImg");

                entity.Property(e => e.CurrentCarStateCarLeftImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarLeftImg");

                entity.Property(e => e.CurrentCarStateCarPhysicalDamage)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarPhysicalDamage");

                entity.Property(e => e.CurrentCarStateCarRightImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarRightImg");

                entity.Property(e => e.CurrentCarStateCarStatusDescription)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarStatusDescription");

                entity.Property(e => e.CurrentCarStateCurrentEtcAmount).HasColumnName("CurrentCarState_CurrentEtcAmount");

                entity.Property(e => e.CurrentCarStateFuelPercent).HasColumnName("CurrentCarState_FuelPercent");

                entity.Property(e => e.CurrentCarStateSpeedometerNumber).HasColumnName("CurrentCarState_SpeedometerNumber");

                entity.Property(e => e.CustomerSignature).HasMaxLength(255);

                entity.Property(e => e.DateReceive).HasColumnType("datetime");

                entity.Property(e => e.DepositItemAsset).HasColumnName("DepositItem_Asset");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FileWithSignsPath).HasMaxLength(255);

                entity.Property(e => e.ReceiveAddress)
                    .HasMaxLength(255)
                    .HasColumnName("ReceiveAddress ");

                entity.Property(e => e.StaffSignature).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.ReceiveContract)
                    .HasForeignKey<ReceiveContract>(d => d.ContractGroupId)
                    .HasConstraintName("FK__ReceiveCo__Contr__603D47BB");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.ReceiveContracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .HasConstraintName("FK__ReceiveCo__Contr__61316BF4");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ReceiveContracts)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__ReceiveCo__Recei__5F492382");
            });

            modelBuilder.Entity<RentContract>(entity =>
            {
                entity.ToTable("RentContract");

                entity.HasIndex(e => e.ContractGroupId, "UQ__RentCont__BD73678FEA44C76E")
                    .IsUnique();

                entity.Property(e => e.CarGeneralInfoAtRentLimitedKmForMonth).HasColumnName("CarGeneralInfoAtRent_LimitedKmForMonth");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForMonth).HasColumnName("CarGeneralInfoAtRent_PriceForMonth");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForNormalDay).HasColumnName("CarGeneralInfoAtRent_PriceForNormalDay");

                entity.Property(e => e.CarGeneralInfoAtRentPriceForWeekendDay).HasColumnName("CarGeneralInfoAtRent_PriceForWeekendDay");

                entity.Property(e => e.CarGeneralInfoAtRentPricePerHourExceed).HasColumnName("CarGeneralInfoAtRent_PricePerHourExceed");

                entity.Property(e => e.CarGeneralInfoAtRentPricePerKmExceed).HasColumnName("CarGeneralInfoAtRent_PricePerKmExceed");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerSignature).HasMaxLength(255);

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.DepositItemAsset)
                    .HasMaxLength(255)
                    .HasColumnName("DepositItem_Asset");

                entity.Property(e => e.DepositItemDescription)
                    .HasMaxLength(255)
                    .HasColumnName("DepositItem_Description");

                entity.Property(e => e.DepositItemDownPayment).HasColumnName("DepositItem_DownPayment");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FileWithSignsPath).HasMaxLength(255);

                entity.Property(e => e.StaffSignature).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.RentContract)
                    .HasForeignKey<RentContract>(d => d.ContractGroupId)
                    .HasConstraintName("FK__RentContr__Contr__7167D3BD");

                entity.HasOne(d => d.Representative)
                    .WithMany(p => p.RentContracts)
                    .HasForeignKey(d => d.RepresentativeId)
                    .HasConstraintName("FK__RentContr__Repre__725BF7F6");
            });

            modelBuilder.Entity<TransferContract>(entity =>
            {
                entity.ToTable("TransferContract");

                entity.HasIndex(e => e.ContractGroupId, "UQ__Transfer__BD73678FD8BEA333")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCarStateCarBackImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarBackImg");

                entity.Property(e => e.CurrentCarStateCarBackSeatImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarBackSeatImg");

                entity.Property(e => e.CurrentCarStateCarFrontImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarFrontImg");

                entity.Property(e => e.CurrentCarStateCarInteriorImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarInteriorImg");

                entity.Property(e => e.CurrentCarStateCarLeftImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarLeftImg");

                entity.Property(e => e.CurrentCarStateCarRightImg)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarRightImg");

                entity.Property(e => e.CurrentCarStateCarStatusDescription)
                    .HasMaxLength(255)
                    .HasColumnName("CurrentCarState_CarStatusDescription");

                entity.Property(e => e.CurrentCarStateCurrentEtcAmount).HasColumnName("CurrentCarState_CurrentEtcAmount");

                entity.Property(e => e.CurrentCarStateFuelPercent).HasColumnName("CurrentCarState_FuelPercent");

                entity.Property(e => e.CurrentCarStateSpeedometerNumber).HasColumnName("CurrentCarState_SpeedometerNumber");

                entity.Property(e => e.CustomerSignature).HasMaxLength(255);

                entity.Property(e => e.DateTransfer).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.DepositItemAsset).HasColumnName("DepositItem_Asset");

                entity.Property(e => e.DepositItemAssetInfo)
                    .HasMaxLength(255)
                    .HasColumnName("DepositItem_AssetInfo");

                entity.Property(e => e.DepositItemPaper)
                    .HasMaxLength(255)
                    .HasColumnName("DepositItem_Paper");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.FileWithSignsPath).HasMaxLength(255);

                entity.Property(e => e.StaffSignature).HasMaxLength(255);

                entity.HasOne(d => d.ContractGroup)
                    .WithOne(p => p.TransferContract)
                    .HasForeignKey<TransferContract>(d => d.ContractGroupId)
                    .HasConstraintName("FK__TransferC__Contr__5A846E65");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.TransferContracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .HasConstraintName("FK__TransferC__Contr__5B78929E");

                entity.HasOne(d => d.Transferer)
                    .WithMany(p => p.TransferContracts)
                    .HasForeignKey(d => d.TransfererId)
                    .HasConstraintName("FK__TransferC__Trans__59904A2C");
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

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534CB9FF300")
                    .IsUnique();

                entity.Property(e => e.CardImage).HasMaxLength(255);

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
