using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractGroup
    {
        public ContractGroup()
        {
            AppraisalRecords = new HashSet<AppraisalRecord>();
            RentContracts = new HashSet<RentContract>();
        }

        public int Id { get; set; }
        public int? CustomerInfoId { get; set; }
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public string? RentPurpose { get; set; }
        public DateTime? RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public string? RequireDescriptionInfoCarBrand { get; set; }
        public int? RequireDescriptionInfoSeatNumber { get; set; }
        public double? RequireDescriptionInfoPriceForDay { get; set; }
        public string? RequireDescriptionInfoCarColor { get; set; }
        public string? RequireDescriptionInfoGearBox { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? ContractGroupStatusId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual ContractGroupStatus? ContractGroupStatus { get; set; }
        public virtual CustomerInfo? CustomerInfo { get; set; }
        public virtual User? User { get; set; }
        public virtual ReceiveContract? ReceiveContract { get; set; }
        public virtual TransferContract? TransferContract { get; set; }
        public virtual ICollection<AppraisalRecord> AppraisalRecords { get; set; }
        public virtual ICollection<RentContract> RentContracts { get; set; }
    }
}
