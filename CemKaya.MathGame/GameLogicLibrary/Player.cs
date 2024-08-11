namespace GameLogicLibrary;

public class Player
{
  private List<GameRound> _gameRounds = new();
  public string? Name { get; }

  public Player(string name)
  {
    Name = name;
  }

  public int GetTotalScore() => _gameRounds.Sum(round => round.CalculateScore());
  public int GetHighestScore() => _gameRounds.Max(round => round.CalculateScore());
  public void AddGameRound(GameRound theRound) => _gameRounds.Add(theRound);
  public IReadOnlyList<GameRound> GetGameHistory() => _gameRounds.AsReadOnly();
}