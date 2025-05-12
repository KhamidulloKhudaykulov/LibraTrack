namespace AccountService.Application.UseCases.Users.Contracts;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
