using System.Text.RegularExpressions;

namespace GameLogicLibrary;

public class InputValidator
{
  public static bool IsOnlyLetters(string text)
  {
    return (string.IsNullOrEmpty(text) == false) && text.All(char.IsLetter);
  }
}