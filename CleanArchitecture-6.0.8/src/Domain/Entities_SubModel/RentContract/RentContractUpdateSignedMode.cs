using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.RentContract;
public class RentContractUpdateSignedModel
{
    public int Id { get; set; }
    public string? FileWithSignPath { get; set; }
}
