using zOrdo.Models.Models;

namespace zOrdo.Models.Responses;

public class UserResponse
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime InsertedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }

    public UserResponse FromUserModel(User user)
    {
        FirstName = user.FirstName;
        MiddleName = user.MiddleName;
        LastName = user.LastName;
        Email = user.Email;
        InsertedOnUtc = user.InsertedOnUtc;
        UpdatedOnUtc = user.UpdatedOnUtc;
        return this;
    }
}