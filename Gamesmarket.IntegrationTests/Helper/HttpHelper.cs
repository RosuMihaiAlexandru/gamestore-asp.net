using Newtonsoft.Json;
using System.Text;

namespace Gamesmarket.IntegrationTests.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string Authenticate = "/api/account/login";
            public readonly static string Register = "/api/account/register";
            public readonly static string RefreshToken = "/api/account/refresh-token";
            public readonly static string Revoke = "/api/account/revoke/";
            public readonly static string RevokeAll = "/api/account/revoke-all";
            public readonly static string GetUsers = "/api/account/getUsers";
            public readonly static string ChangeUserRole = "/api/account/change-role";

        }
    }
}
