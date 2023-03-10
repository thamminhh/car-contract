namespace CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;
public class CarScheduleCreateModel
{
    public int? CarId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int? CarStatusId { get; set; }

}
