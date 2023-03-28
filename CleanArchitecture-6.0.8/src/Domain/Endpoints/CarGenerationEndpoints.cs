namespace CleanArchitecture.Domain.Endpoints;

public class CarGenerationEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/cargeneration";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarModelId = Base + "/get-by-carModelId/{carModelId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{carGenerationId}";
    public const string Delete = Base + "/delete/{id}";
}
