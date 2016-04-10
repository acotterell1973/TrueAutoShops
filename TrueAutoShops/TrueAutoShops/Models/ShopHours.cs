using PropertyChanged;

namespace TrueAutoShops.Models
{
    [ImplementPropertyChanged]

    public class ShopHours
    {
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string HoursFormatted { get; set; }
        public string DayFormatted { get; set; }
        public bool IsToday { get; set; }
        public bool IsClosed { get; set; }
    }
}