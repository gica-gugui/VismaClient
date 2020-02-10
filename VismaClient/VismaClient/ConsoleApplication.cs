using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VismaClient.Models;
using VismaClient.Services.Interfaces;

namespace VismaClient
{
    public class ConsoleApplication: IHostedService
    {
        private readonly IDocumentService _documentService;
        private readonly IInvitationService _invitationService;
        private readonly IOrganizationService _organizationService;

        public ConsoleApplication
        (
            IDocumentService documentService, 
            IInvitationService invitationService,
            IOrganizationService organizationService
        )
        {
            _documentService = documentService;
            _invitationService = invitationService;
            _organizationService = organizationService;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var document = await _documentService.GetAsync("e59c8dc8-8848-4936-ac7c-50d9ed72085a");

                var documentLocation = await _documentService.PostAsync(new Document { Name = "New document" });

                var invitation = await _invitationService.GetAsync("2076243e-351d-4b29-86ad-c2b02d7f867d");

                var organization = await _organizationService.GetAsync("7d2ad74a-8672-414c-b570-f261f638b622");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // cleanup

            return Task.CompletedTask;
        }
    }
}
