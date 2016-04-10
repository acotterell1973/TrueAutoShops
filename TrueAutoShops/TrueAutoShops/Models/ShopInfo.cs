using System;
using System.Collections.Generic;
using PropertyChanged;

namespace TrueAutoShops.Models
{
    public class ShopInfo  : BaseNotificationHandler
    {
        public ShopInfo()
        {
            Amenities = new List<ShopAmenities>();
            Hours = new List<ShopHours>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ContantName { get; set; }
        public string ShopIntro { get; set; }

        public bool InNetwork { get; set; }

        public DateTime? InNetworkDate { get; set; }

        public string ShopImageUri => "/content/images/default-shop.jpg";
        public int Ratings { get; set; }

        public List<ShopAmenities> Amenities { get; set; }
        public List<ShopHours> Hours { get; set; }
    }
}