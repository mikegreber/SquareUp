namespace SquareUp.Shared.Requests;

public class InviteParticipantRequest
{
    public int ParticipantId { get; set; }
    public int GroupId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}