namespace MediacalApp.Service.LoginService;

public record DummyUser(string Username, string Password, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}