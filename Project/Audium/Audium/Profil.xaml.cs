using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Gestionnaires;


namespace Audium
{
    /// <summary>
    /// Logique d'interaction pour Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {
        string nombak;

        string cheminbak;


        string imageName;
        string imagesource;
        public Manager Mgr => (App.Current as App).LeManager;

        public ManagerProfil MgrProfil => (App.Current as App).LeManager.ManagerProfil;

        /// <summary>
        /// Constructeur de la fenêtre qui prépare un back up de toutes les propriétés susceptibles d'être changée si l'utilisateur annule ses choix
        /// </summary>
        public Profil()
        {
            InitializeComponent();
            DataContext = MgrProfil;
            nombak = MgrProfil.Nom;
            cheminbak = MgrProfil.CheminImage;
        }

        /// <summary>
        /// Petite méthode permettant de déplacer la fenêtre en glissant en maintenant appuyé le clique gauche
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }


        /// <summary>
        /// Fonction permettant de charger une image de profil depuis l'explorateur windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg| .gif |.png";
            
           

            bool? result = dialog.ShowDialog();

            if(result == true) 
            {
                
                theImage.ImageSource = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                
                imagesource = dialog.FileName;
                Uri uri = new Uri(imagesource);

                //On importe l'image dans le dossier img, et on change son nom pour être sûr qu'elle soit unique en utilisant la date actuelle
                imageName = $"{ DateTime.Now.ToString().Replace("/", "").Replace(":", "")}.{uri.Segments.Last().Split(".")[1]}";
                File.Copy(imagesource, @$"..\img\PP\{imageName}", true);
                MgrProfil.CheminImage = imageName;//On attribue temporairement à chemin image la nouvelle image ajoutée, pour pouvoir la voir en apperçue


            }
        }


        /// <summary>
        /// Sauvegarde de tout les éléments modifiés 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            MgrProfil.ModifierProfil(PseudoInput.Text, MgrProfil.CheminImage);
            this.Close();
        }


        /// <summary>
        /// Annulation des changements, et remet les valeurs de back up en place
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MgrProfil.ModifierProfil(nombak, cheminbak);
            this.Close();
        }
    }
}
