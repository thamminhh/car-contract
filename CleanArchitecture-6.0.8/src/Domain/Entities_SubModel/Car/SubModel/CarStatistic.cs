using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
public class CarStatistic
{
    public int CarId { get; set; }
    public int? ParkingLotId { get; set; }
    public string CarLicensePlates { get; set; }
    public ICollection<CarExpenseUpdateModel>? CarExpenses { get; set; }
    public ICollection<CarRevenue>? CarRevenues { get; set; }
}
