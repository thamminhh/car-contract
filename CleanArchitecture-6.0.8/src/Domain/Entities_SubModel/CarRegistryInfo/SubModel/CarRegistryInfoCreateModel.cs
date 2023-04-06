using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo.SubModel;
public class CarRegistryInfoCreateModel
{
    public int? CarId { get; set; }
    public DateTime? RegistrationDeadline { get; set; }
    public double? RegistryAmount { get; set; }
    public string? RegistryInvoice { get; set; }
    public string? RegistryAddress { get; set; }
    public string? CertificateRegistryDocument { get; set; }
}
