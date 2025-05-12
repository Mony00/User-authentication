namespace AuthApiProject.DTOs
{
    public class RegisterDTO{

        // class created to hide the parts which the user don't have to see when accessing site, such as the id.
        // used in the controller
        public string Username { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
    }
}