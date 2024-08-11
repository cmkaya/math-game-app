using GameLogicLibrary.Enums;

namespace GameLogicLibrary;

public static class Calculator
{
  public static (int FirstOperand, int SecondOperand, int CorrectAnswer) CalculateOperation(
    MathOperation selectedOperation, DifficultyLevel selectedDifficulty)
  {
    // Validate input enums
    if (!Enum.IsDefined(selectedOperation))
      throw new ArgumentException("Invalid operation", nameof(selectedOperation));
    if (!Enum.IsDefined(selectedDifficulty))
      throw new ArgumentException("Invalid difficulty", nameof(selectedDifficulty));
    
    // Generate number pair and calculate result based on selected operation
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