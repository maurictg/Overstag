using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Overstag.Models;

namespace Overstag.Classes
{
    public class ConnectionPool
    {
        public byte Type { get; set; }
        public object Data { get; set; }
        public ConcurrentDictionary<string, int> auths = new ConcurrentDictionary<string, int>();

        public ConnectionPool(byte type)
        {
            this.Type = type;
        }

        public (ConnectionPool, object) HandleMessage(byte[] _data)
        {
            object returnData = null; //If returndata != null then this data will be broadcasted
            string json = Encoding.UTF8.GetString(_data);
            CommonData data = JsonSerializer.Deserialize<CommonData>(json);

            switch (data.t)
            {
                case "createData": //Fill pool with data
                    this.Data = data.data;
                    break;

               //Section lasergame
                case "lg_nextRound": //JSON: {t: 'lg_nextRound', data: 'TOKEN,0'}
                    {
                        string[] values = data.data.Split(',');
                        if(values.Length == 2)
                            if (Authenticate(values[0]))
                                returnData = new { t = "nextRound", data = values[1] };
                    }
                    break;
                case "lg_reset": //JSON: {t: 'lg_reset', data: 'TOKEN'}
                    {
                        if (Authenticate(data.data))
                            returnData = new { t = "reset" };
                    }
                    break;
                case "lg_insert": //JSON {t: 'lg_insert', data: TOKEN,json... }
                    {
                        try
                        {
                            string[] d = data.data.Split(',');
                            if(d.Count() == 3)
                            {
                                if (Authenticate(d[0]))
                                {
                                    this.Data = new LG_Data() { round = Convert.ToInt32(d[1]), roundLength = Convert.ToInt32(d[2]) };
                                    return (this, new { t = "fillData", this.Data });
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("=== ERROR: " + e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                    break;
                case "lg_update": //JSON {t: 'lg_insert', data: TOKEN,name,score }
                    {
                        try
                        {
                            string[] d = data.data.Split(',');
                            if (d.Length == 3)
                            {
                                if (Authenticate(d[0]))
                                {
                                    if (this.Data == null)
                                        this.Data = new LG_Data();

                                    if (this.Data.GetType() == typeof(LG_Data))
                                    {
                                        var l = (LG_Data)this.Data;
                                        if (l.scores.ContainsKey(d[1]))
                                            l.scores[d[1]] = Convert.ToInt32(d[2]);
                                        else
                                            l.scores.Add(d[1], Convert.ToInt32(d[2]));

                                        this.Data = l;

                                        returnData = new { t = "updateScore", data = new { user = d[1], score = d[2] } };
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    break;
                default:
                    break;
            }

            return (this, returnData);
        }

        private bool Authenticate(string userToken)
        {
            userToken = Uri.UnescapeDataString(userToken);
            if(userToken != null)
            {
                if (auths.ContainsKey(userToken))
                {
                    if (auths[userToken] > 0)
                        return true;
                }
                else
                {
                    var user = new OverstagContext().Accounts.FirstOrDefault(f => f.Token == userToken);
                    if (user != null && user.Type > 0)
                    {
                        auths.TryAdd(user.Token, user.Type);
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class CommonData
    {
        public string t { get; set; }
        public string data { get; set; }
    }

    /*
     *  Lasergame
     */

    public class LG_Data
    {
        public int round { get; set; }
        public int roundLength { get; set; }
        public Dictionary<string, int> scores { get; set; }
       
        public LG_Data()
        {
            this.round = 0;
            this.roundLength = 300;
            this.scores = new Dictionary<string, int>();
        }
    }
}
