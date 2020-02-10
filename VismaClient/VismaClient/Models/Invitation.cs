using VismaClient.Enums;

namespace VismaClient.Models
{
    public class Invitation
    {
        public string Uuid { get; set; }

        public InvitationStatus Status { get; set; }

        public string Passphrase { get; set; }

        public string Email { get; set; }

        public string Sms { get; set; }

        public Document Document { get; set; }
    }
}
