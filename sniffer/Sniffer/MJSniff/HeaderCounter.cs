using System;
using System.Collections.Generic;
using System.Text;

namespace Sniffer
{
    class HeaderCounter
    {
        static public long tcp = 0;
        static public long udp = 0;
        static public long dns = 0;
        static public long icmp = 0;
        static public long unknown = 0;

        static public String getString()
        {
            return "tcp: " + tcp + " " +
                "udp: " + udp + " " +
                "dns: " + dns + " " +
                "icmp: " + icmp + " " +
                "unknown: " + unknown + " ";
        }
    }
}
