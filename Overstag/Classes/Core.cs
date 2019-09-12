using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Net.Mail;

namespace Overstag.Core
{
    public class Credentials
    {
        public string mailUsername { get; set; }
        public string mailPass { get; set; }
        public string mySqlConnectionString { get; set; }
        public string msSqlConnectionString { get; set; }
        public string mollieApiToken { get; set; }
        public string msSqlDebugCString { get; set; }

        public Credentials Get()
        {
            var o =  JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "credentials.json")));
            return o;
        }
    }

    public static class General
    {
        public static Credentials Credentials = new Credentials().Get();

        public static bool DateIsPassed(DateTime check)
            => check < DateTime.Now;
        public static int getAge(DateTime bd)
         => (new DateTime(DateTime.Now.Year, bd.Month, bd.Day) > DateTime.Now ? (DateTime.Now.Year - bd.Year)-1 : (DateTime.Now.Year - bd.Year));
        public static string SendMail(string title, string body, string to)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mijnhostingpartner.nl", 25); //587
                client.UseDefaultCredentials = false;
                if (Credentials.mailPass != string.Empty)
                {
                    client.Credentials = new NetworkCredential(Credentials.mailUsername, Credentials.mailPass);

                    //Code toegevoegd voor overstag mailsender
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = false;

                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(Credentials.mailUsername);
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    client.Send(mail);
                    return "OK";
                }
                else
                {
                    return "ERR: pass is empty";
                }

            }
            catch (Exception e) { throw e; /*return "ERR: " + e.ToString();*/ }
        }

        public static DateTime GetNetworkTime()
        {
            try
            {
                //default Windows time server
                const string ntpServer = "time.windows.com";

                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];

                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;

                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                //NTP uses UDP

                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();
                }

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;

                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);


                if (networkDateTime.Year == DateTime.Now.Year)
                    return networkDateTime.ToLocalTime();
                else
                    return DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
            
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }


}
