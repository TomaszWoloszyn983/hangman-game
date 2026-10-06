class MovieTitles{

    private static List<String> movieTitlesList = new List<String>(){
            "The Shawshank Redemption",
            "The Godfather",
            "Blade Runner",
            "Pulp Fiction",
            "Forrest Gump",
            "Inception",
            "Fight Club",
            "The Matrix",
            "Goodfellas",
            "Braveheart",
            "Avengers: Endgame",
            "Requiem for a Dream",
            "Bourne Identity"
    };

    public void DisplayMovieTitles(){
        Console.WriteLine("Available Movie Titles:");
        foreach (var title in movieTitlesList){
            Console.WriteLine(title);
        }
    }

    /**
        Returns a random movie title from the list 
        and removes it from the list to avoid repetition.
    */
    public String GetRandomTitle(){
        Random random = new Random();
        String randomTitle = movieTitlesList[random.Next(movieTitlesList.Count)];
        movieTitlesList.Remove(randomTitle);
        return randomTitle;
    }

    public int GetListCount(){
        return movieTitlesList.Count;
    }
}