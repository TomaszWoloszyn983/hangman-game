public class Player
{
    public string Name { get; private set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

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
}