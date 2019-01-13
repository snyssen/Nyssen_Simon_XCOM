using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranMulti : Form
    {
        public SocComm Comm;

        public EcranMulti()
        {
            InitializeComponent();
        }

        #region Opération multithread sur la listbox
        delegate void ReturnToInsertMessage(string Message);
        private void InsertMessage(string Message)
        {
            if (this.LbMsg.InvokeRequired)
            {
                ReturnToInsertMessage d = new ReturnToInsertMessage(InsertMessage);
                this.Invoke(d, new object[] { Message });
            }
            else
                this.LbMsg.Items.Insert(0, Message);
        }
        #endregion

        private void BtnClient_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TbIP.Text))
                {
                    Comm = new SocComm(false, TbIP.Text, (int)NudPort.Value);
                    Comm.ReceivedMessageChanged += this.OnMessageReceived;
                    Comm.IsConnectedChanged += this.OnIsConnectedChanged;
                }
                else
                    MessageBox.Show("Veuillez entrer une adresse IP !");
            }
            catch (Exception ex) { MessageBox.Show("Une erreur a été rencontrée\n" + ex.Message);}
        }

        private void BtnServer_Click(object sender, EventArgs e)
        {
            try
            {
                Comm = new SocComm(true);
                Comm.ReceivedMessageChanged += this.OnMessageReceived;
                Comm.IsConnectedChanged += this.OnIsConnectedChanged;
                // On prévient l'utilisateur qu'il doit attendre, mais dans un thread pour ne pas bloquer l'application
                Task.Run(() => { MessageBox.Show("Veuillez attendre qu'un client se connecte\nVous serez notifié quand ce sera le cas..."); });
            }
            catch (Exception ex) { MessageBox.Show("Une erreur a été rencontrée\n" + ex.Message); }
        }

        private void BtnSendMsg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbMsg.Text))
            {
                Comm.SendMessage(TbMsg.Text);
                this.InsertMessage("Envoyé -> " + TbMsg.Text);
                TbMsg.Invoke((MethodInvoker)(() => { TbMsg.Text = ""; }));
            }
        }

        private void OnIsConnectedChanged(object sender, EventArgs e)
        {
            SocComm TmpComm = (SocComm)sender;
            if (TmpComm.IsConnected)
            {
                MessageBox.Show("Connexion établie !");
                if (TmpComm.IsServer)
                    this.Invoke((MethodInvoker)(() => { this.Text = "Multijoueur - joueur 1"; }));
                else
                    this.Invoke((MethodInvoker)(() => { this.Text = "Multijoueur - joueur 2"; }));
            }
            else
            {
                MessageBox.Show("La connexion a été interrompue !");
                this.Invoke((MethodInvoker)(() => { this.Text = "Multijoueur"; }));
            }
                
            BtnClient.Invoke((MethodInvoker)(() => { BtnClient.Enabled = !TmpComm.IsConnected; }));
            BtnServer.Invoke((MethodInvoker)(() => { BtnServer.Enabled = !TmpComm.IsConnected; }));
            BtnSendMsg.Invoke((MethodInvoker)(() => { BtnSendMsg.Enabled = TmpComm.IsConnected; }));
        }

        private void OnMessageReceived(object sender, EventArgs e)
        {
            SocComm TmpComm = (SocComm)sender;
            this.InsertMessage("Reçu -> " + TmpComm.ReceivedMessage);
        }

        private void EcranMulti_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Comm != null)
            {
                Comm.ReceivedMessageChanged -= this.OnMessageReceived;
                Comm.IsConnectedChanged -= this.OnIsConnectedChanged;
            }
        }
    }
}
