

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace TrenerPersonalny.IntegrationTests
{
    public class TrainerControllerTests
    {
        private string url = "/api/Trainer";

        [Fact]
        public async Task GetTrainers_ReturnOkResultAsync()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(url);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetTrainers_ById_ReturnOkResultAsync()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(url + "/1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
