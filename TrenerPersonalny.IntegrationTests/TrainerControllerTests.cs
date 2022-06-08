

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;
using Xunit;

namespace TrenerPersonalny.IntegrationTests
{
    public class TrainerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private string url = "/api/Trainer/";
        private HttpClient _client;

        public TrainerControllerTests(WebApplicationFactory<Startup> factory)
        {
           _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service =>
                            service.ServiceType == typeof(DbContextOptions<ApiDbContext>));
                        services.Remove(dbContextOptions); //usuwa aktualn¹ rejestracjê dbcontext i ustawia nowy context do pamiêci
                        
                        services.AddDbContext<ApiDbContext>(options =>
                        { options.UseInMemoryDatabase("PersTrainerDb");

                        });
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task GetTrainers_ReturnOkResultAsync()
        {
   
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("50")]
        public async Task GetTrainers_ById_ReturnOkResultAsync(string id)
        {
            var response = await _client.GetAsync(url + id);
            
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
