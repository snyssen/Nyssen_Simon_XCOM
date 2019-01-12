namespace Nyssen_Simon_XCOM
{
    partial class EcranMulti
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
            this.BtnClient = new System.Windows.Forms.Button();
            this.BtnServer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TbIP = new System.Windows.Forms.TextBox();
            this.LblIP = new System.Windows.Forms.Label();
            this.LblPort = new System.Windows.Forms.Label();
            this.NudPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TbMsg = new System.Windows.Forms.TextBox();
            this.BtnSendMsg = new System.Windows.Forms.Button();
            this.LbMsg = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClient
            // 
            this.BtnClient.Location = new System.Drawing.Point(6, 19);
            this.BtnClient.Name = "BtnClient";
            this.BtnClient.Size = new System.Drawing.Size(150, 50);
            this.BtnClient.TabIndex = 0;
            this.BtnClient.Text = "Se connecter à un serveur";
            this.BtnClient.UseVisualStyleBackColor = true;
            this.BtnClient.Click += new System.EventHandler(this.BtnClient_Click);
            // 
            // BtnServer
            // 
            this.BtnServer.Location = new System.Drawing.Point(166, 19);
            this.BtnServer.Name = "BtnServer";
            this.BtnServer.Size = new System.Drawing.Size(150, 50);
            this.BtnServer.TabIndex = 1;
            this.BtnServer.Text = "S\'établir en tant que serveur";
            this.BtnServer.UseVisualStyleBackColor = true;
            this.BtnServer.Click += new System.EventHandler(this.BtnServer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NudPort);
            this.groupBox1.Controls.Add(this.LblPort);
            this.groupBox1.Controls.Add(this.LblIP);
            this.groupBox1.Controls.Add(this.TbIP);
            this.groupBox1.Controls.Add(this.BtnClient);
            this.groupBox1.Controls.Add(this.BtnServer);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 134);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration de la connexion";
            // 
            // TbIP
            // 
            this.TbIP.Location = new System.Drawing.Point(70, 75);
            this.TbIP.Name = "TbIP";
            this.TbIP.Size = new System.Drawing.Size(246, 20);
            this.TbIP.TabIndex = 2;
            this.TbIP.Text = "127.0.0.1";
            this.TbIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LblIP
            // 
            this.LblIP.AutoSize = true;
            this.LblIP.Location = new System.Drawing.Point(6, 78);
            this.LblIP.Name = "LblIP";
            this.LblIP.Size = new System.Drawing.Size(58, 13);
            this.LblIP.TabIndex = 3;
            this.LblIP.Text = "Adresse IP";
            // 
            // LblPort
            // 
            this.LblPort.AutoSize = true;
            this.LblPort.Location = new System.Drawing.Point(134, 103);
            this.LblPort.Name = "LblPort";
            this.LblPort.Size = new System.Drawing.Size(26, 13);
            this.LblPort.TabIndex = 5;
            this.LblPort.Text = "Port";
            // 
            // NudPort
            // 
            this.NudPort.Location = new System.Drawing.Point(166, 101);
            this.NudPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudPort.Name = "NudPort";
            this.NudPort.Size = new System.Drawing.Size(150, 20);
            this.NudPort.TabIndex = 6;
            this.NudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NudPort.Value = new decimal(new int[] {
            1337,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LbMsg);
            this.groupBox2.Controls.Add(this.BtnSendMsg);
            this.groupBox2.Controls.Add(this.TbMsg);
            this.groupBox2.Location = new System.Drawing.Point(13, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 207);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chat";
            // 
            // TbMsg
            // 
            this.TbMsg.Location = new System.Drawing.Point(5, 19);
            this.TbMsg.Name = "TbMsg";
            this.TbMsg.Size = new System.Drawing.Size(228, 20);
            this.TbMsg.TabIndex = 0;
            // 
            // BtnSendMsg
            // 
            this.BtnSendMsg.Enabled = false;
            this.BtnSendMsg.Location = new System.Drawing.Point(239, 19);
            this.BtnSendMsg.Name = "BtnSendMsg";
            this.BtnSendMsg.Size = new System.Drawing.Size(75, 20);
            this.BtnSendMsg.TabIndex = 1;
            this.BtnSendMsg.Text = "Envoyer";
            this.BtnSendMsg.UseVisualStyleBackColor = true;
            this.BtnSendMsg.Click += new System.EventHandler(this.BtnSendMsg_Click);
            // 
            // LbMsg
            // 
            this.LbMsg.FormattingEnabled = true;
            this.LbMsg.Location = new System.Drawing.Point(5, 46);
            this.LbMsg.Name = "LbMsg";
            this.LbMsg.Size = new System.Drawing.Size(309, 147);
            this.LbMsg.TabIndex = 2;
            // 
            // EcranMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EcranMulti";
            this.Text = "Multijoueur";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnClient;
        private System.Windows.Forms.Button BtnServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown NudPort;
        private System.Windows.Forms.Label LblPort;
        private System.Windows.Forms.Label LblIP;
        private System.Windows.Forms.TextBox TbIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox LbMsg;
        private System.Windows.Forms.Button BtnSendMsg;
        private System.Windows.Forms.TextBox TbMsg;
    }
}