using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

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

        public void HandleMessage(byte[] _data)
        {
            CommonData data = JsonSerializer.Deserialize<CommonData>(Encoding.UTF8.GetString(_data));

            switch (data.t)
            {
                case "createPool": //Fill pool with data
                    this.Data = data.data;
                    break;

                default:
                    break;
            }
        }
    }

    public class CommonData
    {
        public string t { get; set; }
        public object data { get; set; }
    }

    public class LasergameData
    {
        public int round { get; set; }
        public int roundLength { get; set; }
        public Dictionary<string, int> scores { get; set; }

        public LasergameData()
        {
            this.round = 0;
            this.roundLength = 300;
            this.scores = new Dictionary<string, int>();
        }
    }
}
