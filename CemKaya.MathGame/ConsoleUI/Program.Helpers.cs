partial class Program
{
  private static void PromptMessage(string text, ConsoleColor color, bool addNewLine = false)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = color;
    if (addNewLine)
    {
      WriteLine(text);
    }
    else
    {
      Write(text);
    }
    ForegroundColor = previousColor;
  }

  private static void PromptErrorMessage(string message)
  {
    PromptMessage($"Error > {message}", ConsoleColor.Red, true);
  }

  private static void PromptSuccessMessage(string message)
  {
    PromptMessage($"Success > {message}", ConsoleColor.Green, true);
  }

  private static void PromptHighlightMessage(string message)
  {
    PromptMessage($"{message}", ConsoleColor.Yellow, true);
  }

  private static void PromptQuery(string question)
  {
    PromptMessage($"{question}", ConsoleColor.DarkMagenta);
  }
}