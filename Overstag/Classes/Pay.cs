using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using Newtonsoft.Json;
using System.Threading.Tasks;
//using Mollie.Api;
//using Mollie.Api.Client;
//using Mollie.Api.Models;
//using Mollie.Api.Framework;
using Overstag.Models;
using Overstag.Models.NoDB;
//using Mollie.Api.Client.Abstract;
//using Mollie.Api.Models.Payment.Request;
//using Mollie.Api.Models.Payment.Response;

namespace Overstag.Payment
{
    public class OPay
    {
        private static readonly string _apiKey = "test_DWJGrH8tpkxVSuKpnBffCwq35x2GGg";
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> Test()
        {

            var formdata = new Dictionary<string, string>
            {
                { "amount[currency]", "EUR" },
                { "amount[value]", "10.00" },
                { "description","Test mollie" }
            };

            var content = new FormUrlEncodedContent(formdata);
            content.Headers.Add("Authorization",_apiKey);

            var response = await client.PostAsync("https://api.mollie.com/v2/payments",content);

            return JsonConvert.SerializeObject(response);
        }
    }
}
