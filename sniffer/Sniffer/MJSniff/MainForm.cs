using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Sniffer
{
    public partial class MJsnifferForm : Form
    {
        private delegate void AddTreeNode(TreeNode node);

        public MJsnifferForm()
        {
            InitializeComponent();

            label1.Text = HeaderCounter.getString();
        }

        public void setFilter()
        {
            Filter.dns = dnsCheckBox.Checked;
            Filter.udp = udpCheckBox.Checked;
            Filter.tcp = tcpCheckBox.Checked;
            Filter.icmp = icmpCheckBox.Checked;
            Filter.unknown = unknownCheckBox.Checked;
        }

        public void displayCounter()
        {
            label1.Invoke((MethodInvoker)(() => label1.Text = HeaderCounter.getString()));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbInterfaces.Text == "")
            {
                MessageBox.Show("Select an Interface to capture the packets.", "Sniffer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnStart.Text = "&Stop";
            setFilter();
            groupBox1.Enabled = false;


            IPlistener ip = new IPlistener(cmbInterfaces.Text, treeView, label1);
            ip.start();

            ICMPlistener icmp = new ICMPlistener(cmbInterfaces.Text, treeView, label1);
            icmp.start();
        }

        private void OnAddTreeNode(TreeNode node)
        {
            treeView.Nodes.Add(node);
        }

        private void SnifferForm_Load(object sender, EventArgs e)
        {
            string strIP = null;

            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    strIP = ip.ToString();
                    cmbInterfaces.Items.Add(strIP);
                }
            }
        }

        private void SnifferForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}