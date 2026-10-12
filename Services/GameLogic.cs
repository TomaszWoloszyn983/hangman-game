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
    MovieTitles MovieTitles;

    public void StartGame(){
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
            MovieTitles = new MovieTitles();
            StartRound(player);
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
    * Starts a single game for the given player
    * for a single movie title.
    */
    public void StartRound(Player player){
        titleToGuess = MovieTitles.GetRandomTitle();
        List<char> guessedLetters = new List<char>();
        int wrongGuesses = 0;
        player.IncrementTotalGames();
        Char[] displayTitle = prepareMovieTitleForDisplay(titleToGuess);
        Char letter;


        Console.Clear();
        do{
            Console.Clear();
            Console.WriteLine($"Your movie title is: {new string(displayTitle)}"
                                +"\nEnter a letter: ");
            letter = Console.ReadKey().KeyChar;

            // Check if the input is a valid letter
            if (!Char.IsLetter(letter)){
                Console.Clear();
                Console.WriteLine("\nPlease enter a valid letter.");
                Thread.Sleep(1000);
                continue;
            }else if (guessedLetters.Contains(letter)){
                Console.Clear();
                Console.WriteLine("You have already typed that letter. Select another.");
                Thread.Sleep(1000);
                continue;
            }
            guessedLetters.Add(letter);
  
            if (titleToGuess.ToUpper().Contains(letter.ToString().ToUpper())){
                // guessedLetters.Add(letter);
                for (int i = 0; i < titleToGuess.Length; i++){
                    if (titleToGuess[i].ToString().ToUpper() == letter.ToString().ToUpper()){
                        displayTitle[i] = titleToGuess[i];
                    }
                }
            }else{
                Console.Clear();
                wrongGuesses++;
                Console.WriteLine($"Your movie title is: {new string(displayTitle)}");
                DisplayHangman(wrongGuesses);
                Console.WriteLine($"\nWrong guess! You have {6 - wrongGuesses} guesses left.");
                Thread.Sleep(2000);
            }
            

            /*
            If the palyer has won the game, the nextTitle() method will be called 
            to ask the user if they want to keep playing the game,
            and go to the next title.
            */
            if(isWinner(displayTitle)){
                Thread.Sleep(500);
                player.UpdateScore(1);
                player.GuessedTitles.Add(titleToGuess);
                Console.Clear();
                Console.WriteLine("\tCongratulations!"
                                    +"\n\n\t  Your Won!"
                                    +"\n  You guessed the movie title:"
                                    +$"\n\t {new string(titleToGuess)}"
                                    +$"\n\n\tYour Score: {player.Score}");
                
                if(MovieTitles.GetFilmCount() > 0 && NextTitle()){
                    StartRound(player);
                }else if(MovieTitles.GetFilmCount() == 0 ){
                    Console.Clear();
                    Console.WriteLine("\nThere are no more movie titles to guess!"+
                                        "\nCongratulation! You finished the game.");
                    Thread.Sleep(2000);
                    DisplayStatistics(player);
                    break;
                }else {
                    Thread.Sleep(1000);
                    DisplayStatistics(player);
                    break;
                }
            }

            /**
            If the player has lost the game, the nextTitle() method will be called 
            to ask the user if they want to keep playing the game,  
            */
            if(isLooser(wrongGuesses)){
                Thread.Sleep(600);
                // Console.Clear();
                Console.WriteLine("\n\n\t  You Lost!"
                                    +"\n    The movie title was:"
                                    +$"\n\t {new string(titleToGuess)}"
                                    +$"\n\n\tYour Score: {player.Score}");

                if(MovieTitles.GetFilmCount() > 0 && NextTitle()){
                    StartRound(player);
                }else if(MovieTitles.GetFilmCount() == 0 ){
                    Console.Clear();
                    Console.WriteLine("\nThere are no more movie titles to guess!"+
                                        "\nCongratulation! You finished the game.");
                    Thread.Sleep(3000);
                    DisplayStatistics(player);
                    break;
                }else {
                    Thread.Sleep(1000);
                    DisplayStatistics(player);
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
    public bool NextTitle(){
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
            return true;
        } else {
            return false;
        }
    }

    /**
    * Gets the user input and validates it.
    */
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
        
    /**
    * Displays the final statistics of the player after the game ends.
    */
    public void DisplayStatistics(Player player){
        Console.Clear();
        Console.WriteLine($"\n\nThanks for playing {player.Name}!"
                            +$"\nYour final score is: {player.Score}"
                            +$"\nYou played a total of {player.TotalGames} games."
                            +$"\nYou won {player.Score} games."
                            +$"\nYou lost {player.TotalGames - player.Score} games."
                            +$"\nYou guessed the following titles: {string.Join(",  \n", player.GuessedTitles)}"
                            +"\n\n  Goodbye!\n\n");
    }

    /**
    * Displays the hangman figure based on the number of wrong guesses. 
    */
    public void DisplayHangman(int wrongGuesses){
       
        //     +---+
        //     |   |
        //     O   |
        //    /|\  |
        //    / \  |
        //   ______|

        String[] HangmanParts = new String[]{" +---+", " |   |", " O   |", "/|\\  |", "/ \\  |", "_____|"};
        String hangmanDisplay = "";
    
        for (int i = 0; i < wrongGuesses; i++){
            Console.Clear();
            hangmanDisplay += "\t   "+HangmanParts[i]+"\n";
            Console.WriteLine(hangmanDisplay);
            Thread.Sleep(200);
        }
    }
}