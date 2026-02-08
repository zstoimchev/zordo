using zOrdo.Models.Requests;

namespace zOrdo.Models.Models;

public class User : SharedProperties
{
    /// <summary>
    /// First name of the user
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Middle name of the user
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Password hash of the user
    /// </summary>
    public string? PasswordHash { get; set; }

    public User FromUserRequest(UserRequest request)
    {
        FirstName = request.FirstName;
        MiddleName = request.MiddleName;
        LastName = request.LastName;
        Email = request.Email;
        return this;
    }
}