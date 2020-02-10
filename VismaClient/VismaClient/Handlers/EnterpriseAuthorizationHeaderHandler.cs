using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VismaClient.Options;

namespace VismaClient.Handlers
{
    public class EnterpriseAuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly AppOptions _appOptions;
        private readonly EnterpriseOptions _entOptions;

        private HashAlgorithm macHash;
        private HashAlgorithm contentHash;

        public EnterpriseAuthorizationHeaderHandler(IOptions<AppOptions> appOptions, IOptions<EnterpriseOptions> entOptions)
        {
            _appOptions = appOptions.Value;
            _entOptions = entOptions.Value;

            this.macHash = new HMACSHA512(Convert.FromBase64String(_entOptions.Secret));
            this.contentHash = new MD5CryptoServiceProvider();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                request.Content.Headers.ContentMD5 = contentHash.ComputeHash(await request.Content.ReadAsByteArrayAsync());
            }

            request.Headers.Date = request.Headers.Date.GetValueOrDefault(DateTime.UtcNow);

            request.Headers.Authorization =
               new AuthenticationHeaderValue(
                   "Onnistuu",
                   _entOptions.Identifier + ":" +
                    Convert.ToBase64String(
                        macHash.ComputeHash(
                            Encoding.UTF8.GetBytes(
                                string.Join(
                                    "\n",
                                    new string[] {
                                        request.Method.ToString(),
                                        Convert.ToBase64String(request.Content != null ? request.Content.Headers.ContentMD5 : contentHash.ComputeHash(new byte[] {})),
                                        request.Content != null ? request.Content.Headers.ContentType.ToString() : "",
                                        request.Headers.Date.GetValueOrDefault(DateTime.UtcNow).ToString("r"),
                                        request.RequestUri.ToString().Replace(_appOptions.BaseAddress, "")
                                    }
                                )
                            )
                        )
                    )
                );

            return await base.SendAsync(request, cancellationToken);
        }
    }
}