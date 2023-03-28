using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ReceiveContract
    {
        public ReceiveContract()
        {
            ReceiveContractFiles = new HashSet<ReceiveContractFile>();
        }

        public int Id { get; set; }
        public int? ReceiverId { get; set; }
        public int? ContractGroupId { get; set; }
        public DateTime? DateReceive { get; set; }
        public string? ReceiveAddress { get; set; }
        public bool? OriginalCondition { get; set; }
        public int? CurrentCarStateSpeedometerNumber { get; set; }
        public int? CurrentCarStateFuelPercent { get; set; }
        public double? CurrentCarStateCurrentEtcAmount { get; set; }
        public string? CurrentCarStateCarStatusDescription { get; set; }
        public string? DepositItemAsset { get; set; }
        public string? DepositItemAssetInfo { get; set; }
        public double? DepositItemDownPayment { get; set; }
        public bool? ReturnDepostiItem { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? FilePath { get; set; }
        public string? StaffSignature { get; set; }
        public string? CustomerSignature { get; set; }
        public string? FileWithSignsPath { get; set; }
        public int? ContractStatusId { get; set; }
        public double? TotalKilometersTraveled { get; set; }
        public string? CurrentCarStateCarDamageDescription { get; set; }
        public double? InsuranceMoney { get; set; }
        public double? ExtraTime { get; set; }
        public bool? DetectedViolations { get; set; }
        public string? SpeedingViolationDescription { get; set; }
        public string? ForbiddenRoadViolationDescription { get; set; }
        public string? TrafficLightViolationDescription { get; set; }
        public string? OrtherViolation { get; set; }
        public double? ViolationMoney { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual ContractStatus? ContractStatus { get; set; }
        public virtual User? Receiver { get; set; }
        public virtual ICollection<ReceiveContractFile> ReceiveContractFiles { get; set; }
    }
}
