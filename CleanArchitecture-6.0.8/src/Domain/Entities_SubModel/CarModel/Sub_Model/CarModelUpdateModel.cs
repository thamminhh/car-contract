using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarModel.Sub_Model;
public class CarModelUpdateModel
{

    public int Id { get; set; }

    public int CarMakeId { get; set; }
    public string CarModelName { get; set; }
}
