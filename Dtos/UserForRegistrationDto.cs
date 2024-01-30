namespace DotnetAPI.Dtos
{
    public class UserForRegistrationDto
    {
        string Email { get; set; } = "";
        string Password { get; set; } = "";
        string PasswordConfirm { get; set; } = "";
    }
}