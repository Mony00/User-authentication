namespace AuthApiProject.Models
{
    public class User
    {
        public int Id { get; set; } // only used in database
        public string Username { get; set; } = string.Empty; // has to initialize an empty string otherwise error
        public string PasswordHash { get; set; } = string.Empty;
    }
}