using GameLogicLibrary;

partial class Program
{
  private void RunGame()
  {
    // Get username and greet him/her 
    string playerName = GreetUser();

  }
  
  private string GreetUser()
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
}