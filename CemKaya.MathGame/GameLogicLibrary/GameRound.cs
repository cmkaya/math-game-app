using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

public record GameRound(
  int UserAnswer,
  int CorrectAnswer,
  int FirstOperand,
  int SecondOperand,
  TimeSpan TimeTaken,
  DifficultyLevel SelectedDifficulty,
  MathOperation SelectedOperation)
{
  public bool IsCorrect => UserAnswer == CorrectAnswer;

  public int CalculateScore()
  {
    if (IsCorrect == false) return 0;

    int score = SelectedDifficulty switch
    {
      DifficultyLevel.Easy => 1,
      DifficultyLevel.Normal => 2,
      DifficultyLevel.Hard => 3,
      _ => 0
    };

    score += TimeTaken.TotalSeconds switch
    {
      <20 => 3,
      <30 => 2,
      _ => 1
    };

    return score;
  }
}