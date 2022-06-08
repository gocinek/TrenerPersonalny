using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;
using Xunit;

namespace TrenerPersonalny.IntegrationTests
{
    public class ExcerciseControllesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private string url = "/api/Trainer/";
        private HttpClient _client;

        public ExcerciseControllesTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
                 .WithWebHostBuilder(builder =>
                 {
                     builder.ConfigureServices(services =>
                     {
                         var dbContextOptions = services
                             .SingleOrDefault(service =>
                             service.ServiceType == typeof(DbContextOptions<ApiDbContext>));
                         services.Remove(dbContextOptions); //usuwa aktualną rejestrację dbcontext i ustawia nowy context do pamięci

                         services.AddDbContext<ApiDbContext>(options =>
                          {
                              options.UseInMemoryDatabase("PersTrainerDb");

                          });
                     });
                 })
                 .CreateClient();
        }

        [Fact]
        public async Task CreateExcercise_WithValidModel_ReturnCreatedAtRoute()
        {
            var model = new CreateExcerciseDTO()
            {
                Name = "Test1",
                Description = "dsfdsfdscfdsf",
              //  File,
                ExcerciseTypeId = 2
            };
            }
    }
}
