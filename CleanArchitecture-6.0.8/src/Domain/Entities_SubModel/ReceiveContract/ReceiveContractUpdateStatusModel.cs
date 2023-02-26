using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
public class ReceiveContractUpdateStatusModel
{
    public int Id { get; set; }
    public int? ContractStatusId { get; set; }
}
