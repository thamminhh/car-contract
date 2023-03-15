namespace CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;
public class CarScheduleDataModel
{
    public int? Id { get; set; }
    public int? CarId { get; set; }

    public string? CarLicensePlates { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int? CarStatusId { get; set; }

    public string? CarStatusName { get; set; }
}
