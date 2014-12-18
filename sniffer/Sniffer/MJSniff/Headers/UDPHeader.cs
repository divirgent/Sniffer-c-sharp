using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;

namespace Sniffer
{
    public class UDPHeader
    {
        private ushort usSourcePort;
        public string SourcePort { get { return usSourcePort.ToString(); } }

        private ushort usDestinationPort;
        public string DestinationPort { get { return usDestinationPort.ToString(); } }

        private ushort usLength;
        public string Length { get { return usLength.ToString(); } }

        private short sChecksum;
        public string Checksum { get { return sChecksum.ToString(); } }

        private byte[] byUDPData = new byte[4096];
        public byte[] Data { get { return byUDPData; } }

        public UDPHeader(byte[] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            usLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            Array.Copy(byBuffer, 8, byUDPData, 0, nReceived - 8);
        }
    }
}