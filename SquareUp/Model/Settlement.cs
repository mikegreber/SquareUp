namespace SquareUp.Model;

public class Settlement
{
    public ObservableParticipant From { get; set; }
    public ObservableParticipant To { get; set; }
    public decimal Amount { get; set; }
}