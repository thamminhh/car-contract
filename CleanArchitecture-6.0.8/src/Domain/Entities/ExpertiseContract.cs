using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ExpertiseContract
    {
        public int Id { get; set; }
        public int? ContractGroupId { get; set; }
        public int? ExpertiserId { get; set; }
        public DateTime? ExpertiseDate { get; set; }
        public string? Description { get; set; }
        public string? Result { get; set; }
        public string? ResultOther { get; set; }
        public string? TrustLevel { get; set; }
        public string? DepositInfoDescription { get; set; }
        public string? DepositInfoAsset { get; set; }
        public double? DepositInfoDownPayment { get; set; }
        public double? PaymentAmount { get; set; }
        public string? FilePath { get; set; }
        public bool? IsDeposited { get; set; }
        public bool? IsExported { get; set; }
        public int? ContractStatusId { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual ContractStatus? ContractStatus { get; set; }
        public virtual User? Expertiser { get; set; }
    }
}
