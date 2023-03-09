namespace CleanArchitecture.Domain.Endpoints;

public class CarScheduleEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/carschedule";
    public const string GetAll = Base + "/all";
    public const string GetSingle = Base + "/carScheduleId/{carScheduleId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string GetByCarId = Base + "/carId/{carId}";
    public const string GetByCarStatusId = Base + "/{carStatusId}";
}
