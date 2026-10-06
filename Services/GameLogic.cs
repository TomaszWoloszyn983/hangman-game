class GameLogic
{
    public bool InitiateGame(){

        Console.Clear();
        Console.WriteLine("\tHello!\nWelcome to the Hangman Game!\n\nPlease enter your name: ");

// ---------- Assign Player ----------------
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

        // Create a new Player object with the entered name
        Player player = new Player(playerName);
        Console.WriteLine($"Nice to meet you, {player.Name}!"
                            +"\nAre You ready to play? (Y/N)");
        string startGame = Console.ReadLine().ToLower();

        if(startGame == null || startGame.StartsWith("y")){
            Console.WriteLine("Great! Let's get started!");
            return true;
        } else {
            Console.WriteLine("No worries! Come back when you're ready.");
            return false;
        }
    }

    public void StartGame(){
        player.MovieTitle = new MovieTitles().GetRandomTitle();
        Console.WriteLine(player.MovieTitle);
        
}