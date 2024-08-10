using GameLogicLibrary;

partial class Program
{
  private static void RunGame()
  {
    // Get username and greet him/her 
    string playerName = GreetUser();
    
    // Display the main menu
    int selectedOption = DisplayMainMenu();

  }
  
  private static string GreetUser()
  {
    while (true)
    {
      Ask(UserNameQuestion);
      string userName = ReadLine()!;
      if (InputValidator.IsOnlyLetters(userName))
      {
        Info($"*** Welcome {userName} to Math Game App ***");
        return userName;
      }
      
      Fail(InvalidUserName);
    }
  }
  
  private static int DisplayMainMenu()
  {
    string[] mainMenuOptions = new[]
    {
      "Start New Game",
      "View Game History",
      "View Highest Score",
      "Exit"
    };

    int optionsLength = mainMenuOptions.Length;
    
    // List off each menu option
    Info(MenuIntroduction);
    for (int i = 0; i < optionsLength; i++)
    {
      var option = mainMenuOptions[i];
      WriteLine($"{i + 1} for '{option}'");
    }
    
    // Get the valid main menu option
    string selectedOption = ReadLine()!;
    
    while (true)
    {
      if (InputValidator.IsInputValidDigit(selectedOption, optionsLength))
      {
        Success(ValidMenuOption);
        return int.Parse(selectedOption);
      }
      else
      {
        Fail(InvalidMenuOption);
        selectedOption = ReadLine()!;
      }
    }
  }
}