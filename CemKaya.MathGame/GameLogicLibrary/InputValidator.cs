namespace GameLogicLibrary;

public class InputValidator
{
  public static bool IsOnlyLetters(string text)
  {
    return (string.IsNullOrEmpty(text) == false) && text.All(char.IsLetter);
  }

  public static bool IsInputValidDigit(string text, int maxValue)
  {
    // Try to parse the string into an integer
    if (int.TryParse(text, out int number))
    {
      // Check if the parsed number is within the valid range
      return number < maxValue;
    }

    return false;
  }
}