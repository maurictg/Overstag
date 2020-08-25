namespace Overstag.Models.Database.Meta
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Residence { get; set; }

        public UserData() { }
        public UserData(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            PostalCode = user.PostalCode;
            Residence = user.Residence;
        }
    }
}
