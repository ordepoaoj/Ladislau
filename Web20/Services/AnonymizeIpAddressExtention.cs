using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Web20.Services
{
    public static class AnonymizeIpAddressExtention
    {
        public static string AnonymizeIP(this IPAddress ipAddress)
        {
            string ipAnonymizedString;
            if (ipAddress != null)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    var ipString = ipAddress.ToString();
                    string[] octets = ipString.Split('.');
                    octets[3] = "0";
                    ipAnonymizedString = string.Join(".", octets);
                }
                else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    var ipString = ipAddress.ToString();
                    string[] hextets = ipString.Split(':');
                    var hl = hextets.Length;
                    if (hl > 3) { for (var i = 3; i < hl; i++) { if (hextets[i].Length > 0) { hextets[i] = "0"; } } }
                    ipAnonymizedString = string.Join(":", hextets);
                }
                else { ipAnonymizedString = $"Not Valid - {ipAddress.ToString()}"; }
            }
            else { ipAnonymizedString = "Is Null"; }

            return ipAnonymizedString;
        }
    }
}
