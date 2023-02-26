namespace CleanArchitecture.Domain.Entities_SubModel.User;

using System;

public class LoginModel
{
    public string UserName { get; init; }
    public string Password { get; init; }

    public LoginModel(string userName, string password)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}
