using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;
public class CarSeriesUpdateModel
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public int CarGenerationId { get; set; }
    public string CarSeriesName { get; set;}
}
