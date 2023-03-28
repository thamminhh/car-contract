namespace CleanArchitecture.Domain.Endpoints;

public class CarTrimEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/cartrim";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarModelIdAndCarSeriesId = Base + "/carModelId/{carModelId}/carSeriesId/{carSeriesId}";
    public const string GetByCarModelId = Base + "/carModelId/{carModelId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{carTrimId}";
    public const string Delete = Base + "/delete/{id}";
}
