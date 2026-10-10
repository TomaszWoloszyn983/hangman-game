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
        
        string startGame = Console.ReadLine().ToLower();
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
        // player.MovieTitle = titleToGuess; // No need to assign the movie title here, as it is already assigned in the Player constructor.
        // Console.WriteLine(player.MovieTitle);
        return player;
    }

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

    public void StartGame(Player player){
        titleToGuess = new MovieTitles().GetRandomTitle();
        List<char> guessedLetters = new List<char>();
        int wrongGuesses = 0;
        // String movieTitle = player.MovieTitle;
        Char[] displayTitle = prepareMovieTitleForDisplay(titleToGuess);
        Char letter;
        bool keepPlaying = true;


        Console.Clear();
        do{
            Console.WriteLine($"Your movie title is: {new string(displayTitle)}");
            Console.WriteLine("\nEnter a letter: ");
            letter = Console.ReadKey().KeyChar;
            
            // Check if the input is a valid letter
            if (!Char.IsLetter(letter)){
                Console.WriteLine("\nPlease enter a valid letter.");
                continue;
            }
  
            if (titleToGuess.Contains(letter)){
                guessedLetters.Add(letter);
                for (int i = 0; i < titleToGuess.Length; i++){
                    if (titleToGuess[i] == letter){
                        displayTitle[i] = letter;
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
            keepPlaying = !isWinner(displayTitle) && !isLooser(wrongGuesses);

        }while (keepPlaying);
    }

    /**
    * Checks if the player has won the game.
    * If displayed title contains '_' the player has not won yet.
    */
    public bool isWinner(Char[] displayTitle){
        if (new string(displayTitle).Contains('_')){
            return false;
        } else {
            Console.WriteLine("\tCongratulations!");
            Console.WriteLine($"\n\tYour Won!\nYou guessed the movie title:\n\t {new string(displayTitle)}");
            return true;
        }
    }

    /**
    * Checks if the player has lost the game.
    * If the player has made 6 wrong guesses, the player has lost the game.
    */
    public bool isLooser(int wrongGuesses){
        if (wrongGuesses >= 6){
            Console.WriteLine("\tGame Over!");
            Console.WriteLine($"\n\tYour Lost!");
            Console.WriteLine($"\nThe movie title was: {titleToGuess}");
            return true;
        } else {
            return false;
        }
    }
        
}