partial class Program
{
  private static void WriteLineInConsole(string text, ConsoleColor color)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = color;
    WriteLine(text);
    ForegroundColor = previousColor;
  }
  
  private static void WriteInConsole(string text, ConsoleColor color)
  {
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = color;
    WriteLine(text);
    ForegroundColor = previousColor;
  }

  private void Fail(string message)
  {
    WriteLineInConsole($"Fail > {message}", ConsoleColor.Red);
  }

  private void Success(string message)
  {
    WriteLineInConsole($"Success > {message}", ConsoleColor.Green);
  }

  private void Info(string message)
  {
    WriteLineInConsole($"{message}", ConsoleColor.Cyan);
  }

  private void Ask(string question)
  {
    WriteInConsole($"{question}", ConsoleColor.DarkYellow);
  }
}