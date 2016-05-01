using System;

namespace TrueAutoShops.Models.Response
{
    public class CreateProfileResponse
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Level { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
