namespace SquareUp.Shared.Requests;

public class AddGroupRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}