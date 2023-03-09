namespace CleanArchitecture.Domain.Entities_SubModel.User;

using System.ComponentModel.DataAnnotations;

public class UpdateUserPasswordModel
{

    public string UserName { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }

    public string ConfirmPassword { get; init;}

    public UpdateUserPasswordModel(string userName, string oldPassword, string newPassword, string confirmPassword)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        OldPassword = oldPassword ?? throw new ArgumentNullException(nameof(oldPassword));
        NewPassword = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        ConfirmPassword = confirmPassword ?? throw new ArgumentNullException(nameof(confirmPassword));
    }


}