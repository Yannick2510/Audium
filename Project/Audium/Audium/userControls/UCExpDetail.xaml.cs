using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gestionnaires;
using Donnees;
using System.Diagnostics;
using System.IO;

namespace Audium.userControls
{
    /// <summary>
    /// Logique d'interaction pour UCExpDetail.xaml
    /// </summary>
    public partial class UCExpDetail : UserControl


    {
        public Manager Mgr => (App.Current as App).LeManager;

        public ManagerEnsembleSelect MgrEnsemble => (App.Current as App).LeManager.ManagerEnsemble;

        string imagesource;
        string imageName;
        string oldimage;
        public UCExpDetail()
        {
            InitializeComponent();
            DataContext = this;
         

        }

        
        /// <summary>
        /// Méthode appelée lorsqu'on clique sur le bouton play d'une des pistes dans le detail.
        /// Appelle de manière très rudimentaire lire depuis dans main window, mais pourrait être remplacé par un système d'événements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lire_Exp(object sender, RoutedEventArgs e)
        {
            int index = Mgr.ManagerEnsemble.ListeSelect.IndexOf(((Button)sender).Tag as Piste);

            ((MainWindow)Application.Current.MainWindow).LireDepuis(index);
        }


        /// <summary>
        /// Méthode permettant de supprimer une piste sélectionnée dans la liste des pistes de l'Expander, mais également de la supprimer 
        /// en même temps dans la liste de lecture, pour que le lecteur ne se mette pas à lire une piste devenue inexistante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Piste(object sender, RoutedEventArgs e)
        {

            if (MgrEnsemble.EnsembleSelect == Mgr.ManagerPlayer.EnsembleLu) //On vérifie si on supprime une des pistes se trouvant dans la collection qui est en train d'être lue
            {
                Mgr.ManagerPlayer.Playlist.Remove((Piste)ListePiste.SelectedItem); //Si c'est le cas, on la supprime de la playlist
                StopLecteur?.Invoke(sender, new RoutedEventArgs()); //Et on stoppe le lecteur, avec un système d'événement qui va appeler la méthode correspondante dans Main Window
            }
            try
            {
                MgrEnsemble.SupprimerPiste((Piste)ListePiste.SelectedItem); 
            }
            catch (ArgumentException erreur)
            {
                Debug.WriteLine(erreur.Message);
            }
           

        }

       
        /// <summary>
        /// Méthode qui permet d'actualiser la liste des pistes à chaque fermeture du bouton de paramètre d'une pistee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupBox_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            Mgr.ManagerEnsemble.ActualiserListe(); 
        }


        /// <summary>
        /// Méthode permettant de récupérer le chemin d'un fichier audio mp3 ou wave uniquement, qui sera utilisé pour la lecture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = $"{Mgr.ManagerProfil.CheminBaseDonnees}";
            
            dialog.Filter = "Fichiers audio (*.mp3,*.wav)|*.mp3;*.wav";

            bool? result = dialog.ShowDialog();


            if (result == true)
            {
                ((Piste)((Button)sender).Tag).Source = dialog.FileName; //On récupère l'attribut source se trouvant dans la piste contenue dans le tag du bouton cliqué et on lui change sa valeur
            }

            Mgr.ManagerEnsemble.ActualiserListe();
        }


        /// <summary>
        /// Méthode qui permet d'actualiser la liste des pistes à chaque fermeture du popup de paramètre d'une piste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupBox_Closed_1(object sender, RoutedEventArgs e)
        {
            Mgr.ManagerEnsemble.ActualiserListe();
        }

        /// <summary>
        /// Méthode permettant d'ajouter une piste de type morceau, et même de l'ajouter en direct à la liste de lecture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterMorceau(object sender, RoutedEventArgs e)
        {
            Morceau morceau = MgrEnsemble.AjouterMorceau("Nouveau Morceau", "", ""); //On ajoute un morceau par défaut
           
            if (MgrEnsemble.EnsembleSelect == Mgr.ManagerPlayer.EnsembleLu) //On vérifie si l'ensemble audio que l'on est en train de modifier est aussi celui qui est en train d'être lu
            {
                Mgr.ManagerPlayer.Playlist.AddLast(morceau); //Dans ce cas là on l'ajoute également à la playlist
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter une piste de type podcast, et même de l'ajouter en direct à la liste de lecture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterPodcast(object sender, RoutedEventArgs e)
        {
            Podcast nouvPod = MgrEnsemble.AjouterPodcast("Nouveau Podcast", "", "", "", DateTime.Today);
            if (MgrEnsemble.EnsembleSelect == Mgr.ManagerPlayer.EnsembleLu) //On vérifie si l'ensemble audio que l'on est en train de modifier est aussi celui qui est en train d'être lu
            {
                Mgr.ManagerPlayer.Playlist.AddLast(nouvPod); //Dans ce cas là on l'ajoute également à la playlist
            }
        }


        /// <summary>
        /// Fonction qui permet de sauvegarder toutes les modifications apportées à l'ensemble sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sauvegarder(object sender, RoutedEventArgs e)
        {
           
            MgrEnsemble.EnsembleSelect.ModifierEnsemble(Titre_box.Text, Etoiles.Value, Description_box.Text, MgrEnsemble.EnsembleSelect.CheminImage, (EGenre)Combo_Genre.SelectedItem);
           
        }

        public event RoutedEventHandler CliqueFavori;

        /// <summary>
        /// Méthode qui appelle un Evénement qui permettra d'accéder à la fonction modifier favori dans main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            CliqueFavori?.Invoke(sender, e);
        }

        /// <summary>
        /// Méthode permettant d'importer une image pour la pochette de l'album
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg| .gif |.png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                oldimage = MgrEnsemble.EnsembleSelect.CheminImage;
                imagesource = dialog.FileName;
                Uri uri = new Uri(imagesource);
                //On change le nom de l'image qui vient d'être sélectionnée, et on utilise la date actuelle pour être sûr qu'elle soit unique
                imageName = $"{ DateTime.Now.ToString().Replace("/", "").Replace(":","")}.{uri.Segments.Last().Split(".")[1]}";

                //On l'importe dans le dossier img
                File.Copy(imagesource, @$"..\img\{imageName}",true);
               
                //On modifie dans Ensemble Select la valeur de chemin image pour voir le changement en direct
                MgrEnsemble.EnsembleSelect.ModifierImage(imageName);

             
            }
        }

        /// <summary>
        /// Méthode lancant le dialogue de confirmation de suppression d'Ensemble
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerEns(object sender, RoutedEventArgs e)
        {
            Dialog.IsOpen = true;
        }


        public event RoutedEventHandler StopLecteur;

        /// <summary>
        /// Méthode de suppression de l'ensemble audio sélectionné avec gestion de la playlist et visibilité de l'expander
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderSuppr(object sender, RoutedEventArgs e)
        {
            
            Mgr.SupprimerEnsembleAudio(Mgr.ManagerEnsemble.EnsembleSelect); //On supprime l'ensemble audio du dico
            try { 

                Mgr.ManagerEnsemble.EnsembleSelect = Mgr.Mediatheque.Last().Key; //On essaye de récupérer changer l'ensemble select en le dernier élément du dico

            }
            catch(Exception)
            {
                if (Mgr.Mediatheque.Count == 0 && Mgr.ListeFavoris.Count == 0) //Si on a une erreur, on revérifie tout de même le contenu du dico et de la liste de fav, mais si eux aussi sont vide 
                {
                    Visibility = Visibility.Collapsed; //Alors on bloque la visiblité de l'expander 
                    StopLecteur?.Invoke(sender, new RoutedEventArgs()); //On invoque un événement qui arrêtera le lecteur dans le code behind de main window
                }
            }
        }
    }
}
