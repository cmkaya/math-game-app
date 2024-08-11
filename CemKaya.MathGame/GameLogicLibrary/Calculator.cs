using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

/// <summary>
/// Provides static methods for performing mathematical operations.
/// </summary>
public static class Calculator
{
  /// <summary>
  /// Calculates a mathematical operation based on the selected operation and difficulty level.
  /// </summary>
  /// <param name="selectedOperation">The mathematical operation to perform.</param>
  /// <param name="selectedDifficulty">The difficulty level determining the range of numbers.</param>
  /// <returns>A tuple containing the two operands and the calculated answer.</returns>
  /// <exception cref="DivideByZeroException">Thrown when a division by zero is attempted.</exception>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when an unexpected operation is provided.</exception>
  public static (int FirstOperand, int SecondOperand, int Answer) CalculateOperation(
    MathOperation selectedOperation, DifficultyLevel selectedDifficulty)
  {
    var (first, second) = RandomNumberGenerator.GenerateNumberPair(
      selectedOperation, selectedDifficulty);

    int answer = selectedOperation switch
    {
      MathOperation.Addition => first + second,
      MathOperation.Subtraction => first - second,
      MathOperation.Multiplication => first * second,
      MathOperation.Division => second != 0
        ? first / second
        : throw new DivideByZeroException("Cannot divide by zero"),
      _ => throw new ArgumentOutOfRangeException(nameof(selectedOperation),
        $"Unexpected operation: {selectedOperation}")
    };

    return (first, second, answer);
  }
}