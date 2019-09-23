using System;

namespace Overstag.API
{
    public enum Status { success, warning, error };

    public class HttpObject
    {
        public Status status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public class Request
    {
        public string token { get; set; }
    }

    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Activity
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime when { get; set; }
        public string description { get; set; }
        public int cost { get; set; }
        public bool subscribed { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string token { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }


}
