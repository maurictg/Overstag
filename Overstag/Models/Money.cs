using System;
using System.Collections.Generic;
using Overstag.Models;

namespace Overstag.Accountancy
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int? Type { get; set; }
        public DateTime When { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }

        public bool Payed { get; set; }
        public string Metadata { get; set; } //JSON data

        public int UserId { get; set; }
        public Account User { get; set; }
    }

    public static class Transactions
    {
        public static Dictionary<int, string> Types = new Dictionary<int, string>()
        {
            //Inkomsten/Income
            {1, "Activiteiten"}, //Activities
            {2, "Subsidie"}, //subsidy
            {3, "Sponsor (bedrijf)"}, //company
            {4, "Sponsor (particulier)"}, //self-employed/private
            {5, "Sponsor (kerk)"}, //church
            {6, "Overige"}, //Other

            //Uitgaven/Expenses
            {21, "Boodschappen"}, //Shopping
            {22, "Huur gebouw"}, //Buiding rental
            {23, "Website"}, //Webhosting
            {24, "Activiteiten"}, //Activities
            {25, "Drukwerk"}, //Printed matter
            {26, "Inboedel"}, //Furniture
            {27, "Reiskosten"}, //Travel expresses
            {28, "Overige" }, //Other
            {29, "Declaratie" } //Declarations
        };
    }
}
