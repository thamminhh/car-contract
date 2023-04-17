using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class TransferContract
    {
        public TransferContract()
        {
            TransferContractFiles = new HashSet<TransferContractFile>();
        }

        public int Id { get; set; }
        public int? TransfererId { get; set; }
        public int? ContractGroupId { get; set; }
        public DateTime? DateTransfer { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? CurrentCarStateSpeedometerNumber { get; set; }
        public int? CurrentCarStateFuelPercent { get; set; }
        public double? CurrentCarStateCurrentEtcAmount { get; set; }
        public string? CurrentCarStateCarStatusDescription { get; set; }
        public double? DepositItemDownPayment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsExported { get; set; }
        public string? CustomerSignature { get; set; }
        public string? StaffSignature { get; set; }
        public string? FilePath { get; set; }
        public string? FileWithSignsPath { get; set; }
        public int? ContractStatusId { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
        public virtual ContractStatus? ContractStatus { get; set; }
        public virtual User? Transferer { get; set; }
        public virtual ICollection<TransferContractFile> TransferContractFiles { get; set; }
    }
}
