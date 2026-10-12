public class Player
{
    public string Name { get; private set; }
    public int TotalGames { get; set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

    public List<String> GuessedTitles { get; private set;}

    /**
        Assigned it automatically as the object is created.
    */
    public string MovieTitle { get; set; }

    public Player(string name){
        Name = name;
        TotalGames = 0;
        Score = 0;
        IsWon = false;
        GuessedTitles = new List<String>();
    }

    public void UpdateScore(int points){
        Score += points;
    }

    public void IncrementTotalGames(){
        TotalGames++;
    }

    public void ResetScore(){
        Score = 0;
    }

    public String GetPlayerInfo(){
        return $"Player: {Name}, Score: {Score}, IsWon: {IsWon}, Guessed Titles: {string.Join(", ", GuessedTitles)}";
    }

    public void AddGuessedTitle(String title){
        GuessedTitles.Add(title);
    }
}