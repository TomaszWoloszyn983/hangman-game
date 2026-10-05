class GameLogic
{
    public void StartGame(){
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
        Player player = new Player(playerName);
        Console.WriteLine($"Nice to meet you, {player.Name}!");

        
    }
}