using PropertyChanged;

namespace TrueAutoShops.Models
{
    [ImplementPropertyChanged]
    public class ShopAmenities
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}