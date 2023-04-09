namespace CleanArchitecture.Domain.Endpoints;

public class CarEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/car";
    public const string GetAll = Base + "/all";
    public const string GetAllActive = Base + "/active";
    public const string GetByStatus = Base + "/status";
    public const string GetByCarMakeName = Base + "/carMakeName";
    public const string GetSingle = Base + "/{carId}";
    public const string GetCarsMaintenance = Base + "/need-maintenance";
    public const string GetCarsMaintenanceByParkingLotId = Base + "/need-maintenance/{parkingLotId}";
    public const string GetCarsRegistry = Base + "/need-registry";
    public const string GetCarsRegistryByParkingLotId = Base + "/need-registry/{parkingLotId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string UpdateCarStatus = Base + "/update-status/{id}";
    public const string Delete = Base + "/delete/{id}";
}
