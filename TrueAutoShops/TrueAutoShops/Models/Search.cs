namespace TrueAutoShops.Models
{
    public class Search
    {
        public string Value { get; set; }

        public SearchProperty Zip { get; set; }
    }

    public enum SearchProperty : int
    {
        Name = 0,
        Zip = 1
    }
}