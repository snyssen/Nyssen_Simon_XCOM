using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Nyssen_Simon_XCOM
{
    public class SocComm : INotifyPropertyChanged
    {
        #region Données membres
        private Socket socServer, socClient;
        private byte[] socBuffer; // buffer de réception
        public readonly bool IsServer; // true => serveur | false => client

        private bool _IsConnected;
        private string _ReceivedMessage;
        #endregion
        /// <summary>
        /// Initie une nouvelle connexion
        /// </summary>
        /// <param name="_IsServer">Vrai pour serveur, faux pour client</param>
        /// <param name="ServerIP"> IP du serveur à joindre, nécessaire uniquement si on initie un client</param>
        /// <param name="port">Port à utiliser, défaut à 1337 si non précisé</param>
        public SocComm(bool _IsServer, string ServerIP = null, int port = 1337)
        {
            this.IsServer = _IsServer;
            if (IsServer) // Initialisation du serveur
            {
                if (!CreateServer(port))
                    throw new Exception("Le serveur n'a pas pu être créé");
            }
            else // Initialisation du client
            {
                if (string.IsNullOrWhiteSpace(ServerIP))
                    throw new Exception("IP du serveur non fournie !");
                if (!CreateClient(ServerIP, port))
                    throw new Exception("Impossible de se connecter au serveur");
            }
        }

        #region Server
        private bool CreateServer(int port)
        {
            IPAddress MachineIP;
            if ((MachineIP = ValidateAddress(Dns.GetHostName())) == null)
                return false;
            socServer = new Socket(AddressFamily.InterNetwork /* IPv4*/, SocketType.Stream, ProtocolType.Tcp);
            socServer.Bind(new IPEndPoint(MachineIP, port));
            socServer.Listen(1); // On accepte qu'un seul client dans la liste de connexion
            socServer.BeginAccept(new AsyncCallback(OnConnectionRequest), socServer);
            return true;
        }

        private void OnConnectionRequest(IAsyncResult iAR)
        {
            Socket Serv = (Socket)iAR.AsyncState; // Récupère le socket de serveur
            socClient = Serv.EndAccept(iAR); // Initialise le socket client pour pouvoir échanger des messages avec le client connecté
            IsConnected = true;
            BeginReception(socClient); // Se prépare à recevoir les données du client
        }
        #endregion
        #region Client
        private bool CreateClient(string ServerIP, int port)
        {
            IPAddress IPserver = null;
            if ((IPserver = ValidateAddress(ServerIP)) == null) // Validation de l'IP du serveur
                return false;
            try
            {
                socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { Blocking = false }; // Création du socket client
                socClient.BeginConnect(new IPEndPoint(IPserver, port), new AsyncCallback(OnConnection), socClient); // On initie la connexion (une fois établie on est renvoyé dans "OnConnection"
            }
            catch { return false; }
            return true;
        }
        private void OnConnection(IAsyncResult iAR)
        {
            Socket Client = (Socket)iAR.AsyncState; // récupère le socket client
            if (Client.Connected)
            {
                BeginReception(Client);
                IsConnected = true;
            }
            else
                throw new Exception("Le serveur ne répond pas");
        }
        #endregion
        #region Réception
        /// <summary>
        /// Prépare un socket à recevoir des données
        /// </summary>
        /// <param name="soc">Le socket qui s'occupe des transferts de données</param>
        private void BeginReception(Socket soc)
        {
            try
            {
                socBuffer = new byte[256]; // Initialisation du buffer de réception
                soc.BeginReceive(socBuffer, 0, socBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
            }
            catch { }
        }
        private void Reception(IAsyncResult iAR)
        {
            Socket soc = (Socket)iAR.AsyncState; // On récupère le socket chargé de réceptionné les données
            try
            {
                if (soc.EndReceive(iAR) > 0) // On a reçu des données
                {
                    ReceivedMessage = Encoding.UTF8.GetString(socBuffer);
                    BeginReception(soc);
                }
                else // On a reçu un message vide => Déconnexion
                {
                    soc.Disconnect(true);
                    soc.Close();
                    socServer.BeginAccept(new AsyncCallback(OnConnectionRequest), socServer);
                    socClient = null;
                    IsConnected = false;
                }
            }
            catch { }
        }
        #endregion
        /// <summary>
        /// Permet d'envoyer un message à l'autre terminal
        /// /!\ déclenche une exception si il n'y a pas de connexion
        /// </summary>
        /// <param name="Message">Message à envoyer</param>
        public void SendMessage(string Message)
        {
            if (!IsConnected || socClient == null)
                throw new Exception("Envoi d'un message impossible avant d'être connecté");
            socClient.Send(Encoding.UTF8.GetBytes(Message));
        }
        #region NotifyPropertyChanged
        // voir -> https://stackoverflow.com/questions/2246777/raise-an-event-whenever-a-propertys-value-changed
        public event PropertyChangedEventHandler PropertyChanged; // handler général
        public event EventHandler IsConnectedChanged; // handler pour IsConnected
        public event EventHandler ReceivedMessageChanged; // handler pour ReceivedMessage

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected void OnIsConnectedChanged(EventArgs e)
        {
            IsConnectedChanged?.Invoke(this, e);
        }
        protected void OnReceivedMessageChanged(EventArgs e)
        {
            ReceivedMessageChanged?.Invoke(this, e);
        }

        public bool IsConnected
        {
            get { return this._IsConnected; }
            set
            {
                if (value != this._IsConnected)
                {
                    this._IsConnected = value;
                    OnPropertyChanged();
                    OnIsConnectedChanged(EventArgs.Empty);
                }
            }
        }
        public string ReceivedMessage
        {
            get { return this._ReceivedMessage; }
            set
            {
                if (value != this._ReceivedMessage)
                {
                    this._ReceivedMessage = value;
                    OnPropertyChanged();
                    OnReceivedMessageChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        /// <summary>
        /// Valide une adresse IP ou un nom de domaine
        /// </summary>
        /// <param name="Host">Adresse IP ou nom de domaine</param>
        /// <returns>l'adresse IP liée (si valide)</returns>
        public static IPAddress ValidateAddress(string Host)
        {
            IPAddress IP = null;
            if (!string.IsNullOrWhiteSpace(Host))
            {
                IPAddress[] AssociatedIPs = Dns.GetHostEntry(Host).AddressList;
                for (int i = 0; i < AssociatedIPs.Length; i++)
                {
                    if (AssociatedIPs[i].AddressFamily == AddressFamily.InterNetwork /* Adresse est de type IPv4 */ && AssociatedIPs[i] != new IPAddress(0x0100007f) /* Adresse n'est pas localhost */)
                    {
                        PingReply ping = new Ping().Send(AssociatedIPs[i]);
                        if (ping.Status == IPStatus.Success) // Le ping a réussi
                        {
                            IP = AssociatedIPs[i];
                            break;
                        }
                    }
                }
            }
            return IP;
        }
    }
}
