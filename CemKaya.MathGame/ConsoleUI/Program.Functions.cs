using GameLogicLibrary;
using GameLogicLibrary.Enums;

partial class Program
{
  private static void RunGame()
  {
    // Get username and greet the user
    string playerName = GreetUser();

    // Display the main menu and get the selected option
    int selectedOption = DisplayMainMenu();

    switch (selectedOption)
    {
      case 1:
        StartNewGame();
        break;
    }
  }

  private static string GreetUser()
  {
    while (true)
    {
      PrintUserQuery(UserNameQuestion);
      string userName = ReadLine()!;
      if (InputValidator.IsOnlyLetters(userName))
      {
        WriteInConsole($"""
                        *** Welcome, {userName}! ***
                        You've just joined the Math Game App. Let's have some fun and put your math skills to the test!
                        """, ConsoleColor.DarkCyan);
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
    Intro("\n" + MenuIntroduction);
    for (int i = 0; i < optionsLength; i++)
    {
      var option = mainMenuOptions[i];
      WriteLine($" {i + 1} for '{option}'");
    }

    // Get the valid main menu option
    string selectedOption = ReadLine()!;

    while (true)
    {
      if (InputValidator.IsIntegerInRange(selectedOption, optionsLength))
      {
        Success(ValidSelection);
        return int.Parse(selectedOption);
      }
      else
      {
        Fail(InvalidMenuOption);
        selectedOption = ReadLine()!;
      }
    }
  }

  private static void StartNewGame()
  {
    do
    {
      SetDifficultyLevel();
      SetMathOperation();
    } while (AskForAnotherRound());
    
    Clear();
    Intro(Farewell);
  }

  private static void SetDifficultyLevel()
  {
    // Get all difficulty levels as an array of enum values
    DifficultyLevel[] difficultyOptions = Enum.GetValues<DifficultyLevel>();
    DifficultyLevel chosenDifficulty = GetEnumOptionFromUser(difficultyOptions);
  }

  private static void SetMathOperation()
  {
    // Get all math operations as an array of enum values
    MathOperation[] mathOperations = Enum.GetValues<MathOperation>();
    MathOperation chosenOperation = GetEnumOptionFromUser(mathOperations);
  }

  /// <summary>
  /// Presents a menu of enum options to the user and returns their selection.
  /// </summary>
  /// <typeparam name="TEnum">The type of enum to present options for.</typeparam>
  /// <param name="enumOptions">An array of enum values to present as options.</param>
  /// <returns>The enum value selected by the user.</returns>
  /// <remarks>
  /// This method clears the console, displays a numbered list of options,
  /// and validates user input to ensure a valid selection is made.
  /// </remarks>
  private static TEnum GetEnumOptionFromUser<TEnum>(TEnum[] enumOptions)
    where TEnum : struct, Enum
  {
    Clear();
    Intro(MenuIntroduction);

    int optionCount = enumOptions.Length;

    // Display options to user, numbering from 1 to match user expectations
    for (int i = 0; i < optionCount; i++)
    {
      WriteLine($" {i + 1} for '{enumOptions[i]}'");
    }

    while (true)
    {
      string userInput = ReadLine()!;

      // Validate input: must be a number between 1 and the number of options
      if (InputValidator.IsIntegerInRange(userInput, optionCount))
      {
        // Convert user input to zero-based index
        int selectedIndex = int.Parse(userInput);
        TEnum chosenOption = enumOptions[selectedIndex - 1];
        Success(ValidSelection);
        return chosenOption;
      }
      else
      {
        Fail(InvalidMenuOption);
      }
    }
  }
  
  private static bool AskForAnotherRound()
  {
    PrintUserQuery(AnotherRound);
    while (true)
    {
      string userInput = ReadLine()!.Trim().ToLower();
      if (InputValidator.IsValidInput(userInput, new[] { "y", "n" }))
      {
        return userInput == "y";
      }

      Fail(InvalidInput);
    }
  }
}