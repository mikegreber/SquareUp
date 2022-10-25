namespace SquareUp.Shared.Requests;

public class GroupRequest
{
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ExtraCategories { get; set; } = string.Empty;
}