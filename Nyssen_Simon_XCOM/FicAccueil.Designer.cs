namespace Nyssen_Simon_XCOM
{
    partial class EcranAccueil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EcranAccueil));
            this.pAccueil = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.dlgLoadGame = new System.Windows.Forms.OpenFileDialog();
            this.llblCopyright = new System.Windows.Forms.LinkLabel();
            this.pAccueil.SuspendLayout();
            this.SuspendLayout();
            // 
            // pAccueil
            // 
            this.pAccueil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pAccueil.Controls.Add(this.btnExit);
            this.pAccueil.Controls.Add(this.btnLoadGame);
            this.pAccueil.Controls.Add(this.btnNewGame);
            this.pAccueil.Location = new System.Drawing.Point(13, 13);
            this.pAccueil.Name = "pAccueil";
            this.pAccueil.Size = new System.Drawing.Size(352, 203);
            this.pAccueil.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(4, 134);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(341, 59);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Quitter";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(4, 69);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(341, 59);
            this.btnLoadGame.TabIndex = 1;
            this.btnLoadGame.Text = "Charger partie";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(4, 4);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(341, 59);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "Nouvelle partie";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // dlgLoadGame
            // 
            this.dlgLoadGame.FileName = "openFileDialog1";
            // 
            // llblCopyright
            // 
            this.llblCopyright.AutoSize = true;
            this.llblCopyright.Location = new System.Drawing.Point(286, 239);
            this.llblCopyright.Name = "llblCopyright";
            this.llblCopyright.Size = new System.Drawing.Size(86, 13);
            this.llblCopyright.TabIndex = 1;
            this.llblCopyright.TabStop = true;
            this.llblCopyright.Text = "© Simon Nyssen";
            this.llblCopyright.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblCopyright_LinkClicked);
            // 
            // EcranAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.llblCopyright);
            this.Controls.Add(this.pAccueil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EcranAccueil";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XCOM";
            this.pAccueil.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pAccueil;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.OpenFileDialog dlgLoadGame;
        private System.Windows.Forms.LinkLabel llblCopyright;
    }
}