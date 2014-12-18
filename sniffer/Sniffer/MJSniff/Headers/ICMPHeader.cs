using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sniffer
{
    class ICMPHeader
    {
        public short type;
        public byte code;
        public short checksum;

        public ICMPHeader(byte[] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            binaryReader.ReadBytes(20);

            type = (byte)(IPAddress.NetworkToHostOrder(binaryReader.ReadByte()) >> 8);

            code = (byte)(IPAddress.NetworkToHostOrder(binaryReader.ReadByte()) >> 8);

            checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
        }


    }
}
