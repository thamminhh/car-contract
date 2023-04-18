using System;
namespace CleanArchitecture.Domain.Entities_SubModel.CarStatistic.Sub_Model
{
    public class CarStatisticUpdateModel
    {
        public int? Id { get; set; }
        public int? ContractGroupId { get; set; }
        public double? EtcmoneyUsing { get; set; }
        public double? FuelMoneyUsing { get; set; }
        public double? ExtraTimeMoney { get; set; }
        public double? PaymentAmount { get; set; }
        public double? Total { get; set; }
    }
}

