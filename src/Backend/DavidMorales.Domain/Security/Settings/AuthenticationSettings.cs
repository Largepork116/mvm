namespace DavidMorales.Domain.Security.Settings
{
    public class AuthenticationSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigninKey { get; set; }
    }
}
