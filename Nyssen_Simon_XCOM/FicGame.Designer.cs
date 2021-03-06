﻿namespace Nyssen_Simon_XCOM
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
            this.tsTour = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsBarreOutils = new System.Windows.Forms.ToolStrip();
            this.tsFichier = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsfSauvegarder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsfRetourMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsfQuitter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAudio = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsaMuet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.musique1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musique2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musique3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musique4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsHelp = new System.Windows.Forms.ToolStripButton();
            this.ttInfos = new System.Windows.Forms.ToolTip(this.components);
            this.dlgSauvegarder = new System.Windows.Forms.SaveFileDialog();
            this.pbCase = new System.Windows.Forms.PictureBox();
            this.pbCarte = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tsTimer = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.tsTour,
            this.tsTimer});
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
            // tsTour
            // 
            this.tsTour.Name = "tsTour";
            this.tsTour.Size = new System.Drawing.Size(31, 17);
            this.tsTour.Text = "Tour";
            // 
            // tsBarreOutils
            // 
            this.tsBarreOutils.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFichier,
            this.tsAudio,
            this.toolStripSeparator2,
            this.tsHelp});
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
            this.tsfRetourMenu,
            this.tsfQuitter});
            this.tsFichier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFichier.Name = "tsFichier";
            this.tsFichier.Size = new System.Drawing.Size(60, 22);
            this.tsFichier.Text = "Fichiers";
            // 
            // tsfSauvegarder
            // 
            this.tsfSauvegarder.Name = "tsfSauvegarder";
            this.tsfSauvegarder.Size = new System.Drawing.Size(208, 22);
            this.tsfSauvegarder.Text = "Sauvegarder";
            this.tsfSauvegarder.Click += new System.EventHandler(this.tsfSauvegarder_Click);
            // 
            // tsfRetourMenu
            // 
            this.tsfRetourMenu.Name = "tsfRetourMenu";
            this.tsfRetourMenu.Size = new System.Drawing.Size(208, 22);
            this.tsfRetourMenu.Text = "Retour au menu principal";
            this.tsfRetourMenu.Click += new System.EventHandler(this.tsfRetourMenu_Click);
            // 
            // tsfQuitter
            // 
            this.tsfQuitter.Name = "tsfQuitter";
            this.tsfQuitter.Size = new System.Drawing.Size(208, 22);
            this.tsfQuitter.Text = "Quitter";
            this.tsfQuitter.Click += new System.EventHandler(this.tsfQuitter_Click);
            // 
            // tsAudio
            // 
            this.tsAudio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsAudio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsaMuet,
            this.toolStripSeparator1,
            this.musique1ToolStripMenuItem,
            this.musique2ToolStripMenuItem,
            this.musique3ToolStripMenuItem,
            this.musique4ToolStripMenuItem});
            this.tsAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAudio.Name = "tsAudio";
            this.tsAudio.Size = new System.Drawing.Size(52, 22);
            this.tsAudio.Text = "Audio";
            // 
            // tsaMuet
            // 
            this.tsaMuet.Image = global::Nyssen_Simon_XCOM.Properties.Resources.audio_on;
            this.tsaMuet.Name = "tsaMuet";
            this.tsaMuet.Size = new System.Drawing.Size(129, 22);
            this.tsaMuet.Text = "audio on";
            this.tsaMuet.Click += new System.EventHandler(this.tsaMuet_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // musique1ToolStripMenuItem
            // 
            this.musique1ToolStripMenuItem.Image = global::Nyssen_Simon_XCOM.Properties.Resources.audio_play;
            this.musique1ToolStripMenuItem.Name = "musique1ToolStripMenuItem";
            this.musique1ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.musique1ToolStripMenuItem.Text = "Musique 1";
            this.musique1ToolStripMenuItem.Click += new System.EventHandler(this.musique1ToolStripMenuItem_Click);
            // 
            // musique2ToolStripMenuItem
            // 
            this.musique2ToolStripMenuItem.Name = "musique2ToolStripMenuItem";
            this.musique2ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.musique2ToolStripMenuItem.Text = "Musique 2";
            this.musique2ToolStripMenuItem.Click += new System.EventHandler(this.musique2ToolStripMenuItem_Click);
            // 
            // musique3ToolStripMenuItem
            // 
            this.musique3ToolStripMenuItem.Name = "musique3ToolStripMenuItem";
            this.musique3ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.musique3ToolStripMenuItem.Text = "Musique 3";
            this.musique3ToolStripMenuItem.Click += new System.EventHandler(this.musique3ToolStripMenuItem_Click);
            // 
            // musique4ToolStripMenuItem
            // 
            this.musique4ToolStripMenuItem.Name = "musique4ToolStripMenuItem";
            this.musique4ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.musique4ToolStripMenuItem.Text = "Musique 4";
            this.musique4ToolStripMenuItem.Click += new System.EventHandler(this.musique4ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsHelp
            // 
            this.tsHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsHelp.Image")));
            this.tsHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsHelp.Name = "tsHelp";
            this.tsHelp.Size = new System.Drawing.Size(23, 22);
            this.tsHelp.ToolTipText = "Aide";
            this.tsHelp.Click += new System.EventHandler(this.tsHelp_Click);
            // 
            // dlgSauvegarder
            // 
            this.dlgSauvegarder.DefaultExt = "sav";
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
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tsTimer
            // 
            this.tsTimer.Name = "tsTimer";
            this.tsTimer.Size = new System.Drawing.Size(49, 17);
            this.tsTimer.Text = "00:00:00";
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
        private System.Windows.Forms.ToolStripMenuItem tsfRetourMenu;
        private System.Windows.Forms.ToolStripMenuItem tsfQuitter;
        private System.Windows.Forms.ToolTip ttInfos;
        private System.Windows.Forms.SaveFileDialog dlgSauvegarder;
        private System.Windows.Forms.ToolStripStatusLabel tsTour;
        private System.Windows.Forms.ToolStripDropDownButton tsAudio;
        private System.Windows.Forms.ToolStripMenuItem tsaMuet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem musique1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem musique2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem musique3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem musique4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsHelp;
        private System.Windows.Forms.ToolStripStatusLabel tsTimer;
        private System.Windows.Forms.Timer timer1;
    }
}