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
using System.Diagnostics;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Donnees;
using Gestionnaires;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls.Primitives;
using Audium.userControls;

namespace Audium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

       
        // On récupère les managers depuis App.xaml
        public Manager Mgr => (App.Current as App).LeManager;
        public ManagerEnsembleSelect MgrEnsemble => (App.Current as App).LeManager.ManagerEnsemble;
        public ManagerPlayer MgrPlayer => (App.Current as App).LeManager.ManagerPlayer;

        /// <summary>
        /// Vérifie si le lecteur lit un fichier média
        /// </summary>
        bool isPlaying = false;

        /// <summary>
        /// Propriété qui servira à donner le timespan de la durée d'un fichier audio
        /// Sa valeur permettra d'avoir un min et un max pour les valeurs de la progressbar et sera utilisée chaque tick du timer pour donner la positon de la progress bar
        /// </summary>
        TimeSpan position;

        /// <summary>
        /// Minuteur qui gère le lecteur
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();

       
        /// <summary>
        /// Propriétés constituant la fil de caractères tapés dans la barre de recherche, qui sera utilisée par l'utilitaire de recherche
        /// </summary>
        public string Motcle
        {
            get => motcle;
            set
            {
            motcle = value;
                OnPropertyChanged(nameof(Motcle));
                Mgr.OnPropertyChanged(nameof(Mgr.Mediatheque));
            }
        }
        private string motcle;


        /// <summary>
        /// Constructeur de MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            (App.Current as App).InitTheme(); //Permet de récuperer le thème sauvegardé de l'interface, et de le changer une fois l'application construite, et pas pendant la construction de App.xaml
            DataContext = this;


            timer.Interval = TimeSpan.FromMilliseconds(1000); //Déclaration de la période du timer, toutes les opérations d'actualisation du timer se font toutes les 1000 ms
            timer.Tick += new EventHandler(tick); //Abonnement du timer à l'événement tick, qui sera exécuté à chaque fin de période
            timer.Start(); //Démarrage du timer
            Lecteur.MediaEnded += Media_Next; //Abonnement du lecteur à l'évenmement Media Next, qui sera appelé à chaque fin de lecture
            Lecteur.MediaOpened += Media_Opened; //Abonnement du lecteur à l'événement media opened
            Lecteur.MediaFailed += Media_Stopped; //Abonnement du lecteur à l'évenement media stopped
            Lecteur.Volume = 0.2;
            Recherche.KeyUp += Recherche_KeyUp; //Abonnement qui permet de lancer la recherche à chaque lettre tapée, pour fournir un résultat au fur et à mesure que l'utilisateur tape sa recherche

            




    }
        /// <summary>
        /// Appelle de la recherche a chaque nouvelle lettre entrée dans la barre de recherche.
        /// Cette méthode appelle la fonction recherche de l'utilitarie, prennant en compte le genre actuellement sélectionné
        /// et donne le résultat dans la liste des clés, pour avoir un affichage actualisé du résultat de recherche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recherche_KeyUp(object sender, KeyEventArgs e)
        {
            Motcle = Recherche.Text;
            Mgr.ListeClé = URecherche.Recherche(Motcle, Mgr.GenreSelect, Mgr.Mediatheque);
        }


        /// <summary>
        /// Méthode appeleée à chaque fin de période du timer. 
        /// Cette méthode a deux fonctions : actualiser la position de la progress bar de l'interface avec la position du lecteur dans le média qu'il lit
        /// et actualiser l'affichage du temps du lecteur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tick(object sender, EventArgs e) 
        {
            ProgressBar.Value = Lecteur.Position.TotalSeconds;
            TimerDisplay.Text = Lecteur.Position.ToString(@"mm\:ss");
        }

        /// <summary>
        /// Fonction qui permet de lancer l'explorateur windows à l'emplacement qu'il a mis par défaut pour sa base de données. 
        /// Si cet cet emplacement n'est pas spécifié, où qu'il n'est pas valide, il affiche alors l'accès rapide de l'explorateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFolderMusic(Object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", $"{Mgr.ManagerProfil.CheminBaseDonnees}");

        }
        /// <summary>
        /// Ouvre la fenêtre des paramètres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenParameters(Object sender, RoutedEventArgs e)
        {
            Parametres par = new Parametres();
            par.ShowDialog();
            
        }

        /// <summary>
        /// Ouvre la fenêtre du profil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProfile(Object sender, RoutedEventArgs e)
        {
            Profil pro = new Profil();
            pro.ShowDialog();
        }

        /// <summary>
        /// Ouvre la popup d'ajout de musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMusic(Object sender, RoutedEventArgs e)
        {
            Dialog.IsOpen = true;
        }

        /// <summary>
        /// Ouvre l'expander qui contient la partie detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenExp(Object sender, RoutedEventArgs e)
        {
            
            MainExp.IsExpanded = true;
            ListeFav.UnselectAll(); //Pour des soucis d'ergonomie, on déselectionne tout les éléments de listeFav, pour éviter des affichages incohérents
        }

    

      

        /// <summary>
        /// Méthode de lecture. Permet de lire un EnsembleAudio depuis sa première Piste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LireTout(object sender, RoutedEventArgs e)
        {
            //On met l'index de la lecture à zéro puisqu'on lit la première piste
            Mgr.ManagerPlayer.MediaIndex = 0;
            
            //On récupère l'ensembleAudio depuis le tag du bouton de lecture sur lequel on a cliqué, et cela change également le contenu de Mgr.Playlist avec les valeurs de l'ensemble audio
            Mgr.ManagerPlayer.EnsembleLu = ((Button)sender).Tag as EnsembleAudio;

            //Si la playlist est vide (c'est à dire que la clé ensemble audio n'a aucune piste en valeur)
            if(Mgr.ManagerPlayer.Playlist.Count==0)
            {
                Lecteur.Pause();
                Lecteur.Position = new TimeSpan(0, 0, 0, 0, 0);
                TitleDisplay.Text = Mgr.ManagerPlayer.EnsembleLu.Titre;
                return; 
            }

            //On récupère l'attribut source qui est un string du chemin d'un fichier multimédia, que l'on converti en Uri, pour pouvoir l'affecter au lecteur
            try
            {
                Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            //On affiche dans le lecteur le titre de la piste lue
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;

            //On lance la lecture 
            Lecteur.Play();
            //On change l'icone en pause 
            PlayPauseIcon.Kind = PackIconKind.Pause;
            isPlaying = true;
           
        }

        /// <summary>
        /// Méthode similaire à LireTout, mais permet de lire toute une playliste depuis n'importe quelle piste
        /// </summary>
        /// <param name="index"></param>
        public void LireDepuis(int index)
        {
            Mgr.ManagerPlayer.MediaIndex = index;


            Mgr.ManagerPlayer.EnsembleLu = MgrEnsemble.EnsembleSelect;

            //On récupère l'attribut source qui est un string du chemin d'un fichier multimédia, que l'on converti en Uri, pour pouvoir l'affecter au lecteur
            try
            {
                Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            //On affiche dans le lecteur le titre de la piste lue
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;

            //On lance la lecture 
            Lecteur.Play();
            //On change l'icone en pause 
            PlayPauseIcon.Kind = PackIconKind.Pause;
            isPlaying = true;

        }
        /// <summary>
        /// Cette méthode est appelée dès que la source du lecteur est modifiée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Opened(object sender, EventArgs e) 
        {
            //La valeur de positon s'adapte à la durée du fichier média actuellement en source
            position = Lecteur.NaturalDuration.TimeSpan;
            //Permet de calibrer le minimum et le maximum des valeurs de la progress bar
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = position.TotalSeconds;
        }

        /// <summary>
        /// Fonction permettant d'arrêter la lecture en cas d'erreur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Stopped(object sender, EventArgs e)
        {
            Lecteur.Stop();
        }

        /// <summary>
        /// Méthode qui contrôle l'état de pause et lecture du lecteur, et modifie l'icone du bouton principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPause(object sender, EventArgs e)
        {
            if (isPlaying) 
            {
                Lecteur.Pause();
                isPlaying = false;
                PlayPauseIcon.Kind = PackIconKind.Play;
            }
            else
            {
                Lecteur.Play();
                isPlaying = true;
                PlayPauseIcon.Kind = PackIconKind.Pause;
            }
        }


        /// <summary>
        /// Méthode appelée automatiquement à chaque fin de lecture de média, ou alors quand l'utilisateur clique sur le bouton fastforward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Next(object sender, EventArgs e)
        {
            //On teste que la playlist ne se soit pas vidée
            if(Mgr.ManagerPlayer.Playlist?.Count==0 || Mgr.ManagerPlayer.Playlist ==null)
            {
                return;
            }

            //Si possible, on incrémente la valeur de MediaIdex, sinon on stoppe la lecture, généralement parce qu'on est arrivé en fin de playlist
            if(Mgr.ManagerPlayer.MediaIndex < Mgr.ManagerPlayer.Playlist.Count-1)
            {
                Mgr.ManagerPlayer.MediaIndex++;
            }
            else if(Mgr.ManagerPlayer.MediaIndex ==Mgr.ManagerPlayer.Playlist.Count-1)
            {
                Lecteur.Stop();
                isPlaying = false;
                PlayPauseIcon.Kind = PackIconKind.Play;
                return;
            }
            Lecteur.Stop();
            //On change la source du lecteur avec la piste suivante récupérée dans la playlist avec l'incrémentation de MediaIndex
            Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source));
            //On reprend la lecture
            Lecteur.Play();
            
           
            isPlaying = true;
            PlayPauseIcon.Kind = PackIconKind.Pause;
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;

        }
      
        /// <summary>
        /// Méthode appelée lorsque l'utilisateur qui sur le bouton backward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Previous(object sender, EventArgs e)
        {
            //Teste si la playlist n'est pas null
            if (Mgr.ManagerPlayer.Playlist?.Count == 0 || Mgr.ManagerPlayer.Playlist == null)
            {
                return;
            }
            //Vérifie si on en est pas déjà au premier morceau
            if (Mgr.ManagerPlayer.MediaIndex > 0)
            {
                Mgr.ManagerPlayer.MediaIndex--;
            }
            
            Lecteur.Stop();
            //Changement de la source pour le morceau précédent
            Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source)); 
            Lecteur.Play();
            isPlaying = true;
            PlayPauseIcon.Kind = PackIconKind.Pause;
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;
        }
       
        /// <summary>
        /// Méthode appelée à chaque clique unique sur la barre de progression. Converti la position du clique en une position temporelle de lecture du média
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBarChanged(object sender, MouseButtonEventArgs e)
        {
           
            
            int pos = Convert.ToInt32(ProgressBar.Value);
            Lecteur.Position = new TimeSpan(0, 0, 0, pos, 0);
    
        }


        /// <summary>
        /// Méthode appelée lorsque l'utilisateur glisse et maintient appuyé son clique gauche sur la barre de progression
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBarValueSlided(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ProgressBar.IsMouseCaptureWithin)
            {
                int pos = Convert.ToInt32(ProgressBar.Value);
                Lecteur.Position = new TimeSpan(0, 0, 0, pos, 0);
            }
        }


        /// <summary>
        /// Méthode qui appelle la fonction du manager pour modifier les favoris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clique_Fav(object sender, RoutedEventArgs e)
        {
            Mgr.ModifierListeFavoris(((ToggleButton)sender).Tag as EnsembleAudio);
        }

        /// <summary>
        /// Méthode appelée lorsque l'utilisateur a validé le titre de son EnsembleAudio dans la popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderAjout(object sender, RoutedEventArgs e)
        {
           //On ajoute un ensembleAudio par défaut, avec le nom qui vient d'être saisi, et on l'ajoute avec une liste en valeur vide
            Mgr.AjouterEnsemblePiste(Mgr.CreerEnsembleAudio(TitreBlock.Text), new LinkedList<Piste>());
            if (Mgr.Mediatheque.Count == 1) //Teste si c'est le premier album ajouté
            {
                
                Mgr.ManagerEnsemble.EnsembleSelect = Mgr.Mediatheque.First().Key; //Permet d'attribuer une valeur à Ensemble Select, maintenant qu'il y au moins 1 élément dans le dico
                ExpanderDetail.Visibility = Visibility.Visible; //Permet de rendre visible le contenu de l'expander, maintenant qu'il y au moins un élément à afficher
            }
        }

        /// <summary>
        /// Méthode appelée par le clique d'un tag, qui lance une recherche par genre, mais en tenant compte des éventuels mot clés déjà entrés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagListClicked(object sender, MouseButtonEventArgs e)
        {
            MainExp.IsExpanded = false;
            Mgr.ListeClé = URecherche.Recherche(Motcle, Mgr.GenreSelect, Mgr.Mediatheque);
        }


        private void ExpanderDetail_StopLecteur(object sender, RoutedEventArgs e)
        {
            TitleDisplay.Text = null;
            TimerDisplay.Text = null;
            Lecteur.Stop();
            Lecteur.Source = null;
        }


        /// <summary>
        /// Méthode appelant la sauvegarde au moment de la fermeture de la fenêtre principale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Mgr.Sauvegarder();
        }


        /// <summary>
        /// Méthode appelée à la construction de la fenêtre, vérifie que tout les fichiers utiles sont bien présents, et nettoie ceux qui sont inutilisés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Vérifie si le fichier d'images de pochettes existe, sinon le crée
            DirectoryInfo directoryInfo = Directory.CreateDirectory(@"..\img\");

            //Supprime toutes les images qui ne sont pas actuellement utilisées par une pochette
            foreach(FileInfo file in directoryInfo.GetFiles()) 
            {
                if(!Mgr.Mediatheque.Keys.Any(x=>x.CheminImage == file.Name)) {
                    try { file.Delete(); }
                    catch(IOException exception) { Debug.WriteLine(exception.Message); }
                }
            }

            //Vérifie si le fichier d'images de photo de profil existe, sinon le crée
            DirectoryInfo directoryInfoPP = Directory.CreateDirectory(@"..\img\PP\");

            //Supprime toutes les images qui ne sont pas actuellement utilisées par la photo de profil
            foreach (FileInfo file in directoryInfoPP.GetFiles())
            {
                if (Mgr.ManagerProfil.CheminImage != file.Name)
                {
                    try { file.Delete(); }
                    catch (IOException exception) { Debug.WriteLine(exception.Message); }

                }
            }

            //Si la médiathèque est vide, on empêche l'affichage de l'expander

            if (Mgr.Mediatheque.Count == 0)
            {
                ExpanderDetail.Visibility = Visibility.Collapsed;
            }



        }
    }
}