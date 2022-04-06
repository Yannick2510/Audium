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

namespace Audium.userControls
{
    /// <summary>
    /// Logique d'interaction pour UCPochette.xaml
    /// </summary>
    public partial class UCPochette : UserControl
    {
        public UCPochette()
        {
            InitializeComponent();
        }

        //Utilisation d'une dependency property pour l'image de la pochette, ce qui permet d'ailleurs d'avoir une valeur de défaut
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageNameProperty =
            DependencyProperty.Register("ImageName", typeof(string), typeof(UCPochette), new PropertyMetadata(@"icondefault\default.png"));


        //Ces événements vont être reliés dans le xaml de main window, là où se trouve la balise du user control, à des fonctions bien précises dans le code behind de main window
        public event RoutedEventHandler BoutonLire;
        public event RoutedEventHandler CliquePochette;
        public event RoutedEventHandler CliqueFavori;


        //Plusieurs méthodes, qui peuvent être déclenchés selon les boutons cliqués sur le user control, et qui appellent des événements du user control

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            BoutonLire?.Invoke(sender, new RoutedEventArgs());
        }

        private void Canvas_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            CliquePochette?.Invoke(sender, e);
        }

        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            CliqueFavori?.Invoke(sender, e);
        }
    }
}
