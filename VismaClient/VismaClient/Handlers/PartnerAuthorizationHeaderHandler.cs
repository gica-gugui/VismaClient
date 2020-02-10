using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using VismaClient.Options;
using VismaClient.Services.Interfaces;

namespace VismaClient.Handlers
{
    public class PartnerAuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly IBearerService _bearerService;
        private readonly AppOptions _appOptions;
        private readonly PartnerOptions _partnerOptions;

        public PartnerAuthorizationHeaderHandler
        (
            IBearerService bearerService, 
            IOptions<AppOptions> appOptions, 
            IOptions<PartnerOptions> partnerOptions
        )
        {
            _bearerService = bearerService;
            _appOptions = appOptions.Value;
            _partnerOptions = partnerOptions.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Date = request.Headers.Date.GetValueOrDefault(DateTime.UtcNow);

            var bearerToken = await _bearerService.PostAsync(_partnerOptions.Identifier, _partnerOptions.Secret, _partnerOptions.Scopes);

            request.Headers.Authorization =
               new AuthenticationHeaderValue(
                   "Bearer",
                   bearerToken.AccessToken
                );

            return await base.SendAsync(request, cancellationToken);
        }
    }
}