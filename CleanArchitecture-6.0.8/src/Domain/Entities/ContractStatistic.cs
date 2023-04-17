using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractStatistic
    {
        public int? Id { get; set; }
        public int? ContractGroupId { get; set; }
        public double? EtcmoneyUsing { get; set; }
        public double? FuelMoneyUsing { get; set; }
        public double? ExtraTimeMoney { get; set; }
        public double? PaymentAmount { get; set; }
        public double? Total { get; set; }

        public virtual ContractGroup? ContractGroup { get; set; }
    }
}
