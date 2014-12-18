using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Sniffer;

namespace Sniffer
{
    class IPlistener
    {
        private Socket mainSocket;
        private byte[] byteData = new byte[4096];
        private bool bContinueCapturing = true;

        private delegate void AddTreeNode(TreeNode node);
        TreeView treeView;

        String ip;
        Label counter;

        public IPlistener(String ip, TreeView treeView, Label counter)
        {
            this.ip = ip;
            this.treeView = treeView;
            this.counter = counter;

            bContinueCapturing = true;

            mainSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Raw, ProtocolType.IP);

            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), 0));

            mainSocket.SetSocketOption(SocketOptionLevel.IP,
                                       SocketOptionName.HeaderIncluded,
                                       true);

            byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
            byte[] byOut = new byte[4] { 1, 0, 0, 0 };

            mainSocket.IOControl(IOControlCode.ReceiveAll, byTrue, byOut);

        }

        public void start()
        {
            mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
        }

        public void OnReceive(IAsyncResult ar)
        {
            try
            {
                int nReceived = mainSocket.EndReceive(ar);

                ParseData(byteData, nReceived);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MJsniffer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            byteData = new byte[4096];

            try
            {
                mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), null);
            }
            catch (Exception) { }

        }



        private void ParseData(byte[] byteData, int nReceived)
        {
            TreeNode rootNode = new TreeNode();

            IPHeader ipHeader = new IPHeader(byteData, nReceived);

            TreeNode ipNode = MakeIPTreeNode(ipHeader);
            rootNode.Nodes.Add(ipNode);

            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    if (Filter.tcp != true)
                    {
                        return;
                    }

                    TCPHeader tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength);

                    TreeNode tcpNode = MakeTCPTreeNode(tcpHeader);

                    rootNode.Nodes.Add(tcpNode);

                    if (tcpHeader.DestinationPort == "53" || tcpHeader.SourcePort == "53")
                    {
                        TreeNode dnsNode = MakeDNSTreeNode(tcpHeader.Data, (int)tcpHeader.MessageLength);
                        rootNode.Nodes.Add(dnsNode);
                    }

                    HeaderCounter.tcp++;
                    break;

                case Protocol.UDP:
                    if (Filter.udp != true)
                    {
                        return;
                    }
                    UDPHeader udpHeader = new UDPHeader(ipHeader.Data, (int)ipHeader.MessageLength);

                    TreeNode udpNode = MakeUDPTreeNode(udpHeader);

                    rootNode.Nodes.Add(udpNode);

                    if (udpHeader.DestinationPort == "53" || udpHeader.SourcePort == "53")
                    {
                        if (Filter.dns != true)
                        {
                            return;
                        }

                        TreeNode dnsNode = MakeDNSTreeNode(udpHeader.Data,
                                                           Convert.ToInt32(udpHeader.Length) - 8);
                        rootNode.Nodes.Add(dnsNode);

                        HeaderCounter.dns++;
                    }

                    HeaderCounter.udp++;
                    break;

                case Protocol.Unknown:
                    if (Filter.unknown != true)
                    {
                        return;
                    }
                    HeaderCounter.unknown++;
                    break;
            }

            displayCounter();

            AddTreeNode addTreeNode = new AddTreeNode(OnAddTreeNode);

            rootNode.Text = ipHeader.SourceAddress.ToString() + "-" +
                ipHeader.DestinationAddress.ToString();

            treeView.Invoke(addTreeNode, new object[] { rootNode });
        }

        public void displayCounter()
        {
            counter.Invoke((MethodInvoker)(() => counter.Text = HeaderCounter.getString()));
        }

        private TreeNode MakeIPTreeNode(IPHeader ipHeader)
        {
            TreeNode ipNode = new TreeNode();

            ipNode.Text = "IP";
            ipNode.Nodes.Add("Ver: " + ipHeader.Version);
            ipNode.Nodes.Add("Header Length: " + ipHeader.HeaderLength);
            ipNode.Nodes.Add("Differntiated Services: " + ipHeader.DifferentiatedServices);
            ipNode.Nodes.Add("Total Length: " + ipHeader.TotalLength);
            ipNode.Nodes.Add("Identification: " + ipHeader.Identification);
            ipNode.Nodes.Add("Flags: " + ipHeader.Flags);
            ipNode.Nodes.Add("Fragmentation Offset: " + ipHeader.FragmentationOffset);
            ipNode.Nodes.Add("Time to live: " + ipHeader.TTL);
            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    ipNode.Nodes.Add("Protocol: " + "TCP");
                    break;
                case Protocol.UDP:
                    ipNode.Nodes.Add("Protocol: " + "UDP");
                    break;
                case Protocol.Unknown:
                    ipNode.Nodes.Add("Protocol: " + "Unknown");
                    break;
            }
            ipNode.Nodes.Add("Checksum: " + ipHeader.Checksum);
            ipNode.Nodes.Add("Source: " + ipHeader.SourceAddress.ToString());
            ipNode.Nodes.Add("Destination: " + ipHeader.DestinationAddress.ToString());

            return ipNode;
        }

        private TreeNode MakeTCPTreeNode(TCPHeader tcpHeader)
        {
            TreeNode tcpNode = new TreeNode();

            tcpNode.Text = "TCP";

            tcpNode.Nodes.Add("Source Port: " + tcpHeader.SourcePort);
            tcpNode.Nodes.Add("Destination Port: " + tcpHeader.DestinationPort);
            tcpNode.Nodes.Add("Sequence Number: " + tcpHeader.SequenceNumber);

            if (tcpHeader.AcknowledgementNumber != "")
                tcpNode.Nodes.Add("Acknowledgement Number: " + tcpHeader.AcknowledgementNumber);

            tcpNode.Nodes.Add("Header Length: " + tcpHeader.HeaderLength);
            tcpNode.Nodes.Add("Flags: " + tcpHeader.Flags);
            tcpNode.Nodes.Add("Window Size: " + tcpHeader.WindowSize);
            tcpNode.Nodes.Add("Checksum: " + tcpHeader.Checksum);

            if (tcpHeader.UrgentPointer != "")
                tcpNode.Nodes.Add("Urgent Pointer: " + tcpHeader.UrgentPointer);

            return tcpNode;
        }

        private TreeNode MakeUDPTreeNode(UDPHeader udpHeader)
        {
            TreeNode udpNode = new TreeNode();

            udpNode.Text = "UDP";
            udpNode.Nodes.Add("Source Port: " + udpHeader.SourcePort);
            udpNode.Nodes.Add("Destination Port: " + udpHeader.DestinationPort);
            udpNode.Nodes.Add("Length: " + udpHeader.Length);
            udpNode.Nodes.Add("Checksum: " + udpHeader.Checksum);

            return udpNode;
        }

        private TreeNode MakeDNSTreeNode(byte[] byteData, int nLength)
        {
            DNSHeader dnsHeader = new DNSHeader(byteData, nLength);

            TreeNode dnsNode = new TreeNode();

            dnsNode.Text = "DNS";
            dnsNode.Nodes.Add("Identification: " + dnsHeader.Identification);
            dnsNode.Nodes.Add("Flags: " + dnsHeader.Flags);
            dnsNode.Nodes.Add("Questions: " + dnsHeader.TotalQuestions);
            dnsNode.Nodes.Add("Answer RRs: " + dnsHeader.TotalAnswerRRs);
            dnsNode.Nodes.Add("Authority RRs: " + dnsHeader.TotalAuthorityRRs);
            dnsNode.Nodes.Add("Additional RRs: " + dnsHeader.TotalAdditionalRRs);

            return dnsNode;
        }

        private void OnAddTreeNode(TreeNode node)
        {
            treeView.Nodes.Add(node);
        }
    }
}
