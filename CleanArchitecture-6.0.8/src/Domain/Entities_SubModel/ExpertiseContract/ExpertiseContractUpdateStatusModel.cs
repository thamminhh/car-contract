using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.ExpertiseContract;
public class ExpertiseContractUpdateStatusModel
{
    public int Id { get; set; }
    public int? ContractStatusId { get; set; }
}
