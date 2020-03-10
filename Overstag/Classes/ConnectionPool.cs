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

        public ConnectionPool(byte type)
        {
            this.Type = type;
        }

        public (ConnectionPool, object) HandleMessage(byte[] _data, bool authenticated)
        {
            object returnData = null; //If returndata != null then this data will be broadcasted
            string json = Encoding.UTF8.GetString(_data);
            CommonData data = JsonSerializer.Deserialize<CommonData>(json);

            try
            {


                switch (data.t)
                {
                    case "createData": //Fill pool with data
                        this.Data = data.data;
                        break;

                    //Authed actions to all
                    case "broadcast": //Broadcast message
                        {
                            if (authenticated)
                                returnData = new { t = "alert", data = data.data };
                        }
                        break;
                    //Section lasergame
                    case "lg_nextRound": //JSON: {t: 'lg_nextRound', data: '0'}
                        {
                            if (authenticated)
                            {
                                if (this.Data == null)
                                    this.Data = new LG_Data();

                                if (this.Data.GetType() == typeof(LG_Data))
                                {
                                    var l = (LG_Data)this.Data;
                                    l.round = Convert.ToInt32(data.data);

                                    returnData = new { t = "nextRound", data = data.data };
                                }
                            }

                        }
                        break;
                    case "lg_reset": //JSON: {t: 'lg_reset'}
                        {
                            if (authenticated)
                                returnData = new { t = "reset" };
                        }
                        break;
                    case "lg_insert": //JSON {t: 'lg_insert', data: json... }
                        {
                            try
                            {
                                string[] d = data.data.Split(',');
                                if (d.Count() == 3)
                                    if (authenticated)
                                    {
                                        this.Data = new LG_Data() { round = Convert.ToInt32(d[0]), roundLength = Convert.ToInt32(d[1]), allowAudio = (d[2] == "true") };
                                        return (this, new { t = "fillData", data = this.Data });
                                    }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("=== ERROR: " + e.Message);
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                        break;
                    case "lg_mute": //JSON {t: 'lg_mute', data: bool (allowed)
                        {
                            if (authenticated)
                            {
                                if (this.Data == null)
                                    this.Data = new LG_Data();

                                if (this.Data.GetType() == typeof(LG_Data))
                                {
                                    var l = (LG_Data)this.Data;
                                    l.allowAudio = (data.data == "true");
                                    this.Data = l;

                                    returnData = new { t = (l.allowAudio) ? "unmute" : "mute" };
                                }
                            }
                        }
                        break;
                    case "lg_playSound": //JSON {t: 'playSound', data: soundname}
                        {
                            if (authenticated)
                                returnData = new { t = "playSound", data = data.data };
                        }
                        break;
                    case "lg_update": //JSON {t: 'lg_update', data: name,score }
                        {
                            try
                            {
                                string[] d = data.data.Split(',');
                                if (d.Length == 2)
                                {
                                    if (authenticated)
                                    {
                                        if (this.Data == null)
                                            this.Data = new LG_Data();

                                        if (this.Data.GetType() == typeof(LG_Data))
                                        {
                                            var l = (LG_Data)this.Data;
                                            if (l.scores.ContainsKey(d[0]))
                                                l.scores[d[0]] = Convert.ToInt32(d[1]);
                                            else
                                                l.scores.Add(d[0], Convert.ToInt32(d[1]));

                                            this.Data = l;

                                            returnData = new { t = "updateScore", data = new { user = d[0], score = d[1] } };
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

            }
            catch(Exception e)
            {
                returnData = new { t = "alert", data = "<span class=\"red-text\">Er is een onbekende fout opgetreden></span>", error = e.Message, stacktrace = e.StackTrace };
            }

            return (this, returnData);
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
        public bool allowAudio { get; set; }
        public Dictionary<string, int> scores { get; set; }
       
        public LG_Data()
        {
            this.round = 0;
            this.roundLength = 300;
            this.allowAudio = true;
            this.scores = new Dictionary<string, int>();
        }
    }
}
