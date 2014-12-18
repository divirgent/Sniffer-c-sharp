namespace Sniffer
{
    partial class MJsnifferForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbInterfaces = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unknownCheckBox = new System.Windows.Forms.CheckBox();
            this.dnsCheckBox = new System.Windows.Forms.CheckBox();
            this.udpCheckBox = new System.Windows.Forms.CheckBox();
            this.tcpCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.icmpCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(11, 39);
            this.treeView.Margin = new System.Windows.Forms.Padding(0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(292, 190);
            this.treeView.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(309, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(129, 21);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbInterfaces
            // 
            this.cmbInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterfaces.FormattingEnabled = true;
            this.cmbInterfaces.Location = new System.Drawing.Point(11, 12);
            this.cmbInterfaces.Name = "cmbInterfaces";
            this.cmbInterfaces.Size = new System.Drawing.Size(292, 21);
            this.cmbInterfaces.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.icmpCheckBox);
            this.groupBox1.Controls.Add(this.unknownCheckBox);
            this.groupBox1.Controls.Add(this.dnsCheckBox);
            this.groupBox1.Controls.Add(this.udpCheckBox);
            this.groupBox1.Controls.Add(this.tcpCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(309, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(126, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // unknownCheckBox
            // 
            this.unknownCheckBox.AutoSize = true;
            this.unknownCheckBox.Location = new System.Drawing.Point(6, 112);
            this.unknownCheckBox.Name = "unknownCheckBox";
            this.unknownCheckBox.Size = new System.Drawing.Size(70, 17);
            this.unknownCheckBox.TabIndex = 3;
            this.unknownCheckBox.Text = "unknown";
            this.unknownCheckBox.UseVisualStyleBackColor = true;
            // 
            // dnsCheckBox
            // 
            this.dnsCheckBox.AutoSize = true;
            this.dnsCheckBox.Checked = true;
            this.dnsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dnsCheckBox.Location = new System.Drawing.Point(6, 65);
            this.dnsCheckBox.Name = "dnsCheckBox";
            this.dnsCheckBox.Size = new System.Drawing.Size(43, 17);
            this.dnsCheckBox.TabIndex = 2;
            this.dnsCheckBox.Text = "dns";
            this.dnsCheckBox.UseVisualStyleBackColor = true;
            // 
            // udpCheckBox
            // 
            this.udpCheckBox.AutoSize = true;
            this.udpCheckBox.Checked = true;
            this.udpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.udpCheckBox.Location = new System.Drawing.Point(6, 42);
            this.udpCheckBox.Name = "udpCheckBox";
            this.udpCheckBox.Size = new System.Drawing.Size(44, 17);
            this.udpCheckBox.TabIndex = 1;
            this.udpCheckBox.Text = "udp";
            this.udpCheckBox.UseVisualStyleBackColor = true;
            // 
            // tcpCheckBox
            // 
            this.tcpCheckBox.AutoSize = true;
            this.tcpCheckBox.Checked = true;
            this.tcpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tcpCheckBox.Location = new System.Drawing.Point(6, 19);
            this.tcpCheckBox.Name = "tcpCheckBox";
            this.tcpCheckBox.Size = new System.Drawing.Size(41, 17);
            this.tcpCheckBox.TabIndex = 0;
            this.tcpCheckBox.Text = "tcp";
            this.tcpCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // icmpCheckBox
            // 
            this.icmpCheckBox.AutoSize = true;
            this.icmpCheckBox.Checked = true;
            this.icmpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.icmpCheckBox.Location = new System.Drawing.Point(6, 88);
            this.icmpCheckBox.Name = "icmpCheckBox";
            this.icmpCheckBox.Size = new System.Drawing.Size(48, 17);
            this.icmpCheckBox.TabIndex = 4;
            this.icmpCheckBox.Text = "icmp";
            this.icmpCheckBox.UseVisualStyleBackColor = true;
            // 
            // MJsnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 265);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbInterfaces);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.treeView);
            this.Name = "MJsnifferForm";
            this.Text = "Sniffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnifferForm_FormClosing);
            this.Load += new System.EventHandler(this.SnifferForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cmbInterfaces;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox unknownCheckBox;
        private System.Windows.Forms.CheckBox dnsCheckBox;
        private System.Windows.Forms.CheckBox udpCheckBox;
        private System.Windows.Forms.CheckBox tcpCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox icmpCheckBox;
    }
}

