namespace JokesMVC2023.Models.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<FavouriteList> FavouriteLists { get; set; }

    }
}
