namespace Authentication;

public record AuthentificationResult(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Gender,
    string Image,
    string Token);