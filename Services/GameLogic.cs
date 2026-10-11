using System.Runtime.InteropServices;

class GameLogic{
    
    /**
    * The title of the movie to guess.
    * This variable is used to store the movie title
    * that the player needs to guess during the game.
    * It will be assigned a random movie title from the MovieTitles class
    * when a new game is started.
    */
    static string titleToGuess;

    public void InitiateGame(){
        Console.Clear();
        Console.WriteLine("\tHello!\nWelcome to the Hangman Game!\n\nPlease enter your name: ");

// ---------- Get the Player name from the user----------------
        string playerName = null;

        // Loop until a valid name is entered
        while (playerName == null || playerName.Length == 0){
            playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Please enter a valid name.");
                playerName = null;
            }
        }

        Console.Clear();
        Console.WriteLine($"Nice to meet you, {playerName}!"
                            +"\nAre You ready to play? (Y/N)");
        
        string startGame = GetUserInput();
        if(startGame == null || startGame.StartsWith("y")){
            Console.WriteLine("Great! Let's get started!");
            Thread.Sleep(1000);
            Console.Clear();

            Player player = AssignPlayer(playerName);
            StartGame(player);
        } else {
            Console.WriteLine("No worries!\n\nGoodbye!");
        }
    }

    /**
    * Assigns a player with a name and the first movie title.
    * @return The player instance.
    */
    public Player AssignPlayer(String playerName){
        Player player = new Player(playerName);
        return player;
    }



    /**
    * Starts a single game for the given player.
    * for a single movie title.
    */
    public void StartGame(Player player){
        titleToGuess = new MovieTitles().GetRandomTitle();
        List<char> guessedLetters = new List<char>();
        int wrongGuesses = 0;
        // String movieTitle = player.MovieTitle;
        Char[] displayTitle = prepareMovieTitleForDisplay(titleToGuess);
        Char letter;


        Console.Clear();
        do{
            Console.WriteLine($"Your movie title is: {new string(displayTitle)}"
                                +"\nEnter a letter: ");
            letter = Console.ReadKey().KeyChar;
            
            // Check if the input is a valid letter
            if (!Char.IsLetter(letter)){
                Console.Clear();
                Console.WriteLine("\nPlease enter a valid letter.");
                continue;
            }
  
            if (titleToGuess.ToUpper().Contains(letter.ToString().ToUpper())){
                guessedLetters.Add(letter);
                for (int i = 0; i < titleToGuess.Length; i++){
                    if (titleToGuess[i].ToString().ToUpper() == letter.ToString().ToUpper()){
                        displayTitle[i] = titleToGuess[i];
                    }
                }
            }else{
                Console.Clear();
                wrongGuesses++;
                Console.WriteLine($"Your movie title is: {new string(displayTitle)}");
                Console.WriteLine($"\nWrong guess! You have {6 - wrongGuesses} guesses left.");
                Thread.Sleep(1500);
            }
            Console.Clear();

            /*
            If the palyer has won the game, the nextTitle() method will be called 
            to ask the user if they want to keep playing the game,
            and go to the next title.
            */
            if(isWinner(displayTitle)){
                player.UpdateScore(1);
                player.GuessedTitles.Add(titleToGuess);
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("\tCongratulations!"
                                    +"\n\n\t  Your Won!"
                                    +"\n  You guessed the movie title:"
                                    +$"\n\t {new string(titleToGuess)}"
                                    +$"\n\n\tYour Score: {player.Score}");
                bool wannaKeepPlaying = nextTitle();
                if (wannaKeepPlaying){
                    StartGame(player);
                } else {
                    Console.Clear();
                    Console.WriteLine("\n\nThanks for playing!"
                                        +$"\nYour final score is: {player.Score}"
                                        +$"\nYou guessed the following titles: {string.Join(", ", player.GuessedTitles)}"
                                        +"\n\nGoodbye!");
                    break;
                }
            }
        }while (!isWinner(displayTitle) && !isLooser(wrongGuesses));
    }

    /**
    * Prepares the movie title for display by replacing letters
    * with underscores.   
    */
    public char[] prepareMovieTitleForDisplay(String movieTitle){
        char[] displayTitle = new char[movieTitle.Length];
        titleToGuess = movieTitle;
        for (int i = 0; i < movieTitle.Length; i++){
            if (Char.IsLetter(movieTitle[i])){
                displayTitle[i] = '_';
            }else {
                displayTitle[i] = movieTitle[i];
            }
        }
        return displayTitle;
    }

    /**
    * Aks the user if they want to keep playing the game, 
    * and go to the next title. 
    * If the user enters 'Y' or 'y', the game will continue,
    * otherwise it will end.
    */
    public bool nextTitle(){
        Console.WriteLine("\n\nDo you want to keep playing? (Y/N)");
        string startGame = GetUserInput();
        if(startGame == null || startGame == "y"){
            return true;
        } else if (startGame == "n"){
            return false;
        }
        return false;
    }

    /**
    * Checks if the player has won the game.
    * If displayed title contains '_' the player has not won yet.
    */
    public bool isWinner(Char[] displayTitle){
        if (new string(displayTitle).Contains('_'))
            return false;
        else 
            return true;
    }

    /**
    * Checks if the player has lost the game.
    * If the player has made 6 wrong guesses, the player has lost the game.
    */
    public bool isLooser(int wrongGuesses){
        if (wrongGuesses >= 6){
            Console.WriteLine("\tGame Over!"
                            +"\n\tYour Lost!"
                            +$"\nThe movie title was: {titleToGuess}");
            return true;
        } else {
            return false;
        }
    }

    public string GetUserInput(){
        // Console.WriteLine("\n\nDo you want to keep playing? (y/n)");
        string input = Console.ReadLine().ToLower();
        while(input == null || input != "y" && input != "n" || input.Length != 1){
            Console.Clear();
            Console.WriteLine("Please enter a valid letter. (y/n)");
            input = Console.ReadLine().ToLower();
        }
        return input;
    }
        
}