namespace zOrdo.Models.Models;

public class User : SharedProperties
{
    /// <summary>
    /// First name of the user
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Middle name of the user
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Last name of the user
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Email address of the user
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Password hash of the user
    /// </summary>
    public string? PasswordHash { get; set; }
}