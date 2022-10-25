namespace SquareUp.Shared.Requests;

public class AddParticipantRequest
{
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
}