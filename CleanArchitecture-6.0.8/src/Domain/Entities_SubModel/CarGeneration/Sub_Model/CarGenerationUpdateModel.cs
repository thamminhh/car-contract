using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarGeneration.Sub_Model;
public class CarGenerationUpdateModel
{
    public int Id { get; set; }
    public int CarModelId { get; set; }

    public string CarGenerationName { get; set;}
    public int? YearBegin { get; set;}
    public int? YearEnd { get; set;}
}
