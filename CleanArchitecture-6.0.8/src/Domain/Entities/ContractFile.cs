using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractFile
    {
        public int Id { get; set; }
        public int? ContractGroupId { get; set; }
        public string? Path { get; set; }
        public string? CitizenIdentifyImage1 { get; set; }
        public string? CitizenIdentifyImage2 { get; set; }
        public string? DrivingLisenceImage1 { get; set; }
        public string? DrivingLisenceImage2 { get; set; }
        public string? HousePaperImages { get; set; }
        public string? PassportImages { get; set; }
        public string? OtherImages { get; set; }
        public string? ExpertiseContracts { get; set; }
        public string? RentContracts { get; set; }
        public string? TransferContracts { get; set; }
        public string? ReceiveContracts { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
    }
}
