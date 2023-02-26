namespace CleanArchitecture.Domain.Entities_SubModel.User;

using System.ComponentModel.DataAnnotations;

public class UpdateUserPasswordModel
{
    public string OldPassword { get; init; }
    [StringLength(20, ErrorMessage = "Must be between 8 and 20 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string NewPassword { get; init; }

    public UpdateUserPasswordModel(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword ?? throw new ArgumentException(nameof(oldPassword));
        NewPassword = newPassword ?? throw new ArgumentException(nameof(newPassword));
    }
}