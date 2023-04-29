using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
public class ContractGroupCustomerHistoryModel
{
    public int Id { get; set; }
    public DateTime? RentFrom { get; set; }
    public DateTime? RentTo { get; set; }
    public int? ContractGroupStatusId { get; set; }
    public string? ContractGroupStatusName { get; set; } // public int? ContractGroupStatusId { get; set; }
}
