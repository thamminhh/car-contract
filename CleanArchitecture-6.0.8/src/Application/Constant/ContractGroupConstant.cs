namespace CleanArchitecture.Application.Constant;
public class ContractGroupConstant
{
    public const int ContractGroupNotExpertised  = 1; //"Nhóm hợp đồng chưa được thẩm định!"
    public const int ContractGroupIsExpertising  = 2; //"Nhóm hợp đồng đang được thẩm định"
    public const int ContractGroupNotExported    = 3; //"Nhóm hợp đồng không được duyệt"
    public const int ContractGroupExported       = 4; //"Hợp đông đã được duyệt"

    public const string RentContractNotExisted = "Hợp đồng thuê không tồn tại !";
    public const string RentContractNotFinished = "Hợp đồng thuê chưa hoàn thành !";
    public const string TransferContractNotExisted = "Hợp đồng bàn giao xe không tồn tại !";
    public const string TransferContractNotFinished = "Hợp đồng giao xe chưa hoàn thành !";
    public const string ReceiveContractNotExisted = "Hợp đồng nhận xe không tồn tại !";
    public const string ReceiveContractNotFinished = "Hợp đông nhận xe chưa hoàn thành !";
    public const string ContractNotExist = "Hợp đồng không tồn tại!";
    public const string UserNotExist = "Người dùng không tồn tại !";
    public const string CarNotExist = "Xe không tồn tại !";
    public const string FileNotExist = "File không tồn tại !";
    public const string InvalidRentTime = "Ngày thuê không hợp lệ  !";
    public const string UserSigned = "Khách hàng đã ký !";
    public const string StaffSigned = "Nhân viên đã ký  !";
    public const string InvalidSchedule = "Lịch trình không hợp lệ !";
}
