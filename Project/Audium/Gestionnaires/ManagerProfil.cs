using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Donnees;

namespace Gestionnaires
{

    /// <summary>
    /// Classe qui gère toutes les paramètres et les données de l'utilisateur. Implémente également INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public class ManagerProfil : INotifyPropertyChanged
    {
        /// <summary>
        /// Implémentation de l'interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Constructeur du Manager Profil, qui permet d'avoir des valeurs par défaut à chaque attributs utilisateurs, au cas où il ne soit pas contenu dans un fichier de persistance
        /// </summary>
        public ManagerProfil()
        {
            Nom = "Nom d'utilisateur";
            CheminImage = @"..\icondefault\pp.png";
            CouleurTheme = "Blue";
            CheminBaseDonnees = null;
        }


        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        [DataMember]
        public string Nom
        {
            get => nom;
            set
            {
                if (nom != value)
                {
                    nom = value;
                    OnPropertyChanged(nameof(Nom)); 
                }
            }
        }
        private string nom;

        /// <summary>
        /// Chemin de la photo de profil de l'utilisateur
        /// </summary>
        [DataMember]
        public string CheminImage {
            get => cheminImage;
            set
            {
                if (cheminImage != value)
                {
                    cheminImage = value;
                    OnPropertyChanged(nameof(CheminImage));
                }
            }
        }
        private string cheminImage;



        /// <summary>
        /// Couleur du thème de l'interface
        /// </summary>
        [DataMember]
        public string CouleurTheme { get; private set; }


        /// <summary>
        /// Chemin de la base de données, c'est à dire le dossier par défaut qui s'ouvre au moment de l'import d'un fichier multimédia
        /// Généralement, un utilisateur stocke toutes ses musiques dans un même dossier (dont on récupère le chemin), qui peut contenir plusieurs sous-dossiers pour chaque artiste.
        /// </summary>
        [DataMember]
        public string CheminBaseDonnees { get => cheminBaseDonnees;
            set 
            {
                if(value != null)
                {
                    cheminBaseDonnees = value;
                    OnPropertyChanged(nameof(CheminBaseDonnees));
                }
            }
        }
        private string cheminBaseDonnees;
        

        
        
      
        /// <summary>
        /// Méthode de modification de deux éléments du profil, est compélementaire avec la méthode suivante, pour changer tout les attributs utilisateurs
        /// </summary>
        /// <param name="Nom"></param>
        /// <param name="CheminImage"></param>
        public void ModifierProfil(string Nom, string CheminImage)
        {
            this.Nom = Nom;
            this.CheminImage = CheminImage;
           
        }

        public void ModifierParamètres(string CouleurTheme, string CheminBaseDonnees)
        {
            this.CouleurTheme = CouleurTheme;
            this.CheminBaseDonnees = CheminBaseDonnees;
        }

        override
        public string ToString()
        {
            return $"Profil : \n Nom d'utilisateur : {Nom} \n Image : {CheminImage} \n Thème : {CouleurTheme} \n Chemin de la base de données : {CheminBaseDonnees} \n";
        }
    }
}
