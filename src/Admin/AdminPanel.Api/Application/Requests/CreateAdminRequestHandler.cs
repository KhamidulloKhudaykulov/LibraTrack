using AdminPanel.Api.Entities;
using AdminPanel.Api.Persistence.Repositories;

namespace AdminPanel.Api.Application.Commands;

public record CreateAdminRequest(
    string login,
    string password);
public class CreateAdminRequestHandler(IAdminRepository _adminRepository)
{
    public async Task<Admin> Create(CreateAdminRequest request)
    {
        var result = Admin.Create(request.login, request.password);

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
