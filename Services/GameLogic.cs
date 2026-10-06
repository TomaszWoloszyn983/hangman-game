class GameLogic{
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

        Console.WriteLine($"Nice to meet you, {playerName}!"
                            +"\nAre You ready to play? (Y/N)");
        string startGame = Console.ReadLine().ToLower();

        if(startGame == null || startGame.StartsWith("y")){
            Console.WriteLine("Great! Let's get started!");
            StartGame(playerName);
        } else {
            Console.WriteLine("No worries! Come back when you're ready.");
        }
    }

    public void StartGame(String playerName){

        Player player = new Player(playerName);
        player.MovieTitle = new MovieTitles().GetRandomTitle();
        Console.WriteLine(player.MovieTitle);
    }
        
}