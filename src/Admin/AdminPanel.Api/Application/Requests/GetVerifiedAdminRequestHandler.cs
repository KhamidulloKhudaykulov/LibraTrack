using AdminPanel.Api.Entities;
using AdminPanel.Api.Persistence.Repositories;
using AdminPanel.Api.Services;
using System.Net.Http.Headers;

namespace AdminPanel.Api.Application.Requests;

public record GetVerifiedAdminRequest(
    string login,
    string password);
public class GetVerifiedAdminRequestHandler
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordService _passwordService;
    private readonly HttpClient _httpClient;

    public GetVerifiedAdminRequestHandler(
        IAdminRepository adminRepository,
        IPasswordService passwordService,
        IHttpClientFactory httpClientFactory)
    {
        _adminRepository = adminRepository;
        _passwordService = passwordService;
        _httpClient = httpClientFactory.CreateClient("IdentityService");
    }

    public async Task<string> Handle(GetVerifiedAdminRequest request)
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

        var response = await _httpClient.PostAsync($"api/identity?login={admin.Login}", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Identity service error: {error}");
        }

        var token = await response.Content.ReadAsStringAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return token;
    } 
}
