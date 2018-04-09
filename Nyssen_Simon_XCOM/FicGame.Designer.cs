namespace Nyssen_Simon_XCOM
{
    partial class EcranGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EcranGame));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsNbrSoldatsJoue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsAvancement = new System.Windows.Forms.ToolStripProgressBar();
            this.tsBarreOutils = new System.Windows.Forms.ToolStrip();
            this.tsFichier = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsfSauvegarder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsfCharger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsfQuitter = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCase = new System.Windows.Forms.PictureBox();
            this.pbCarte = new System.Windows.Forms.PictureBox();
            this.ttInfos = new System.Windows.Forms.ToolTip(this.components);
            this.dlgSauvegarder = new System.Windows.Forms.SaveFileDialog();
            this.tsTour = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.tsBarreOutils.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarte)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsInfo,
            this.tsNbrSoldatsJoue,
            this.tsAvancement,
            this.tsTour});
            this.statusStrip1.Location = new System.Drawing.Point(0, 531);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(500, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsInfo
            // 
            this.tsInfo.Name = "tsInfo";
            this.tsInfo.Size = new System.Drawing.Size(38, 17);
            this.tsInfo.Text = "status";
            // 
            // tsNbrSoldatsJoue
            // 
            this.tsNbrSoldatsJoue.Name = "tsNbrSoldatsJoue";
            this.tsNbrSoldatsJoue.Size = new System.Drawing.Size(24, 17);
            this.tsNbrSoldatsJoue.Text = "0/6";
            // 
            // tsAvancement
            // 
            this.tsAvancement.Name = "tsAvancement";
            this.tsAvancement.Size = new System.Drawing.Size(100, 16);
            this.tsAvancement.Step = 1;
            // 
            // tsBarreOutils
            // 
            this.tsBarreOutils.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFichier});
            this.tsBarreOutils.Location = new System.Drawing.Point(0, 0);
            this.tsBarreOutils.Name = "tsBarreOutils";
            this.tsBarreOutils.Size = new System.Drawing.Size(500, 25);
            this.tsBarreOutils.TabIndex = 3;
            this.tsBarreOutils.Text = "toolStrip1";
            // 
            // tsFichier
            // 
            this.tsFichier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsFichier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsfSauvegarder,
            this.tsfCharger,
            this.tsfQuitter});
            this.tsFichier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFichier.Name = "tsFichier";
            this.tsFichier.Size = new System.Drawing.Size(60, 22);
            this.tsFichier.Text = "Fichiers";
            // 
            // tsfSauvegarder
            // 
            this.tsfSauvegarder.Name = "tsfSauvegarder";
            this.tsfSauvegarder.Size = new System.Drawing.Size(139, 22);
            this.tsfSauvegarder.Text = "Sauvegarder";
            this.tsfSauvegarder.Click += new System.EventHandler(this.tsfSauvegarder_Click);
            // 
            // tsfCharger
            // 
            this.tsfCharger.Name = "tsfCharger";
            this.tsfCharger.Size = new System.Drawing.Size(139, 22);
            this.tsfCharger.Text = "Charger";
            this.tsfCharger.Click += new System.EventHandler(this.tsfCharger_Click);
            // 
            // tsfQuitter
            // 
            this.tsfQuitter.Name = "tsfQuitter";
            this.tsfQuitter.Size = new System.Drawing.Size(139, 22);
            this.tsfQuitter.Text = "Quitter";
            this.tsfQuitter.Click += new System.EventHandler(this.tsfQuitter_Click);
            // 
            // pbCase
            // 
            this.pbCase.BackColor = System.Drawing.Color.Transparent;
            this.pbCase.Location = new System.Drawing.Point(12, 51);
            this.pbCase.Name = "pbCase";
            this.pbCase.Size = new System.Drawing.Size(100, 50);
            this.pbCase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCase.TabIndex = 2;
            this.pbCase.TabStop = false;
            this.pbCase.Visible = false;
            this.pbCase.Click += new System.EventHandler(this.pbCase_Click);
            // 
            // pbCarte
            // 
            this.pbCarte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCarte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCarte.Location = new System.Drawing.Point(0, 28);
            this.pbCarte.Name = "pbCarte";
            this.pbCarte.Size = new System.Drawing.Size(500, 500);
            this.pbCarte.TabIndex = 0;
            this.pbCarte.TabStop = false;
            this.pbCarte.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCarte_Paint);
            this.pbCarte.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCarte_MouseMove);
            // 
            // dlgSauvegarder
            // 
            this.dlgSauvegarder.DefaultExt = "sav";
            // 
            // tsTour
            // 
            this.tsTour.Name = "tsTour";
            this.tsTour.Size = new System.Drawing.Size(118, 17);
            this.tsTour.Text = "toolStripStatusLabel1";
            // 
            // EcranGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 553);
            this.Controls.Add(this.tsBarreOutils);
            this.Controls.Add(this.pbCase);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbCarte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(266, 342);
            this.Name = "EcranGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partie en cours";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EcranGame_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EcranGame_FormClosed);
            this.Resize += new System.EventHandler(this.EcranGame_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsBarreOutils.ResumeLayout(false);
            this.tsBarreOutils.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCarte;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsInfo;
        private System.Windows.Forms.ToolStripProgressBar tsAvancement;
        private System.Windows.Forms.ToolStripStatusLabel tsNbrSoldatsJoue;
        private System.Windows.Forms.PictureBox pbCase;
        private System.Windows.Forms.ToolStrip tsBarreOutils;
        private System.Windows.Forms.ToolStripDropDownButton tsFichier;
        private System.Windows.Forms.ToolStripMenuItem tsfSauvegarder;
        private System.Windows.Forms.ToolStripMenuItem tsfCharger;
        private System.Windows.Forms.ToolStripMenuItem tsfQuitter;
        private System.Windows.Forms.ToolTip ttInfos;
        private System.Windows.Forms.SaveFileDialog dlgSauvegarder;
        private System.Windows.Forms.ToolStripStatusLabel tsTour;
    }
}