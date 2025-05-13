namespace AdminPanel.Api.Services;

public interface IPasswordService
{
    public string HashPassword(string password);
    public bool Verify(string password, string hashedPassword);
}
