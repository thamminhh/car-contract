namespace CleanArchitecture.Application.Constant;
public class CarStatusConstants
{
    public const int Expertising         = 1; // "Đang thẩm định"
    public const int Available           = 2; // "Sẵn sàng để thuê"
    public const int NotAvailable        = 3; //"Chưa sẵn sàng để thuê"
    public const int Renting             = 4; //"Đang được thuê"
    public const int UnderInsurance      = 5; //"Đang bảo hiểm"
    public const int UnderMaintenance    = 6; //"Đang bảo dưỡng"
    public const int Fixing              = 7; //"Đang sửa"
    public const int NotExpertising      = 8; //"Chưa thẩm định"
    public const int MaintenanceDeadline = 9; //"Tới hạn bảo dưỡng"
}
