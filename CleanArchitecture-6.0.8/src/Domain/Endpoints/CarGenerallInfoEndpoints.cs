namespace CleanArchitecture.Domain.Endpoints;

public class CarGenerallInfoEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/cargenerallinfo";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarId = Base + "/get-by-carId/{carId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
