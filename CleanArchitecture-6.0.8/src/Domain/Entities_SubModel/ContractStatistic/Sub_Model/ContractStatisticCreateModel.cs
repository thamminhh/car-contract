﻿using System;
namespace CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model
{
    public class ContractStatisticCreateModel
    {
        public int? ContractGroupId { get; set; }
        public double? EtcmoneyUsing { get; set; }
        public double? FuelMoneyUsing { get; set; }
        public double? ExtraTimeMoney { get; set; }

        public double? ExtraKmMoney { get; set; }
        public double? InsuranceMoney { get; set; }
        public double? ViolationMoney { get; set; }
        public double? PaymentAmount { get; set; }
    }
}

