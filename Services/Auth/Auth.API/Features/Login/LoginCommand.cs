namespace Auth.API.Features.Login;

public record class LoginCommand(string Email, string Password);


public record LoginResponse(string Email, string JWTToken, string RefreshToken);