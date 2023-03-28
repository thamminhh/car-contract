using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarTrim.Sub_Model;
public class CarTrimUpdateModel
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public int CarSeriesId { get; set; }

    public string CarTrimName { get; set; }
    public int StartProductYear { get; set; }
    public int EndProductYear { get; set; }
}
