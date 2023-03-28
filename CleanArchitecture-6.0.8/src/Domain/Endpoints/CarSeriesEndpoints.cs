namespace CleanArchitecture.Domain.Endpoints;

public class CarSeriesEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/carseries";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarModelIdAndCarGenerationId = Base + "/carModelId/{carModelId}/carGenerationId/{carGenerationId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{carSeriesId}";
    public const string Delete = Base + "/delete/{id}";
}
