namespace CleanArchitecture.Domain.Endpoints;

public class CarModelEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/carmodel";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarMakeId = Base + "/get-by-carMakeId/{carMakeId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
