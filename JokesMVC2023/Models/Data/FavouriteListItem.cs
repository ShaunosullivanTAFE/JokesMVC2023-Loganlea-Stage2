namespace JokesMVC2023.Models.Data
{
    public class FavouriteListItem
    {
        public int Id { get; set; }
        public int FavouriteListId { get; set; }
        public int JokeId { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;


        public FavouriteList FavouriteList { get; set; }
        public Joke Joke { get; set; }

    }
}
