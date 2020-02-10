using System;

namespace VismaClient.Models
{
    public class Authorization
    {
        public bool Authorized { get; set; }

        public DateTimeOffset? RequestedOn { get; set; }

        public DateTimeOffset? AuthorizedOn { get; set; }

        public DateTimeOffset? CanceledOn { get; set; }
    }
}
