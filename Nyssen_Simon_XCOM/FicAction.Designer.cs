namespace Nyssen_Simon_XCOM
{
    partial class EcranAction
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
            this.btnMoving = new System.Windows.Forms.Button();
            this.btnCovering = new System.Windows.Forms.Button();
            this.btnFiring = new System.Windows.Forms.Button();
            this.lblMoving = new System.Windows.Forms.Label();
            this.lblCovering = new System.Windows.Forms.Label();
            this.lblFiring = new System.Windows.Forms.Label();
            this.ttAide = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnMoving
            // 
            this.btnMoving.BackColor = System.Drawing.Color.Transparent;
            this.btnMoving.BackgroundImage = global::Nyssen_Simon_XCOM.Properties.Resources.Moving;
            this.btnMoving.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoving.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoving.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoving.Location = new System.Drawing.Point(12, 74);
            this.btnMoving.Name = "btnMoving";
            this.btnMoving.Size = new System.Drawing.Size(50, 50);
            this.btnMoving.TabIndex = 0;
            this.ttAide.SetToolTip(this.btnMoving, "Déplacer le soldat vers la position.");
            this.btnMoving.UseVisualStyleBackColor = false;
            this.btnMoving.Click += new System.EventHandler(this.btnMoving_Click);
            // 
            // btnCovering
            // 
            this.btnCovering.BackColor = System.Drawing.Color.Transparent;
            this.btnCovering.BackgroundImage = global::Nyssen_Simon_XCOM.Properties.Resources.Covering;
            this.btnCovering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCovering.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCovering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCovering.Location = new System.Drawing.Point(68, 74);
            this.btnCovering.Name = "btnCovering";
            this.btnCovering.Size = new System.Drawing.Size(50, 50);
            this.btnCovering.TabIndex = 1;
            this.ttAide.SetToolTip(this.btnCovering, "Renforcer la position actuelle du soldat.\r\n\r\nCeci permet d\'augmenter les chances " +
        "d\'esquiver les tirs de l\'ennemi pendant le tour suivant.");
            this.btnCovering.UseVisualStyleBackColor = false;
            this.btnCovering.Click += new System.EventHandler(this.btnCovering_Click);
            // 
            // btnFiring
            // 
            this.btnFiring.BackColor = System.Drawing.Color.Transparent;
            this.btnFiring.BackgroundImage = global::Nyssen_Simon_XCOM.Properties.Resources.Firing;
            this.btnFiring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFiring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiring.Location = new System.Drawing.Point(124, 74);
            this.btnFiring.Name = "btnFiring";
            this.btnFiring.Size = new System.Drawing.Size(50, 50);
            this.btnFiring.TabIndex = 2;
            this.ttAide.SetToolTip(this.btnFiring, "Tirer sur un soldat adverse.");
            this.btnFiring.UseVisualStyleBackColor = false;
            this.btnFiring.Click += new System.EventHandler(this.btnFiring_Click);
            // 
            // lblMoving
            // 
            this.lblMoving.Location = new System.Drawing.Point(12, 31);
            this.lblMoving.Name = "lblMoving";
            this.lblMoving.Size = new System.Drawing.Size(50, 40);
            this.lblMoving.TabIndex = 3;
            this.lblMoving.Text = "Déplacer";
            this.lblMoving.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCovering
            // 
            this.lblCovering.Location = new System.Drawing.Point(68, 31);
            this.lblCovering.Name = "lblCovering";
            this.lblCovering.Size = new System.Drawing.Size(50, 40);
            this.lblCovering.TabIndex = 4;
            this.lblCovering.Text = "Tenir\r\nla position\r\n";
            this.lblCovering.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblFiring
            // 
            this.lblFiring.Location = new System.Drawing.Point(124, 31);
            this.lblFiring.Name = "lblFiring";
            this.lblFiring.Size = new System.Drawing.Size(50, 40);
            this.lblFiring.TabIndex = 5;
            this.lblFiring.Text = "Tirer";
            this.lblFiring.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // EcranAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 136);
            this.Controls.Add(this.lblFiring);
            this.Controls.Add(this.lblCovering);
            this.Controls.Add(this.lblMoving);
            this.Controls.Add(this.btnFiring);
            this.Controls.Add(this.btnCovering);
            this.Controls.Add(this.btnMoving);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EcranAction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choisissez votre action";
            this.ttAide.SetToolTip(this, "Cliquez sur la croix pour annuler.");
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMoving;
        private System.Windows.Forms.Button btnCovering;
        private System.Windows.Forms.Button btnFiring;
        private System.Windows.Forms.Label lblMoving;
        private System.Windows.Forms.ToolTip ttAide;
        private System.Windows.Forms.Label lblCovering;
        private System.Windows.Forms.Label lblFiring;
    }
}