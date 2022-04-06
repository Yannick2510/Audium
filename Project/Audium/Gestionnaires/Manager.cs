using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Donnees;
using Newtonsoft.Json;

namespace Gestionnaires
{

    /// <summary>
    /// Classe Manager, gère toute l'appli et est le point d'entrée de notre façade. Le manage implémente INotifyPropertyChanged pour informer la vue du changements de certains éléments.
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        /// <summary>
        /// Dépendance vers le gestionnaire de persistance
        /// </summary>
        public IPersistanceManager Persistance { get; /*private*/ set; }






        /// <summary>
        /// FOnction de chargement des données, qui utilise une des stratégies de persistance.
        /// Trois éléments persistent : la médiathèque, la liste des favoris (qui est composée de références sur la médiathèque)
        /// et le Manager Profil, qui contient les éléments de l'utilisateur
        /// </summary>
        public void Charger()
        {
            var donnees = Persistance.ChargeDonnees();
            mediatheque = donnees.mediatheque;
            listeFavoris = donnees.listeFavoris;
            foreach(EnsembleAudio ensAudio in mediatheque.Keys)
            {
                listeClé.Add(ensAudio);
            }
            ManagerProfil = donnees.MP;
        }

        /// <summary>
        /// Appelle la fonction de sauvegarde de la stratégie actuelle
        /// </summary>
        public void Sauvegarder()
        {
            Persistance.SauvegardeDonnees(mediatheque, listeFavoris, ManagerProfil);
        }

        /// <summary>
        /// Propriétés qui gèrent les connections aux Managers
        /// </summary>
        public ManagerEnsembleSelect ManagerEnsemble;
        public ManagerProfil ManagerProfil;
        public ManagerPlayer ManagerPlayer;

        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) =>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

     

        /// <summary>
        /// Collection de Favoris en ReadOnlyCollection d'EnsembleAudio, une liste privée est encapsulée à l'interieur
        /// </summary>
        public ReadOnlyCollection<EnsembleAudio> ListeFavoris { get; private set; }
        [JsonProperty (IsReference = true)]
        private List<EnsembleAudio> listeFavoris;

       



        /// <summary>
        /// Genre sélectionné au moment du clique sur l'interface. Cette propriété permet de changer le résultat de la recherche de l'Utilitaire URecherche.
        /// </summary>
        public EGenre GenreSelect
        {
            get => genreSelect;
            set
            {
                if (genreSelect != value)
                {
                    genreSelect = value;
                    OnPropertyChanged(nameof(GenreSelect));
                    OnPropertyChanged(nameof(Mediatheque));
                }
            }
        }
        private EGenre genreSelect;



       
        /// <summary>
        /// Collection de tout les genres disponibles dans l'Enumeration EGenre
        /// </summary>
        public ReadOnlyCollection<EGenre> ListeGenres { get; private set; }
        private List<EGenre> listeGenres;

        /// <summary>
        /// Dictionnaire principal du Manager, Mediathque prend en clé les Enesmbles Audio et en Valeur des LinkedList de Piste. Ce dictionnaire n'est jamais réellement affiché dans la vue (il n'est pas observable)
        /// </summary>
        public ReadOnlyDictionary<EnsembleAudio, LinkedList<Piste>> Mediatheque { get; private set; }
        private Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque; 
       
        /// <summary>
        /// Liste Clé est une astuce qui permet d'afficher la liste des clés du dictionnaire, mais puisqu'elle est de type ObservableCollection, on peut actualiser son affichage automatiquement.
        /// </summary>
        public ObservableCollection<EnsembleAudio> ListeClé 
        {
            get => listeClé;
            set
            {
                listeClé = value;
                OnPropertyChanged(nameof(ListeClé));
            }
        }
        private ObservableCollection<EnsembleAudio> listeClé;

        /// <summary>
        /// Methode de d'initialisation du dictionnaire Mediatheque. Permet de l'initialiser au moment de la déserialisation
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        public void InitReadOnlyDictionary(StreamingContext sc = new StreamingContext())
        {
            Mediatheque = new ReadOnlyDictionary<EnsembleAudio, LinkedList<Piste>>(mediatheque);
        }

        /// <summary>
        /// Initialisation de la liste des favoris
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        public void InitReadOnlyList(StreamingContext sc = new StreamingContext())
        {
            ListeFavoris = new ReadOnlyCollection<EnsembleAudio>(listeFavoris);

        }


        /// <summary>
        /// Constructeur du Manager. Est appellé au moment de la construction de l'application. On lui passe un type de persistance en paramètre
        /// qui permet de savoir quelle stratégie utiliser pour la persistance.
        /// </summary>
        /// <param name="persistance"></param>
        public Manager(IPersistanceManager persistance)
        {
            //Attribution de la persistance
            Persistance = persistance;

            
            mediatheque = new Dictionary<EnsembleAudio, LinkedList<Piste>>();
            listeClé = new();
            listeFavoris = new List<EnsembleAudio>();

            ListeClé = new(listeClé);
           

            //Charger des données et initialisation des collections
            Charger();
            InitReadOnlyDictionary();
            InitReadOnlyList();

            //Construction du ManagerEnsemble
            ManagerEnsemble = new(mediatheque);

            //Construction du ManagerPlayer
            ManagerPlayer = new(mediatheque);

            //Teste pour vérifier si le ManagerProfil a bien été chargé, puisqu'il fait parti de la persistance, sinon on le crée maintenant.
            if(ManagerProfil==null)
            {
                ManagerProfil = new();
            }

            //Attribution de chaque type de genre à la liste de genre
            listeGenres = new List<EGenre>
            {
                EGenre.AUCUN,
                EGenre.BANDEORIGINALE,
                EGenre.BLUES,
                EGenre.CLASSIQUE,
                EGenre.HIPHOP,
                EGenre.JAZZ,
                EGenre.ROCK
            };

            ListeGenres = new ReadOnlyCollection<EGenre>(listeGenres);


            
            


            //Initialisation des propriétés de sélection.
            GenreSelect = EGenre.AUCUN;
            ManagerEnsemble.EnsembleSelect = mediatheque.FirstOrDefault().Key;
      


            

            
            
        }

        /// <summary>
        /// Fonction permettant de modifier l'état du booléen favori d'un EnsembleAudio, et de l'ajouter ou le retirer à la liste des favoris
        /// en fonction de cet état.
        /// </summary>
        /// <param name="ensembleAudio"></param>
        public void ModifierListeFavoris(EnsembleAudio ensembleAudio)
        {

            if (ensembleAudio.Favori == false)
            {
                listeFavoris.Add(ensembleAudio);

            }
            else
            {
                listeFavoris.Remove(ensembleAudio);
            }

            ListeFavoris = new ReadOnlyCollection<EnsembleAudio>(listeFavoris);
            OnPropertyChanged(nameof(ListeFavoris));
            ensembleAudio.ModifierFavori();
             
        }

        /// <summary>
        /// Ajoute un couple clé valeur au dictionnaire médiathèque, ainsi qu'à la liste des clés.
        /// </summary>
        /// <param name="NouvelEnsembleAudio"></param>
        /// <param name="NouvelleListePiste"></param>
        public void AjouterEnsemblePiste(EnsembleAudio NouvelEnsembleAudio, LinkedList<Piste>NouvelleListePiste)
        {
            mediatheque.Add(NouvelEnsembleAudio, NouvelleListePiste);
            listeClé?.Add(NouvelEnsembleAudio); //Si ajouter ensemble Piste est appelée avant la création de listeClé.
            //GenreSelect = Genre.GetString(EGenre.BANDEORIGINALE);
            //GenreSelect = Genre.GetString(EGenre.AUCUN);
            OnPropertyChanged(nameof(Mediatheque));
        }


        /// <summary>
        /// Methode qui permet de creer un ensemble Audio par défaut, vide, avec une image par défaut. Prend un paramètre un titre
        /// qui est saisi dans une popup de l'interface.
        /// </summary>
        /// <param name="titre"></param>
        /// <returns></returns>
        public EnsembleAudio CreerEnsembleAudio(string titre)
        {
            //Finir cette méthode avec un if( Key.Titre n'existe pas dans discothèque) sinon Titre = Titre + "(1);
            EnsembleAudio NouvelEnsembleAudio = new(titre, null, @"icondefault\default.png", EGenre.AUCUN, 0);
            return NouvelEnsembleAudio;
        }

        /// <summary>
        /// Suppression d'un Ensemble Audio, avec gestion des exceptions.
        /// </summary>
        /// <param name="EnsembleASuppr"></param>
        public void SupprimerEnsembleAudio(EnsembleAudio EnsembleASuppr)
        {
            try
            {
                mediatheque.Remove(EnsembleASuppr);
                ListeClé.Remove(EnsembleASuppr);
                if (EnsembleASuppr.Favori == true) //On vérifie si il faisait parti des favoris
                {
                    ModifierListeFavoris(EnsembleASuppr); //Dans ce cas là on appelle ModifierListeFav, mais un Remove aurait fait la même chose
                }
                if (Mediatheque.Count == 0 && ListeFavoris.Count == 0)
                {
                   
                  ManagerPlayer.EnsembleLu = null; //Mise à null des propriétés du lecteur, quand la médiathèque et la liste des fav est vide, pour qu'il ne s'affiche plus rien sur le lecteur
                    ManagerPlayer.Playlist = null;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

     



        public override string ToString()
        {
            return $"Médiathèque : {Mediatheque}";
        }

 

    }
}
