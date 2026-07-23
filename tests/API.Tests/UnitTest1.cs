using Microsoft.AspNetCore.Mvc.Testing;

namespace API.Tests;

public class ProductApiIntegrationTests 
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProductApiIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Health_ReturnsSuccess()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/health");

        response.EnsureSuccessStatusCode();
    }
}