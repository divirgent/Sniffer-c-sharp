using System;
using System.Collections.Generic;
using System.Text;

namespace Sniffer
{
    class Filter
    {
        static public bool tcp = true;
        static public bool udp = true;
        static public bool dns = true;
        static public bool icmp = false;
        static public bool unknown = false;
    }
}
