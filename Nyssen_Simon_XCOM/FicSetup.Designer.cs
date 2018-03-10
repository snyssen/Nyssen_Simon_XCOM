namespace Nyssen_Simon_XCOM
{
    partial class EcranSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EcranSetup));
            this.tbNbrSoldats = new System.Windows.Forms.TrackBar();
            this.btnLancer = new System.Windows.Forms.Button();
            this.gbChoixCarte = new System.Windows.Forms.GroupBox();
            this.btnUrban = new System.Windows.Forms.Button();
            this.btnSnowy = new System.Windows.Forms.Button();
            this.btnDesert = new System.Windows.Forms.Button();
            this.gbNbrSoldats = new System.Windows.Forms.GroupBox();
            this.gbCompo = new System.Windows.Forms.GroupBox();
            this.cbSniper = new System.Windows.Forms.ComboBox();
            this.cbLeger = new System.Windows.Forms.ComboBox();
            this.cbLourd = new System.Windows.Forms.ComboBox();
            this.cbFantassin = new System.Windows.Forms.ComboBox();
            this.lblTypeSoldat = new System.Windows.Forms.Label();
            this.ttDescription = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbNbrSoldats)).BeginInit();
            this.gbChoixCarte.SuspendLayout();
            this.gbNbrSoldats.SuspendLayout();
            this.gbCompo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNbrSoldats
            // 
            this.tbNbrSoldats.LargeChange = 2;
            this.tbNbrSoldats.Location = new System.Drawing.Point(6, 25);
            this.tbNbrSoldats.Maximum = 6;
            this.tbNbrSoldats.Minimum = 2;
            this.tbNbrSoldats.Name = "tbNbrSoldats";
            this.tbNbrSoldats.Size = new System.Drawing.Size(121, 45);
            this.tbNbrSoldats.SmallChange = 2;
            this.tbNbrSoldats.TabIndex = 0;
            this.tbNbrSoldats.TickFrequency = 2;
            this.tbNbrSoldats.Value = 4;
            this.tbNbrSoldats.Scroll += new System.EventHandler(this.tbNbrSoldats_Scroll);
            // 
            // btnLancer
            // 
            this.btnLancer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLancer.Location = new System.Drawing.Point(13, 216);
            this.btnLancer.Name = "btnLancer";
            this.btnLancer.Size = new System.Drawing.Size(126, 33);
            this.btnLancer.TabIndex = 3;
            this.btnLancer.Text = "Lancer la partie";
            this.btnLancer.UseVisualStyleBackColor = true;
            this.btnLancer.Click += new System.EventHandler(this.btnLancer_Click);
            // 
            // gbChoixCarte
            // 
            this.gbChoixCarte.Controls.Add(this.btnUrban);
            this.gbChoixCarte.Controls.Add(this.btnSnowy);
            this.gbChoixCarte.Controls.Add(this.btnDesert);
            this.gbChoixCarte.Location = new System.Drawing.Point(145, 12);
            this.gbChoixCarte.Name = "gbChoixCarte";
            this.gbChoixCarte.Size = new System.Drawing.Size(227, 237);
            this.gbChoixCarte.TabIndex = 2;
            this.gbChoixCarte.TabStop = false;
            this.gbChoixCarte.Text = "Choix du champs de bataille";
            // 
            // btnUrban
            // 
            this.btnUrban.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUrban.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUrban.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnUrban.ForeColor = System.Drawing.Color.Aqua;
            this.btnUrban.Image = global::Nyssen_Simon_XCOM.Properties.Resources.Urban_Parking;
            this.btnUrban.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUrban.Location = new System.Drawing.Point(6, 174);
            this.btnUrban.Name = "btnUrban";
            this.btnUrban.Size = new System.Drawing.Size(215, 57);
            this.btnUrban.TabIndex = 2;
            this.ttDescription.SetToolTip(this.btnUrban, "Terrain urbain");
            this.btnUrban.UseVisualStyleBackColor = true;
            this.btnUrban.Click += new System.EventHandler(this.btnUrban_Click);
            // 
            // btnSnowy
            // 
            this.btnSnowy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSnowy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSnowy.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnSnowy.ForeColor = System.Drawing.Color.Aqua;
            this.btnSnowy.Image = global::Nyssen_Simon_XCOM.Properties.Resources.Snowy_Pass;
            this.btnSnowy.Location = new System.Drawing.Point(6, 95);
            this.btnSnowy.Name = "btnSnowy";
            this.btnSnowy.Size = new System.Drawing.Size(215, 57);
            this.btnSnowy.TabIndex = 1;
            this.ttDescription.SetToolTip(this.btnSnowy, "Terrain enneigé");
            this.btnSnowy.UseVisualStyleBackColor = true;
            this.btnSnowy.Click += new System.EventHandler(this.btnSnowy_Click);
            // 
            // btnDesert
            // 
            this.btnDesert.BackColor = System.Drawing.Color.Aqua;
            this.btnDesert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDesert.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesert.ForeColor = System.Drawing.Color.Aqua;
            this.btnDesert.Image = global::Nyssen_Simon_XCOM.Properties.Resources.desert_road;
            this.btnDesert.Location = new System.Drawing.Point(6, 19);
            this.btnDesert.Name = "btnDesert";
            this.btnDesert.Size = new System.Drawing.Size(215, 57);
            this.btnDesert.TabIndex = 0;
            this.ttDescription.SetToolTip(this.btnDesert, "Terrain désertique");
            this.btnDesert.UseVisualStyleBackColor = false;
            this.btnDesert.Click += new System.EventHandler(this.btnDesert_Click);
            // 
            // gbNbrSoldats
            // 
            this.gbNbrSoldats.Controls.Add(this.tbNbrSoldats);
            this.gbNbrSoldats.Location = new System.Drawing.Point(12, 12);
            this.gbNbrSoldats.Name = "gbNbrSoldats";
            this.gbNbrSoldats.Size = new System.Drawing.Size(133, 76);
            this.gbNbrSoldats.TabIndex = 0;
            this.gbNbrSoldats.TabStop = false;
            this.gbNbrSoldats.Text = "Nombre de soldats";
            // 
            // gbCompo
            // 
            this.gbCompo.Controls.Add(this.cbSniper);
            this.gbCompo.Controls.Add(this.cbLeger);
            this.gbCompo.Controls.Add(this.cbLourd);
            this.gbCompo.Controls.Add(this.cbFantassin);
            this.gbCompo.Controls.Add(this.lblTypeSoldat);
            this.gbCompo.Location = new System.Drawing.Point(12, 88);
            this.gbCompo.Name = "gbCompo";
            this.gbCompo.Size = new System.Drawing.Size(133, 122);
            this.gbCompo.TabIndex = 1;
            this.gbCompo.TabStop = false;
            this.gbCompo.Text = "Composition";
            // 
            // cbSniper
            // 
            this.cbSniper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSniper.FormattingEnabled = true;
            this.cbSniper.Items.AddRange(new object[] {
            "0"});
            this.cbSniper.Location = new System.Drawing.Point(6, 41);
            this.cbSniper.Name = "cbSniper";
            this.cbSniper.Size = new System.Drawing.Size(39, 21);
            this.cbSniper.TabIndex = 1;
            this.cbSniper.SelectedIndexChanged += new System.EventHandler(this.cbFantassin_SelectedIndexChanged);
            // 
            // cbLeger
            // 
            this.cbLeger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeger.FormattingEnabled = true;
            this.cbLeger.Items.AddRange(new object[] {
            "0"});
            this.cbLeger.Location = new System.Drawing.Point(6, 95);
            this.cbLeger.Name = "cbLeger";
            this.cbLeger.Size = new System.Drawing.Size(39, 21);
            this.cbLeger.TabIndex = 3;
            this.cbLeger.SelectedIndexChanged += new System.EventHandler(this.cbFantassin_SelectedIndexChanged);
            // 
            // cbLourd
            // 
            this.cbLourd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLourd.FormattingEnabled = true;
            this.cbLourd.Items.AddRange(new object[] {
            "0"});
            this.cbLourd.Location = new System.Drawing.Point(6, 68);
            this.cbLourd.Name = "cbLourd";
            this.cbLourd.Size = new System.Drawing.Size(39, 21);
            this.cbLourd.TabIndex = 2;
            this.cbLourd.SelectedIndexChanged += new System.EventHandler(this.cbFantassin_SelectedIndexChanged);
            // 
            // cbFantassin
            // 
            this.cbFantassin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFantassin.FormattingEnabled = true;
            this.cbFantassin.Items.AddRange(new object[] {
            "0"});
            this.cbFantassin.Location = new System.Drawing.Point(6, 14);
            this.cbFantassin.Name = "cbFantassin";
            this.cbFantassin.Size = new System.Drawing.Size(39, 21);
            this.cbFantassin.TabIndex = 0;
            this.cbFantassin.SelectedIndexChanged += new System.EventHandler(this.cbFantassin_SelectedIndexChanged);
            // 
            // lblTypeSoldat
            // 
            this.lblTypeSoldat.AutoSize = true;
            this.lblTypeSoldat.Location = new System.Drawing.Point(51, 19);
            this.lblTypeSoldat.Name = "lblTypeSoldat";
            this.lblTypeSoldat.Size = new System.Drawing.Size(52, 91);
            this.lblTypeSoldat.TabIndex = 1;
            this.lblTypeSoldat.Text = "Fantassin\r\n\r\nSniper\r\n\r\nLourd\r\n\r\nLéger\r\n";
            // 
            // ttDescription
            // 
            this.ttDescription.Tag = "";
            // 
            // EcranSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.gbCompo);
            this.Controls.Add(this.gbNbrSoldats);
            this.Controls.Add(this.gbChoixCarte);
            this.Controls.Add(this.btnLancer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EcranSetup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paramètres de la partie";
            ((System.ComponentModel.ISupportInitialize)(this.tbNbrSoldats)).EndInit();
            this.gbChoixCarte.ResumeLayout(false);
            this.gbNbrSoldats.ResumeLayout(false);
            this.gbNbrSoldats.PerformLayout();
            this.gbCompo.ResumeLayout(false);
            this.gbCompo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TrackBar tbNbrSoldats;
        private System.Windows.Forms.Button btnLancer;
        private System.Windows.Forms.GroupBox gbChoixCarte;
        private System.Windows.Forms.Button btnUrban;
        private System.Windows.Forms.Button btnSnowy;
        private System.Windows.Forms.GroupBox gbNbrSoldats;
        private System.Windows.Forms.GroupBox gbCompo;
        private System.Windows.Forms.Label lblTypeSoldat;
        private System.Windows.Forms.ComboBox cbSniper;
        private System.Windows.Forms.ComboBox cbLeger;
        private System.Windows.Forms.ComboBox cbLourd;
        private System.Windows.Forms.ComboBox cbFantassin;
        private System.Windows.Forms.ToolTip ttDescription;
        private System.Windows.Forms.Button btnDesert;
    }
}