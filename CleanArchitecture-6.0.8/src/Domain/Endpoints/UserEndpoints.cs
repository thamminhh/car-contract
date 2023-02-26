namespace CleanArchitecture.Domain.Endpoints;
public class UserEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/user";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string Create = Base + "/create";
    public const string UpdateRole = Base + "/update-role/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string Authenticate = Base + "/authenticate";
    public const string UpdateInfo = Base + "/update-info/{userId}";
}
