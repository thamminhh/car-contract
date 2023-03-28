using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.AppraisalRecord;
public class AppraisalRecordDataModel
{
    public int Id { get; set; }
    public int? CarId { get; set; }
    public int? ContractGroupId { get; set; }
    public int? ExpertiserId { get; set; }
    public DateTime? ExpertiseDate { get; set; }
    public bool? ResultOfInfo { get; set; }
    public bool? ResultOfCar { get; set; }
    public string? ResultDescription { get; set; }
    public double? DepositInfoCarRental { get; set; }
    public double? DepositInfoDownPayment { get; set; }
    public string? FilePath { get; set; }

}
