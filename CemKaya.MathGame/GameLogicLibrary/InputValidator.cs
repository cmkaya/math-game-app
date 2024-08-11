namespace GameLogicLibrary;

public class InputValidator
{
  public static bool IsOnlyLetters(string input)
  {
    return (string.IsNullOrEmpty(input) == false) && input.All(char.IsLetter);
  }

  public static bool IsOnlyDigit(string input)
  {
    if (string.IsNullOrEmpty(input))
    {
      return false;
    }
    // Check if the first character is a negative sign
    if (input.Length > 1 && input[0] == '-')
    {
      return input.Substring(1).All(char.IsDigit);
    }
    
    return input.All(char.IsDigit);
  }
  
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