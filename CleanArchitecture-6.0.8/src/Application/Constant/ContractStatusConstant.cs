namespace CleanArchitecture.Application.Constant;
public class ContractStatusConstant
{
    public const int ContractExporting  = 1; //"Hợp đồng đang chờ kí"
    public const int ContractExported   = 2; //"Hợp đồng đã kí"
    public const int ContractCancelled  = 3; //"Hợp đồng đã hủy"

    public const string ContractExportingName = "Hợp đồng đang chờ kí"; //"Hợp đồng đang chờ kí"
    public const string ContractExportedName = "Hợp đồng đã kí"; //"Hợp đồng đã kí"
    public const string ContractCancelledName = "Hợp đồng đã hủy"; //"Hợp đồng đã hủy"
}
