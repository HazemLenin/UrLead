using Microsoft.AspNetCore.Mvc.Testing;

namespace UrLead.Tests
{
    public class DashboardControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public DashboardControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/Index")]
        public async Task TestEndpoints(string url)
        {
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
