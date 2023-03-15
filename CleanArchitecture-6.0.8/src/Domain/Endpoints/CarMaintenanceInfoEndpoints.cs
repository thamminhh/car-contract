namespace CleanArchitecture.Domain.Endpoints;

public class CarRegistryInfoEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string CreateExpertise = Base + "/create-expertise";
    //public const string UpdateExpertise = Base + "/update-expertise";
    //public const string ExportCarRegistryInfo = Base + "/export-expertise-contract/{groupId}";


    public const string Area = "";
    public const string Base = Area + "/carregistryinfo";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarId = Base + "/get-by-carId/{carId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
