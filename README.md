### Creating the web api backend

This project has the model of the one at the [Microfost tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code) but delete the weatherforecast files created with this project.

Install the BCrypt.Net framework for password hashing. See [here](https://github.com/BcryptNet/bcrypt.net) with the command : dotnet add package BCrypt.Net-Next --version 4.0.2 

For this project, the stored data, ex User is placed in the *Models* class while the Data Transfer Object (DTOs) are stored in the *DTOs* folder. The purpose of the DTO is to define what data is sent to or from the API. DTO shape the data for specific purposes like: registering, logging in, returning user profile data. Also this DTOs are used in the model binding, where the input fro mthe form/json has to be exact the same as the property names fro mthe DTO class, without the Id fro mthe User class.

Example of User class:
public class User
    {
        public int Id { get; set; } // only used in database
        public string Username { get; set; } = string.Empty; // has to initialize an empty string otherwise error
        public string PasswordHash { get; set; } = string.Empty; // saving the password in a hashed form
    }
And Register class:
    public class RegisterDTO{

        // class created to hide the parts which the user don't have to see when accessing site, such as the id.
        // used in the controller
        public string Username { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
    }