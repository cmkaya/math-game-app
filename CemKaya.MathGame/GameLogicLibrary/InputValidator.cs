namespace GameLogicLibrary;

public class InputValidator
{
  public static bool IsOnlyLetters(string text)
  {
    return (string.IsNullOrEmpty(text) == false) && text.All(char.IsLetter);
  }

  /// <summary>
  /// Validates if the input string is a positive integer within a specified range.
  /// </summary>
  /// <param name="input">The string to validate.</param>
  /// <param name="maxAllowedValue">The maximum allowed value (inclusive).</param>
  /// <returns>True if input is a positive integer not exceeding maxAllowedValue, otherwise false.</returns>
  public static bool IsIntegerInRange(string input, int maxAllowedValue)
  {
    if (int.TryParse(input, out int parsedValue))
    {
      return parsedValue > 0 && parsedValue <= maxAllowedValue;
    }

    return false;
  }

  public static bool IsValidInput(string input, string[] validValues)
  {
    return validValues.Contains(input);
  }
}