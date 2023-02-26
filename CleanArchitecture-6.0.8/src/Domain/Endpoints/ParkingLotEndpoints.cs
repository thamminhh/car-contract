namespace CleanArchitecture.Domain.Endpoints;

public class ParkingLotEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/parkinglot";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
