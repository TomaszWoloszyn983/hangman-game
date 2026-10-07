public class Player
{
    public string Name { get; private set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

    public List<String> GuessedTitles { get; set;} = new List<String> {};

    /**
        Assigned it automatically as the object is created.
    */
    public string MovieTitle { get; set; }

    public Player(string name)
    {
        Name = name;
        Score = 0;
        this.IsWon = false;
        MovieTitle = null;

    }

    public void UpdateScore(int points)
    {
        Score += points;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public String GetPlayerInfo()
    {
        return $"Player: {Name}, Score: {Score}, IsWon: {IsWon}, Guessed Titles: {string.Join(", ", GuessedTitles)}";
    }
}