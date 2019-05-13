namespace Overstag.Models
{
    public class Billing
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int Amount { get; set; }
        public string Timestamp { get; set; }
        //List met avonden? of in view genereren?
        public int[] EventIDs { get; set; }
        public bool Payed { get; set; }

    }
}
