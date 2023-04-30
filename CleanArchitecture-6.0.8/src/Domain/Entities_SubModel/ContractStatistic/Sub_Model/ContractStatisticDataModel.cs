using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model;
public class ContractStatisticDataModel
{
    public int? Id { get; set; }
    public int? ContractGroupId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public double? EtcmoneyUsing { get; set; }
    public double? FuelMoneyUsing { get; set; }
    public double? ExtraTimeMoney { get; set; }
    public double? ExtraKmMoney { get; set; }
    public double? InsuranceMoney { get; set; }
    public double? ViolationMoney { get; set; }
    public double? PaymentAmount { get; set; }
    public double? Total { get; set; }
}
