using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class RentContract
    {
        public RentContract()
        {
            RentContractFiles = new HashSet<RentContractFile>();
        }

        public int Id { get; set; }
        public int? ContractGroupId { get; set; }
        public int? RepresentativeId { get; set; }
        public string? DeliveryAddress { get; set; }
        public double? CarGeneralInfoAtRentPriceForNormalDay { get; set; }
        public double? CarGeneralInfoAtRentPriceForWeekendDay { get; set; }
        public double? CarGeneralInfoAtRentPricePerKmExceed { get; set; }
        public double? CarGeneralInfoAtRentPricePerHourExceed { get; set; }
        public double? CarGeneralInfoAtRentLimitedKmForMonth { get; set; }
        public double? CarGeneralInfoAtRentPriceForMonth { get; set; }
        public double? DeliveryFee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CustomerSignature { get; set; }
        public string? StaffSignature { get; set; }
        public string? FilePath { get; set; }
        public string? FileWithSignsPath { get; set; }
        public bool? IsExported { get; set; }
        public double? PaymentAmount { get; set; }
        public double? DepositItemDownPayment { get; set; }
        public string? CancelReason { get; set; }
        public int? ContractStatusId { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual ContractStatus? ContractStatus { get; set; }
        public virtual User? Representative { get; set; }
        public virtual ICollection<RentContractFile> RentContractFiles { get; set; }
    }
}
