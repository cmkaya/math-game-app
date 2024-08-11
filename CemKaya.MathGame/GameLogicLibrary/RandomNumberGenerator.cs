using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

/// <summary>
/// Provides static methods for generating random number pairs for mathematical operations.
/// </summary>
public static class RandomNumberGenerator
{
  private static readonly Random _random = new();

  /// <summary>
  /// Generates a pair of random numbers based on the selected operation and difficulty level.
  /// </summary>
  /// <param name="selectedOperation">The mathematical operation to be performed.</param>
  /// <param name="selectedDifficulty">The difficulty level determining the range of numbers.</param>
  /// <returns>A tuple containing two randomly generated integers.</returns>
  /// <remarks>
  /// For division operations, the method ensures that the division will result in a whole number.
  /// </remarks>
  public static (int, int) GenerateNumberPair(
    MathOperation selectedOperation, DifficultyLevel selectedDifficulty)
  {
    int minValue = 0;
    int maxValue = (int)selectedDifficulty;

    int first = _random.Next(minValue, maxValue);
    int second = _random.Next(minValue, maxValue);

    // For division, ensure the result is a whole number
    if (selectedOperation == MathOperation.Division)
    {
      second = second == 0 ? 1 : second;
      first = second * _random.Next(minValue, 10);
    }

    return (first, second);
  }
}