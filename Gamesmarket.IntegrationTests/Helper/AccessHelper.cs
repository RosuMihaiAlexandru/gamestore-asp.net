using FluentAssertions;
using Gamesmarket.Domain.ViewModel.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Gamesmarket.IntegrationTests.Helper
{
    internal class AccessHelper
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AccessHelper(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public async Task<Tuple<string, string>> GetAdminTokens()
        {
            var authRequest = new AuthRequest
            {
                Email = "admin@gmail.com",
                Password = "Qwe!23"
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync(HttpHelper.Urls.Authenticate, HttpHelper.GetJsonHttpContent(authRequest));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            var accessToken = authResponse.Token;
            var refreshToken = authResponse.RefreshToken;
            return Tuple.Create(accessToken, refreshToken);
        }

        public async Task<HttpClient> GetAuthorizedClient(string email, string password)
        {
            var authRequest = new AuthRequest
            {
                Email = email,
                Password = password
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync(HttpHelper.Urls.Authenticate, HttpHelper.GetJsonHttpContent(authRequest));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
            return client;
        }
    }
}
