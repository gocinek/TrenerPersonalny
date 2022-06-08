using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models.DTOs.Requests;
using TrenerPersonalny.Models.DTOs.Responses;
using Xunit;

namespace TrenerPersonalny.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private string url = "/api/Trainer/";
        private HttpClient _client;

        public AccountControllerTests(WebApplicationFactory<Startup> factory)
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
        public async Task Login_ForRegisteredUser_ReturnOkResultAsync()
        {
            var loginDto = new UserLoginRequest()
            {
                Username = "admin",
                Password = "Test1!"
            };

            //var httpContext = loginDto.ToJsonHttpContext();
                
        }

        [Fact]
        public async Task Register_ForRegisteredUser_ReturnOkResultAsync()
        {
            var registerUser = new UserRegistrationDto()
            {
                Username = "admin2",
                Password = "Test1!",
                Email = "admin2@wp.pl"
            };

            //var httpContext = registerUser.
        }

        }
}
