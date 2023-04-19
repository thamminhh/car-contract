using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;
public class CarExpenseUpdateModel
{
    public int Id { get; set; }
    public int? CarId { get; set; }
    public string? Title { get; set; }
    public DateTime? Day { get; set; }
    public double? Amount { get; set; }
}
