using GameLogicLibrary;
using GameLogicLibrary.Enums;

partial class Program
{
  private static GameManager _gameManager;

  private static void RunGame()
  {
    // Get username and greet the user
    string playerName = GreetUser();
    _gameManager = new GameManager(playerName);
    while (true)
    {
      // Display the main menu and get the selected option
      int selectedOption;
      selectedOption = DisplayMainMenu();

      WriteLine($"Debug: Selected Option = {selectedOption}"); // Debugging line

      switch (selectedOption)
      {
        case 1:
          StartNewGame();
          break;
        case 2:
          DisplayGameHistory();
          break;
        case 3:
          DisplayHighestScore();
          break;
        case 4:
          Intro(Farewell);
          return;
        default:
          Fail("Invalid selection.");
          break;
      }
      
      WriteLine("Press any key to continue...");
      ReadKey(true);
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
      WriteLine(_gameManager.StartRoundWithQuestion());
      GetUserAnswerToQuestion();
    } while (AskForAnotherRound());
  }

  private static void GetUserAnswerToQuestion()
  {
    PrintUserQuery(MathQuestion);
    while (true)
    {
      string userInput = ReadLine()!;
      if (InputValidator.IsOnlyDigit(userInput))
      {
        Success(ValidQuestionAnswer);
        int enteredAnswer = int.Parse(userInput);
        string result = _gameManager.EndRound(enteredAnswer);
        WriteLine(result);
        break;
      }
      
      Fail(InvalidQuestionAnswer);
    }
  }

  private static void SetDifficultyLevel()
  {
    // Get all difficulty levels as an array of enum values
    DifficultyLevel[] difficultyOptions = Enum.GetValues<DifficultyLevel>();
    DifficultyLevel chosenDifficulty = GetEnumOptionFromUser(difficultyOptions);
    _gameManager.CurrentDifficulty = chosenDifficulty;
  }

  private static void SetMathOperation()
  {
    // Get all math operations as an array of enum values
    MathOperation[] mathOperations = Enum.GetValues<MathOperation>();
    MathOperation chosenOperation = GetEnumOptionFromUser(mathOperations);
    _gameManager.CurrentOperation = chosenOperation;
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

  private static void DisplayGameHistory()
  {
    var games = _gameManager.GetGameHistory();
    if (games.Any())
    {
      for (int i = 0; i < games.Count; i++)
      {
        GameRound game = games[i];
        WriteLine($"{i + 1}. Round");
        WriteLine($"  {"Selected operation",-20}: {game.SelectedOperation}");
        WriteLine($"  {"The first operand",-20}: {game.FirstOperand}");
        WriteLine($"  {"The second operand",-20}: {game.SecondOperand}");
        WriteLine($"  {"Correct answer",-20}: {game.CorrectAnswer}");
        WriteLine($"  {"Your answer",-20}: {game.UserAnswer}");
        WriteLine($"  {"Result",-20}: {game.IsCorrect}");
        WriteLine($"  {"Taken time",-20}: {game.TimeTaken.TotalSeconds:F} secs");
        WriteLine($"  {"Score",-20}: {game.CalculateScore()}");
        WriteLine();
      }
      
      WriteLineInConsole($"The total score: {_gameManager.GetTotalScore()}", ConsoleColor.Magenta);
    }
    else
    {
      WriteLineInConsole(NoAnyGame, ConsoleColor.DarkRed);
    }
  }

  private static void DisplayHighestScore()
  {
    (int highestScore, bool isSuccess) = _gameManager.GetHighestScore();

    if (isSuccess)
    {
      WriteLineInConsole($"Your highest core: {highestScore}", ConsoleColor.Magenta);
    }
    else
    {
      WriteLineInConsole(NoAnyGame, ConsoleColor.DarkRed);
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

      Fail(InvalidContinueInput);
    }
  }
}