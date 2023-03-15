using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo.SubModel;
public class CarRegistryInfoCreateModel
{
    public int? CarId { get; set; }
    public DateTime? LastRegistryDate { get; set; }
    public DateTime? RegistryDate { get; set; }
    public double? RegistryAmount { get; set; }
    public string? RegistryInvoice { get; set; }
}
