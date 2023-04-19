namespace CleanArchitecture.Domain.Endpoints;

public class CarExpenseEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/carExpense";
    public const string GetSingle = Base + "/carExpenseId/{carExpenseId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string GetByCarId = Base + "/carId/{carId}";
    public const string Delete = Base + "/{carExpenseId}";
}
