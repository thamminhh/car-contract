using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Domain.Entities_SubModel.User.SubModel
{
    public class UserFilter
    {
        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }
    }
}
