### Creating the web api backend

This project has the model of the one at the [Microfost tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code) but delete the weatherforecast files created with this project.

Install the BCrypt.Net framework for password hashing. See [here](https://github.com/BcryptNet/bcrypt.net) with the command : dotnet add package BCrypt.Net-Next --version 4.0.2 

For this project, the stored data, ex User is placed in the *Models* class while the Data Transfer Object (DTOs) are stored in the *DTOs* folder. The purpose of the DTO is to define what data is sent to or from the API. DTO shape the data for specific purposes like: registering, logging in, returning user profile data. Also this DTOs are used in the model binding, where the input from the form/json has to be exact the same as the property names from the DTO class, without the Id fro mthe User class.

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

    For the login feature, the main goal is to verify that the hashed password is the same as the input password with the BCrypt framework.

    For user login and storing the logged in user, this project uses JWT (Json Web Token). It uses signing with public/private keys insted of encription.
    For using JWT install package: dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    JWT is a secure way of transmitiing sensitive information from the user to the server.Tokens are used to provide a secure method of validation. Consist of Header, Payload, Signature. The header contains metadata about the token such as the algorithm used for signing. The Payload contains the data being transmitted( passwword). The Signature ensures token integrity and is generated using the header, payload and secret key. 

    Use https://jwt.io/ for debugging token

    After login the user is redirected to .../api/user/profile. to test it in postman in the header you should have the key: Authorization, Value: Bearer {token value} and nothing in the body. The Bearer Token marks a token as JWT token and grants access to protected resources.
