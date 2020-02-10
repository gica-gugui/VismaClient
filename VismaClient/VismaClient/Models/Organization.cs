namespace VismaClient.Models
{
    public class Organization
    {
        public string Uuid { get; set; }

        public string BusinessId { get; set; }

        public string Name { get; set; }

        public Authorization Authorization { get; set; }
    }
}
