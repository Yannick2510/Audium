using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using System.Windows.Media;
using Gestionnaires;
using Donnees;
using DataContractPersistance;
using JsonPersistance;

namespace Audium
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        /// <summary>
        /// Manager de notre application
        /// </summary>
        public Manager LeManager { get; private set; }

        /// <summary>
        /// Constructeur de l'application
        /// </summary>
        public App()
        {
            LeManager = new(new JsonPers());
            //LeManager = new(new DataContractPers());
        }


        /// <summary>
        /// Méthode qui permet d'initialiser les thèmes de l'interface MD en fonction de la valeur sauvegardée dans le profil
        /// Puisque toutes les fonctions appelées pour changer la couleur ne fonctionnent que pendant le runtime, InitTheme est appelée juste après la construction de Main Window
        /// </summary>
        public void InitTheme()
        {
            switch (LeManager.ManagerProfil.CouleurTheme)
            {
                case "Amber": Amber(); break;
                case "Blue": Blue(); break;
                case "BlueGrey": BlueGrey(); break;
                case "Cyan": Cyan(); break;
                case "DeepOrange": DeepOrange(); break;
                case "DeepPurple": DeepPurple(); break;
                case "Green": Green(); break;
                case "Grey": Grey(); break;
                case "Indigo": Indigo(); break;
                case "LightBlue": LightBlue(); break;
                case "LightGreen": LightGreen(); break;
                case "Lime": Lime(); break;
                case "Orange": Orange(); break;
                case "Pink": Pink(); break;
                case "Purple": Purple(); break;
                case "Red": Red(); break;
                case "Teal": Teal(); break;
                case "Yellow": Yellow(); break;
                default: DeepOrange(); break;

            }
        }


        //Liste des fonctions qui permettent de changer  le thème de l'interface Material Design, en changeant également la valeur dans le Manager Profil

        public void Amber()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Amber];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Amber", LeManager.ManagerProfil.CheminBaseDonnees);
        }

        public void Blue()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Blue];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Blue", LeManager.ManagerProfil.CheminBaseDonnees);
        }

        public void BlueGrey()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.BlueGrey];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("BlueGrey", LeManager.ManagerProfil.CheminBaseDonnees);
        }

        public void Cyan()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Cyan];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Cyan", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void DeepOrange()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepOrange];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("DeepOrange", LeManager.ManagerProfil.CheminBaseDonnees);
        }

        public void DeepPurple()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("DeepPurple", LeManager.ManagerProfil.CheminBaseDonnees);
        }

        public void Green()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Green];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Green", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Grey()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Grey];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Grey", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Indigo()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Indigo];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Indigo", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void LightBlue()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.LightBlue];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("LightBlue", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void LightGreen()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.LightGreen];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("LightGreen", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Lime()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Lime", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Orange()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Orange];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Orange", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Pink()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Pink];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Pink", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Purple()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Purple];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Purple", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Red()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Red];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Red", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Teal()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Teal];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Teal", LeManager.ManagerProfil.CheminBaseDonnees);
        }
        public void Yellow()
        {
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Yellow];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignDarkTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);
            LeManager.ManagerProfil.ModifierParamètres("Yellow", LeManager.ManagerProfil.CheminBaseDonnees);
        }
    }


    
}
