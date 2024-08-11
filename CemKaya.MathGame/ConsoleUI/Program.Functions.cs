using GameLogicLibrary;
using GameLogicLibrary.Enums;

partial class Program
{
  private static GameManager? _gameManager;
  
  private static void RunGame()
  {
    // Get username and greet the user
    string playerName = GreetUser();
    _gameManager = new GameManager(playerName);
    while (true)
    {
      // Display the main menu and get the selected option
      int selectedOption = DisplayMainMenu();

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
          PromptHighlightMessage(Farewell);
          return;
        default:
          PromptErrorMessage("Invalid selection.");
          break;
      }

      WriteLine("Press any key to continue..." + "\n");
      ReadKey(true);
    }
  }

  private static string GreetUser()
  {
    while (true)
    {
      PromptQuery(UserNameQuestion);
      string userName = ReadLine()!;
      if (InputValidator.IsOnlyLetters(userName))
      {
        PromptHighlightMessage($"""
                                *** Welcome, {userName}! ***
                                You've just joined the Math Game App. Let's have some fun and put your math skills to the test!
                                """ + "\n");
        return userName;
      }

      PromptErrorMessage(InvalidUserName);
    }
  }

  private static int DisplayMainMenu()
  {
    // Define menu options
    string[] mainMenuOptions = new[]
    {
      "Start New Game",
      "View Game History",
      "View Highest Score",
      "Exit"
    };

    int optionsLength = mainMenuOptions.Length;

    // Display menu options to user
    PromptHighlightMessage(MenuIntroduction);
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
        return int.Parse(selectedOption);
      }
      else
      {
        PromptErrorMessage(InvalidMenuOption);
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
      WriteLine(_gameManager?.StartRoundWithQuestion());
      GetUserAnswerToQuestion();
    } while (AskForAnotherRound());
  }

  private static void GetUserAnswerToQuestion()
  {
    PromptQuery(MathQuestion);
    while (true)
    {
      string userInput = ReadLine()!;
      if (InputValidator.IsOnlyDigit(userInput))
      {
        int enteredAnswer = int.Parse(userInput);
        string? result = _gameManager?.EndRound(enteredAnswer);
        WriteLine(result);
        break;
      }

      PromptErrorMessage(InvalidQuestionAnswer);
    }
  }

  private static void SetDifficultyLevel()
  {
    // Get all difficulty levels as an array of enum values
    DifficultyLevel[] difficultyOptions = Enum.GetValues<DifficultyLevel>();
    DifficultyLevel chosenDifficulty = PromptUserForEnumOption(difficultyOptions);
    if (_gameManager != null)
    {
      _gameManager.CurrentDifficulty = chosenDifficulty;
    }
  }

  private static void SetMathOperation()
  {
    // Get all math operations as an array of enum values
    MathOperation[] mathOperations = Enum.GetValues<MathOperation>();
    MathOperation chosenOperation = PromptUserForEnumOption(mathOperations);
    if (_gameManager != null)
    {
      _gameManager.CurrentOperation = chosenOperation;
    }
  }
  
  private static TEnum PromptUserForEnumOption<TEnum>(TEnum[] enumOptions)
    where TEnum : struct, Enum
  {
    Clear();
    PromptHighlightMessage(MenuIntroduction);

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
        // PromptSuccessMessage(ValidSelection);
        return chosenOption;
      }
      else
      {
        PromptErrorMessage(InvalidMenuOption);
      }
    }
  }

  private static void DisplayGameHistory()
  {
    IReadOnlyList<GameRound>? games = _gameManager?.GetGameHistory();
    if (games != null && games.Any())
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

      PromptHighlightMessage($"The total score: {_gameManager?.GetTotalScore()}");
    }
    else
    {
      PromptErrorMessage(NoAnyGame);
    }
  }

  private static void DisplayHighestScore()
  {
      (int highestScore, bool isSuccess) = _gameManager?.GetHighestScore() ?? (0, false);

      if (isSuccess)
      {
        PromptHighlightMessage($"Your highest core: {highestScore}");
      }
      else
      {
        PromptErrorMessage(NoAnyGame);
      }
  }

  private static bool AskForAnotherRound()
  {
    PromptQuery(AnotherRound);
    while (true)
    {
      string userInput = ReadLine()!.Trim().ToLower();
      if (InputValidator.IsValidInput(userInput, new[] { "y", "n" }))
      {
        return userInput == "y";
      }

      PromptErrorMessage(InvalidContinueInput);
    }
  }
}