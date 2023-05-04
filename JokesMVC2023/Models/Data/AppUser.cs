namespace JokesMVC2023.Models.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<FavouriteList> FavouriteLists { get; set; }

    }

    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class FavouriteList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<FavouriteListItem> ListItems { get; set; }

    }

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
