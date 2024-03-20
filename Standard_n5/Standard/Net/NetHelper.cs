using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maqdel.Infra.Net
{
    public class NetHelper
    {
        static public string GetHostname()
        {
            string Answer;
            try
            {
                Answer = System.Net.Dns.GetHostName();
            }
            catch
            {
                Answer = "";
            }
            return Answer;
        }

        static public string GetHostIP()
        {
            string Answer;
            try
            {
                Answer = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString();
            }
            catch
            {
                Answer = "";
            }
            return Answer;
        }

        static public string GetHostIP(string HostName)
        {
            string Answer;
            try
            {
                Answer = System.Net.Dns.GetHostAddresses(HostName).GetValue(0).ToString();
            }
            catch
            {
                Answer = "";
            }
            return Answer;
        }
    }
}
