using System.Collections.Generic;
using VismaClient.Enums;

namespace VismaClient.Models
{
    public class Document
    {
        public string Uuid { get; set; }

        public string Name { get; set; }

        public DocumentStatus Status { get; set; }

        public List<File> Files { get; set; }

        public List<Invitation> Invitations { get; set; }
    }
}
