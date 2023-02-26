using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractGroup
    {
        public int Id { get; set; }
        public int? CustomerInfoId { get; set; }
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public string? RentPurpose { get; set; }
        public DateTime? RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public int? RequireDescriptionInfoCarClass { get; set; }
        public string? RequireDescriptionInfoCarBrand { get; set; }
        public int? RequireDescriptionInfoSeatNumber { get; set; }
        public int? RequireDescriptionInfoYearCreate { get; set; }
        public string? RequireDescriptionInfoCarColor { get; set; }
        public int? ContractGroupStatusId { get; set; }
        public string? DeliveryAddress { get; set; }

        public virtual Car? Car { get; set; }
        public virtual ContractGroupStatus? ContractGroupStatus { get; set; }
        public virtual CustomerInfo? CustomerInfo { get; set; }
        public virtual User? User { get; set; }
        public virtual ContractFile? ContractFile { get; set; }
        public virtual ExpertiseContract? ExpertiseContract { get; set; }
        public virtual ReceiveContract? ReceiveContract { get; set; }
        public virtual RentContract? RentContract { get; set; }
        public virtual TransferContract? TransferContract { get; set; }
    }
}
