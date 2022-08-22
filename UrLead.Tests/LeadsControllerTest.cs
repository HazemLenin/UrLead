using Microsoft.AspNetCore.Mvc.Testing;
using UrLead.Data;

namespace UrLead.Tests
{
    public class LeadsControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public LeadsControllerTest(WebApplicationFactory<Program> factory, ApplicationDbContext context)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _context = context;
        }

        [Theory]
        [InlineData("/Index")]
        public async Task TestEndpoints(string url)
        {
            // Act
            var response1 = await _client.GetAsync(url);

            // Assert
            Assert.False(response1.IsSuccessStatusCode);
        }
    }
}
