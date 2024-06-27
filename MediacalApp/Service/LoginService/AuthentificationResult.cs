namespace MediacalApp.Service.LoginService;

public record AuthentificationResult(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Gender,
    string Image,
    string Token);