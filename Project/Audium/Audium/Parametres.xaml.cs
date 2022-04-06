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
using System.Windows.Shapes;
using Gestionnaires;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Forms;

namespace Audium
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public Manager Mgr => (App.Current as App).LeManager;

        public ManagerProfil MgrProfil => (App.Current as App).LeManager.ManagerProfil;

        public Parametres()
        {
            InitializeComponent();
            DataContext = this;
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
        /// Fonction fermant la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Liste des fonctions appelant dans App.xaml les fonctions de changement de thème en runtime, pourrait être revu avec un système d'événements

        private void AmberClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Amber();
        }

        private void BlueClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Blue();
        }
        private void BlueGreyClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).BlueGrey();
        }

        private void CyanClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Cyan();
        }

        private void DeepOrangeClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).DeepOrange();
        }
        private void DeepPurpleClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).DeepPurple();
        }
        private void GreenClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Green();
        }
        private void GreyClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Grey();
        }
        private void IndigoClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Indigo();
        }
        private void LightBlueClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).LightBlue();
        }
        private void LightGreenClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).LightGreen();
        }
        private void LimeClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Lime();
        }
        private void OrangeClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Orange();
        }
        private void PinkClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Pink();
        }
        private void PurpleClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Purple();
        }
        private void RedClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Red();
        }
        private void TealClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Teal();
        }
        private void YellowClick(Object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Yellow();
        }

        /// <summary>
        /// Méthode permettant de sélectionner un dossier (et non un fichier) et de récupérer son chemin 
        /// On utilise pour cela un FolderBrowserDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog browser = new();
            if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MgrProfil.CheminBaseDonnees = browser.SelectedPath;
            }
        }
    }
}
