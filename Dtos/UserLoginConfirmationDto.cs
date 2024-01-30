namespace DotnetAPI.Dtos
{
    partial class UserLoginConfirmationDto
    {
        byte[] PasswordHash { get; set; } = new byte[0];
        byte[] PasswordSalt { get; set; } = new byte[0];
    }
}