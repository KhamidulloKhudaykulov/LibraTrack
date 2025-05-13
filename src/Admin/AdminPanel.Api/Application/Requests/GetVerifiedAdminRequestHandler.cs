using AdminPanel.Api.Entities;
using AdminPanel.Api.Persistence.Repositories;
using AdminPanel.Api.Services;

namespace AdminPanel.Api.Application.Requests;

public record GetVerifiedAdminRequest(
    string login,
    string password);
public class GetVerifiedAdminRequestHandler(
    IAdminRepository _adminRepository,
    IPasswordService _passwordService)
{
    public async Task<Admin> Handle(GetVerifiedAdminRequest request)
    {
        var admin = await _adminRepository.SelectAsync(
            a => a.Login == request.login);

        if (admin is null)
        {
            throw new Exception("Login or password is incorrect");
        }

        if (!_passwordService.Verify(request.password, admin.Password))
        {
            throw new Exception("Login or password is incorrect");
        }

        return admin;
    } 
}
