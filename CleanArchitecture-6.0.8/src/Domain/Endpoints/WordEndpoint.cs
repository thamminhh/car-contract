namespace CleanArchitecture.Domain.Endpoints;

public class WordEndpoint
{
    public const string Area = "";
    public const string Base = Area + "/word";
    public const string GetTemplate = Base + "/contract (tempplate)";
    public const string GetRent = Base + "/rent-contract";
    public const string GetTransfer = Base + "/transfer-contract";
    public const string GetReceive = Base + "/receive-contract";
}
