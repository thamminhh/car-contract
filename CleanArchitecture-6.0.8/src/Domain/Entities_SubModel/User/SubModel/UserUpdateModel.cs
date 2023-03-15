namespace CleanArchitecture.Domain.Entities_SubModel.User;

using System;


public class UserUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Job { get; set; }

    public string? CurrentAddress { get; set; }

    public string Email { get; set; } = null!;
    public string? CardImage { get; set; }

    public string? CitizenIdentificationInfoNumber { get; set; }

    public string? CitizenIdentificationInfoAddress { get; set; }

    public DateTime? CitizenIdentificationInfoDateReceive { get; set; }

    public string? PassportInfoNumber { get; set; }

    public string? PassportInfoAddress { get; set; }

    public DateTime? PassportInfoDateReceive { get; set; }
    public string? Role { get; set; }

    public bool? isDeleted { get; set; }

}
