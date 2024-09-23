using FluentAssertions;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
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

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<BaseResponse<AuthResponse>>(responseContent);

            responseObject.Should().NotBeNull();
            responseObject.StatusCode.Should().Be(StatusCode.OK);

            var authResponse = responseObject.Data;
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

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<BaseResponse<AuthResponse>>(responseContent);

            responseObject.Should().NotBeNull();
            responseObject.StatusCode.Should().Be(StatusCode.OK);

            var authResponse = responseObject.Data;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
            return client;
        }
    }
}
