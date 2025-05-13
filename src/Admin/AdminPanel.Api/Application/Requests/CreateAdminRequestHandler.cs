using AdminPanel.Api.Entities;
using AdminPanel.Api.Persistence.Repositories;
using AdminPanel.Api.Services;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Api.Application.Commands;

public record CreateAdminRequest(
    string login,
    string password);
public class CreateAdminRequestHandler(IAdminRepository _adminRepository, IPasswordService _passwordService)
{
    public async Task<Admin> Handle(CreateAdminRequest request)
    {
        var result = Admin.Create(request.login, request.password);
        var hashedPassoword = _passwordService.HashPassword(request.password);
        result.HashPassword(hashedPassoword);

        try
        {
            await _adminRepository.InsertAsync(result);
            await _adminRepository.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }

        return await Task.FromResult(result);
    }
}
