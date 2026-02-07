namespace zOrdo.Models.Responses;

public class UserResponse
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime InsertedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
}