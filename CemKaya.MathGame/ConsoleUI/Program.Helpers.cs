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
    Write(text);
    ForegroundColor = previousColor;
  }

  private static void Fail(string message)
  {
    WriteLineInConsole($"Fail > {message}", ConsoleColor.Red);
  }

  private static void Success(string message)
  {
    WriteLineInConsole($"Success > {message}", ConsoleColor.Green);
  }

  private static void Intro(string message)
  {
    WriteLineInConsole($"{message}", ConsoleColor.Yellow);
  }

  private static void PrintUserQuery(string question)
  {
    WriteInConsole($"{question}", ConsoleColor.DarkMagenta);
  }
}