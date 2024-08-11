using System.Diagnostics;
using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

public class GameManager
{
  private Player _currentPlayer;
  private Stopwatch _stopwatch = new();
  public DifficultyLevel CurrentDifficulty { get; set; }
  public MathOperation CurrentOperation { get; set; }
  private (int FirstOperand, int SecondOperand, int CorrectAnswer) _currentQuestion;

  public GameManager(string currentPlayer)
  {
    _currentPlayer = new Player(currentPlayer);
  }

  public string StartRoundWithQuestion()
  {
    _currentQuestion = Calculator.CalculateOperation(CurrentOperation, CurrentDifficulty);

    _stopwatch.Reset();
    _stopwatch.Start();

    return
      $"{_currentQuestion.FirstOperand} {GetOperationSymbol(CurrentOperation)} {_currentQuestion.SecondOperand} = ?";
  }

  public string EndRound(int userAnswer)
  {
    _stopwatch.Stop();
    TimeSpan timeTakenToAnswer = _stopwatch.Elapsed;

    GameRound round = new GameRound(
      CorrectAnswer: _currentQuestion.CorrectAnswer,
      UserAnswer: userAnswer,
      FirstOperand: _currentQuestion.FirstOperand,
      SecondOperand: _currentQuestion.SecondOperand,
      TimeTaken: timeTakenToAnswer,
      SelectedDifficulty: CurrentDifficulty,
      SelectedOperation: CurrentOperation);
    
    _currentPlayer.AddGameRound(round);
    
    return String.Format("Correct answer: {0} - User answer: {1} => Result: {2}",
      arg0: round.CorrectAnswer,
      arg1: round.UserAnswer,
      arg2: round.IsCorrect);
  }

  public IReadOnlyList<GameRound> GetGameHistory()
  {
    return _currentPlayer.GetGameHistory();
  }

  public int GetTotalScore()
  {
    return _currentPlayer.GetTotalScore();
  }

  public (int score, bool isSuccess) GetHighestScore()
  {
    try
    {
      int highestScore = _currentPlayer.GetHighestScore();
      return (highestScore, true);
    }
    catch (InvalidOperationException)
    {
      return (0, false);
    }
  }

  private string GetOperationSymbol(MathOperation currentOperation)
  {
    return currentOperation switch
    {
      MathOperation.Addition => "+",
      MathOperation.Subtraction => "-",
      MathOperation.Multiplication => "*",
      MathOperation.Division => "/",
      _ => throw new ArgumentOutOfRangeException(nameof(currentOperation))
    };
  }
}