using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class AppraisalRecord
    {
        public int Id { get; set; }
        public int? ContractGroupId { get; set; }
        public int? ExpertiserId { get; set; }
        public DateTime? ExpertiseDate { get; set; }
        public bool? ResultOfInfo { get; set; }
        public bool? ResultOfCar { get; set; }
        public string? ResultDescription { get; set; }
        public string? DepositInfoDescription { get; set; }
        public string? DepositInfoAsset { get; set; }
        public double? DepositInfoDownPayment { get; set; }
        public double? PaymentAmount { get; set; }
        public string? FilePath { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual User? Expertiser { get; set; }
    }
}
