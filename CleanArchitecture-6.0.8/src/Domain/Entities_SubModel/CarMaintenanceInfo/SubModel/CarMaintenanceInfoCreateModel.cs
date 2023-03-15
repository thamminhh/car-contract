using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo.SubModel;
public class CarMaintenanceInfoCreateModel
{
    public int? CarId { get; set; }
    public double? CarKmlastMaintenance { get; set; }
    public double? KmTraveled { get; set; }
    public DateTime? MaintenanceDate { get; set; }
    public string? MaintenanceInvoice { get; set; }
    public double? MaintenanceAmount { get; set; }

}
