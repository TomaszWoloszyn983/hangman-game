class GameLogic
{
    public void StartGame(){
        Console.WriteLine("\tHello!\nWelcome to the Hangman Game!\n\nPlease enter your name: ");


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
        Console.WriteLine($"Nice to meet you, {playerName}!");
        // Game logic implementation goes here
    }
}