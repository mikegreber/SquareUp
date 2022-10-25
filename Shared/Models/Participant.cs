namespace SquareUp.Shared.Models;

public interface IParticipant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
}

public class Participant : IParticipant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}