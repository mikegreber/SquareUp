namespace SquareUp.Shared.Requests;

public class AddUserRequest
{
    public int GroupId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}