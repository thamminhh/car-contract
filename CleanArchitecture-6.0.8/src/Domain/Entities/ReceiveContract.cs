using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ReceiveContract
    {
        public int Id { get; set; }
        public int? ReceiverId { get; set; }
        public int? ContractGroupId { get; set; }
        public DateTime? DateReceive { get; set; }
        public string? ReceiveAddress { get; set; }
        public int? CurrentCarStateSpeedometerNumber { get; set; }
        public int? CurrentCarStateFuelPercent { get; set; }
        public double? CurrentCarStateCurrentEtcAmount { get; set; }
        public string? CurrentCarStateCarStatusDescription { get; set; }
        public string? CurrentCarStateCarFrontImg { get; set; }
        public string? CurrentCarStateCarBackImg { get; set; }
        public string? CurrentCarStateCarLeftImg { get; set; }
        public string? CurrentCarStateCarRightImg { get; set; }
        public string? CurrentCarStateCarInteriorImg { get; set; }
        public string? CurrentCarStateCarBackSeatImg { get; set; }
        public string? CurrentCarStateCarPhysicalDamage { get; set; }
        public string? DepositItemAsset { get; set; }
        public double? CarInsuranceMoney { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsExported { get; set; }
        public string? CustomerSignature { get; set; }
        public string? StaffSignature { get; set; }
        public string? FilePath { get; set; }
        public string? FileWithSignsPath { get; set; }
        public int? ContractStatusId { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual ContractStatus? ContractStatus { get; set; }
        public virtual User? Receiver { get; set; }
    }
}
