using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarSchedule;
public class CarScheduleDataModel
{
    public int? Id { get; set; }
    public int? CarId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int? CarStatusId { get; set; }

    public string? CarStatusName { get; set; }
}
