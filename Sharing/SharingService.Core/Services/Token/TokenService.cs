using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharingService.Core.Services.Token
{
    public class TokenService: ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenServiceSettings _settings;

        public TokenService(HttpClient httpClient, TokenServiceSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<string> RequestToken()
        {
            // Get the AAD app token
            var aadTenantId = _settings.AadClientId;
            var authority = $"https://login.microsoftonline.com/{aadTenantId}";
            var clientCredential = new ClientCredential(
                _settings.AadClientId,
                _settings.AadClientKey);
            var authenticationContext = new AuthenticationContext(authority);
            var authenticationResult = await authenticationContext.AcquireTokenAsync(
                _settings.SpatialAnchorsResource,
                clientCredential);
            string aadAppToken = authenticationResult.AccessToken;

            // Use the AAD app token to request a Spatial Anchors token
            using (var httpRequest = new HttpRequestMessage())
            {
                var spatialAnchorsAccountId = _settings.SpatialAnchorsAccountId;
                Uri.TryCreate($"https://sts.mixedreality.azure.com/Accounts/{spatialAnchorsAccountId}/token", UriKind.Absolute, out Uri uri);
                httpRequest.Method = HttpMethod.Get;
                httpRequest.RequestUri = uri;
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", aadAppToken);

                using (var httpResponse = await _httpClient.SendAsync(httpRequest))
                {
                    var responseContent = await httpResponse.Content.ReadAsAsync<Token>();
                    return responseContent.AccessToken;
                }
            }
        }
    }
}
