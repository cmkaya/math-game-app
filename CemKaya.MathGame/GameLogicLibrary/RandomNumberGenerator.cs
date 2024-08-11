using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

public static class RandomNumberGenerator
{
  private static readonly Random _random = new();
  private const int minValue = 0;
  
  public static (int FirstOperand, int SecondOperand) GenerateNumberPair(
    MathOperation selectedOperation, DifficultyLevel selectedDifficulty)
  {
    int maxValue = (int)selectedDifficulty;

    int first = _random.Next(minValue, maxValue);
    int second = _random.Next(minValue, maxValue);

    // For division, ensure the result is a whole number
    if (selectedOperation == MathOperation.Division)
    {
      second = second == 0 ? 1 : second;
      first = second * _random.Next(minValue, maxValue);
    }

    return (FirstOperand: first, SecondOperand:second);
  }
}