namespace JokesMVC2023.Models.Data
{
    public class FavouriteList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<FavouriteListItem> ListItems { get; set; }

    }
}
