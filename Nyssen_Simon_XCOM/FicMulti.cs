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
                MessageBox.Show("Connexion établie !");
            else
                MessageBox.Show("La connexion a été interrompue !");
            BtnClient.Invoke((MethodInvoker)(() => { BtnClient.Enabled = !TmpComm.IsConnected; }));
            BtnServer.Invoke((MethodInvoker)(() => { BtnServer.Enabled = !TmpComm.IsConnected; }));
            BtnSendMsg.Invoke((MethodInvoker)(() => { BtnSendMsg.Enabled = TmpComm.IsConnected; }));
        }

        private void OnMessageReceived(object sender, EventArgs e)
        {
            SocComm TmpComm = (SocComm)sender;
            this.InsertMessage("Reçu -> " + TmpComm.ReceivedMessage);
            MessageBox.Show(TmpComm.ReceivedMessage);
        }
    }
}
