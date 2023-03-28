namespace CleanArchitecture.Domain.Endpoints;

public class CustomerInfoEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/customerinfo";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCitizenIdentificationInfoNumber = Base + "/citizenIdentificationInfoNumber/{citizenIdentificationInfoNumber}";
    public const string Create = Base + "/create";
    public const string UpdateByCitizenIdentificationInfoNumber = Base + "/update/{citizenIdentificationInfoNumber}";
    public const string Delete = Base + "/delete/{id}";
}
