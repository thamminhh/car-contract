namespace CleanArchitecture.Domain.Entities_SubModel.User;
public class UpdateUserRoleModel
{
    public string Role { get; init; }

    public UpdateUserRoleModel(string role)
    {
        Role = role;
    }
}
