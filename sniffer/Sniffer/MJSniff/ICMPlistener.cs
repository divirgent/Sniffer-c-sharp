using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Sniffer
{
    class ICMPlistener
    {
        Socket icmpListener;
        byte[] buffer;
        EndPoint remoteEndPoint;
        int bytesRead;
        private delegate void AddTreeNode(TreeNode node);
        TreeView treeView;
        Label counter;

        String ip;

        public ICMPlistener(String ip, TreeView treeView, Label counter)
        {
            this.ip = ip;
            this.treeView = treeView;
            this.counter = counter;


            icmpListener = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            icmpListener.Bind(new IPEndPoint(IPAddress.Parse(ip), 0));
            icmpListener.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, new byte[] { 1, 0, 0, 0 });

            buffer = new byte[4096];
            remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        public void displayCounter()
        {
            counter.Invoke((MethodInvoker)(() => counter.Text = HeaderCounter.getString()));
        }

        public void start()
        {
            icmpListener.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
                        new AsyncCallback(OnReceiveIcmp), null);
        }

        public TreeNode MakeICMPTreeNode(ICMPHeader icmpHeader)
        {
            TreeNode icmpNode = new TreeNode();

            icmpNode.Text = "ICMP";
            icmpNode.Nodes.Add("type: " + icmpHeader.type);
            icmpNode.Nodes.Add("code " + icmpHeader.code);
            icmpNode.Nodes.Add("Checksum: " + icmpHeader.checksum);

            return icmpNode;
        }

        public void OnReceiveIcmp(IAsyncResult ar)
        {
            int nReceived = icmpListener.EndReceive(ar);

            ParseDataIcmp(buffer, nReceived);

            buffer = new byte[4096];

            icmpListener.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
                new AsyncCallback(OnReceiveIcmp), null);
        }



        public void ParseDataIcmp(byte[] byteData, int nReceived)
        {
            TreeNode rootNode = new TreeNode();

            ICMPHeader icmpHeader = new ICMPHeader(byteData, nReceived);

            TreeNode icmpNode = MakeICMPTreeNode(icmpHeader);
            rootNode.Nodes.Add(icmpNode);

            AddTreeNode addTreeNode = new AddTreeNode(OnAddTreeNode);

            rootNode.Text = nReceived.ToString();

            treeView.Invoke(addTreeNode, new object[] { rootNode });

            HeaderCounter.icmp++;
        }

        private void OnAddTreeNode(TreeNode node)
        {
            treeView.Nodes.Add(node);
        }
    }
}
