namespace CleanArchitecture.Application.Constant;
public class ContractGroupConstant
{
    public const int ContractGroupNotExpertised  = 1; //"Nhóm hợp đồng chưa được thẩm định!"
    public const int ContractGroupIsExpertising  = 2; //"Nhóm hợp đồng đang được thẩm định"
    public const int ContractGroupNotExported    = 3; //"Nhóm hợp đồng không được duyệt"
    public const int RentContractNotExit         = 4; //"Chưa tạo hợp đồng thuê"
    public const int RentContractNotSign         = 5; //"Chưa kí HĐ thuê"
    public const int RentContractSigned          = 6; //"Đã kí HĐ thuê"
    public const int TransferContractNotCreate   = 7; //"Chưa tạo HĐ giao"
    public const int TransferContractNotSign     = 8; //"Chưa kí HĐ giao"
    public const int TransferContractSigned      = 9; //"Đã kí HĐ giao"
    public const int ReceiveContractNotCreate    = 10; //"Chưa tạo HĐ nhận"
    public const int ReceiveContractNotSign      = 11; //"Chưa kí HĐ nhận""
    public const int ReceiveContractSigned       = 12; //"Đã kí HĐ nhận"
    public const int ContractCancel              = 13; //"Đã Hủy"
    public const int CheckedInfo                 = 14; //"Đã check thông tin"

    //public const string RentContractNotExisted = "Hợp đồng thuê không tồn tại !";
    //public const string RentContractNotFinished = "Hợp đồng thuê chưa hoàn thành !";
    //public const string TransferContractNotExisted = "Hợp đồng bàn giao xe không tồn tại !";
    //public const string TransferContractNotFinished = "Hợp đồng giao xe chưa hoàn thành !";
    //public const string ReceiveContractNotExisted = "Hợp đồng nhận xe không tồn tại !";
    //public const string ReceiveContractNotFinished = "Hợp đông nhận xe chưa hoàn thành !";
    //public const string ContractNotExist = "Hợp đồng không tồn tại!";
    //public const string UserNotExist = "Người dùng không tồn tại !";
    //public const string CarNotExist = "Xe không tồn tại !";
    //public const string FileNotExist = "File không tồn tại !";
    //public const string InvalidRentTime = "Ngày thuê không hợp lệ  !";
    //public const string UserSigned = "Khách hàng đã ký !";
    //public const string StaffSigned = "Nhân viên đã ký  !";
    //public const string InvalidSchedule = "Lịch trình không hợp lệ !";
}
