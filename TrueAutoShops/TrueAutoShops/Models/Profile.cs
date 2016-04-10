using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueAutoShops.Models
{
    public class Profile: BaseNotificationHandler
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Boolean ReceiveSms { get; set; }

        public string MakeName { get; set; }
        public string MakeLogo { get; set; }
        public string ModelName { get; set; }
        public string ModelImage { get; set; }
    }
}
