namespace DavidMorales.Services.Api.ViewModels
{
    public class UserCreateViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }

        public string Password { get; set; }

        public PersonCreateViewModel Person { get; set; }
    }
}
