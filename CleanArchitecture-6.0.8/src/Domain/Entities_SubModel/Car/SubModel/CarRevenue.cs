using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
public class CarRevenue
{
    public int? Id { get; set; }
    public int? ContractGroupId { get; set; }
    public double? Total { get; set; }
}
