namespace CleanArchitecture.Application.Constant;
public class ContractGroupConstant
{
    public const int ContractGroupNotExpertised     = 1; //"Nhóm hợp đồng chưa được thẩm định!"
    public const int FailInfo                       = 2; //"Fail thông tin"
    public const int FailCar                        = 3; //"Fail xe"
    public const int ContractGroupExpertised        = 4; //"Đã thẩm định"
    public const int RentContractNotExit            = 5; //"Chưa tạo hợp đồng thuê"
    public const int RentContractNotSign            = 6; //"Chưa kí HĐ thuê"
    public const int RentContractSigned             = 7; //"Đã kí HĐ thuê"
    public const int TransferContractNotCreate      = 8; //"Chưa tạo HĐ giao"
    public const int TransferContractNotSign        = 9; //"Chưa kí HĐ giao"
    public const int TransferContractSigned         = 10; //"Đã kí HĐ giao"
    public const int ReceiveContractNotCreate       = 11; //"Chưa tạo HĐ nhận"
    public const int ReceiveContractNotSign         = 12; //"Chưa kí HĐ nhận""
    public const int ReceiveContractSigned          = 13; //"Đã kí HĐ nhận"
    public const int RentContractCancel             = 14; //"Đã Hủy HĐ thuê"
    public const int TransferContractCancel         = 15; //"Đã Hủy HĐ giao"
    public const int ContractInspecting             = 16; //"Đang nghiệm thu"

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
