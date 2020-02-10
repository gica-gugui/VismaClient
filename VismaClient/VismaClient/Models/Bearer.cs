namespace VismaClient.Models
{
    public class Bearer
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string Scope { get; set; }
    }
}
