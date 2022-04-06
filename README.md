*application réalisée dans le cadre d'un projet de DUT informatique*



<!-- PROJECT LOGO -->
<br />
<p align="center">


  <h1 align="center">Audium</h1>

  <p align="center">
    <a href="https://github.com/othneildrew/Audium">
    <img src="disc-vinyl-icon_34448.png" alt="Logo" width="80" height="80">

  </a>
  <br />
  Gestionnaire de Discothèque et Plateforme d'écoute
    <br />
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table des matières</summary>
  <ol>
    <li>
      <a href="#A-propos">A Propos de notre Projet</a>
    </li>
    <li>
      <a href="#Utiliser-Audium">Utiliser Audium</a>
      <ul>
        <li><a href="#Prérequis">Prérequis</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#Elements-Importants">Elements Importants</a></li>
    <li><a href="#Ajouts-Personnels">Ajouts Personnels</a></li>
    <li><a href="#Documentation">Documentation</a></li>
    <li><a href="#Contacts">Contacts</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## A propos


https://user-images.githubusercontent.com/85686604/121777456-7dbaf400-cb92-11eb-9fdc-2a6cbfd4dcc7.mp4




Audium est une solution innovante dans le monde de la musique. 

Voici pourquoi:
* Une manière de ranger et gérer ses fichiers audio personels.
* Une station d'écoute autonome et complète
* Des possibilités de personnalisation infinies !


<!-- GETTING STARTED -->
## Utiliser Audium

Voici le guide pour installer et utiliser Audium

### Prérequis

Audium est dépendant du framework .Net Core 5.0. Il est nécessaire de le posséder pour exécuter l'application.
Il peut être installé [ici](https://dotnet.microsoft.com/download/dotnet/5.0).

* L'affichage pour des résolutions d'écrans inférieures à 1080p n'est pas pris en charge !

### Installation

1. Le set up se trouve [ici](https://github.com/GuillaumeAssailly/AudiumMusic/releases/tag/1.0).
2. Exécuter l'appli en mode Administrateur.

### QuickStart

Deux albums sont préchargés dans l'applications : 
* The Hypnoflip Invasion (2 pistes)
* 2001 : A Space Odyssey (1 pistes)

D'autres fichiers mp3 sont disponibles pour tester l'ajout de pistes et d'albums, elles se trouvent dans le dossier music, contenu dans le répertoire d'installation de l'application.



## Elements Importants 

* Dictionnaire de stockage de notre médiathèque :
  >Pour stocker des Albums et des Pistes, nous utilisons un système de Dictionnaire, avec en clé l'Album et en Valeur une liste de Pistes.
  ``` ReadOnlyDictionnary<EnsembleAudio, LinkedList<Piste>> mediatheque = new(); ```
  
  >Pour afficher les Pochettes des Albums, qui sont donc stockées dans les clés du dico, on utilise une ReadOnlyCollection ne contenant que les clés.
 ``` ObservableCollection<EnsembleAudio> listeClé = new(); ```
  
  
  
* Polymorphisme des Pistes : 
  > Pour permettre à notre utilisateur de stocker des Pistes de nature différentes, la classe Piste est abstraite, et nous utilisons le polymorphisme pour implémenter différents objets : Morceaux, Podcasts, Station de Radio, etc... C'est notamment très pratique, car cela permet de simplifier des méthodes : ici, supprimer une piste peut se faire quel que soit son type.
```csharp
  public bool SupprimerPiste(Piste pisteAsuppr)
        {
            if (pisteAsuppr == null)
            {
                throw new ArgumentException("La piste à supprimer est nulle");
            }
            if (!listeSelect.Contains(pisteAsuppr)) 
            {
                throw new ArgumentException("La piste en argument n'est pas contenue dans la liste");
            }
            if(!listeSelect.Remove(pisteAsuppr))
            {
                return false;
            }
            mediatheque.TryGetValue(ensembleSelect, out listeSelect);
            ListeSelect = new ReadOnlyCollection<Piste>(listeSelect.ToList());
            OnPropertyChanged(nameof(ListeSelect));
            return true;
        }
```

* Séparation d'EnsembleSelect et EnsembleLu
  >Dans cette application, nous devons mettre deux objets en évidence : un Album, lorsqu'il est sélectionné depuis la fenêtre d'affichage (on devra alors accéder à ce dernier et le placer dans un attribut nommé EnsembleSelect pour pouvoir opérer dessus) et un autre Album, qui lui est en train d'être lu (stocké dans EnsembleLu, on récupère ainsi ces propriétés de lectures). Puisque nous voulions permettre à l'utilisateur d'écouter une musique et de modifier les propriétés d'albums différents à la fois, nous avons fait ce choix de bien distinguer ces deux entités.
  
  >Avec notre système de dictionnaire, lorsque l'on récupère soit EnsembleSelect, soit EnsembleLu (qui sont des clés) il nous faut également accéder à leurs valeurs associés, et les remplir dans 2 listes qui leurs sont respectivement associés. Voici comment :
  * EnsembleSelect :
  ```csharp
   public EnsembleAudio EnsembleSelect
        {
            get => ensembleSelect;
            set
            {
                if (value != null)
                {
                    ensembleSelect = value;
                    mediatheque.TryGetValue(ensembleSelect, out listeSelect);
                    if (listeSelect != null) { ListeSelect = new ReadOnlyCollection<Piste>(listeSelect?.ToList()); OnPropertyChanged(nameof(ListeSelect)); }
                    OnPropertyChanged(nameof(EnsembleSelect));

                }
            }
        }
        private EnsembleAudio ensembleSelect;
  ```
  * EnsembleLu : 
  ```csharp
    public EnsembleAudio EnsembleLu
        {
            get => ensembleLu;
            set
            {
                if (ensembleLu != value)
                {
                    ensembleLu = value;
                    if (ensembleLu != null) { mediatheque.TryGetValue(ensembleLu, out Playlist); }
                    if (Playlist != null) { Playlist = new LinkedList<Piste>(Playlist.ToList()); }
                    OnPropertyChanged(nameof(EnsembleLu));
                    OnPropertyChanged(nameof(Playlist));
                }
            }
        }
        private EnsembleAudio ensembleLu;
  ```
## Ajouts Personnels

  ### Lecteur Audio :
  <p> Nous avons implémenté un lecteur audio autonome et fonctionnel. Ses méthodes se trouvent dans le code behind de la fenêtre principale, puisque que c'est là que se trouve   l'élément xaml MediaPlayer. Ce lecteur a plusieurs buts : </p>
  
  
    * Pouvoir lire des fichiers audios à partir d'un chemin.
    * Procurer des contrôles de bases, boutons play/pause, fast forward, backward, contrôle du volume
    * Enchainer la lecture des pistes dans l'ordre dans lequel elles sont disposées dans un album
    * Actualiser la liste de lecture à chaque ajout/suppression de piste.
  
  
  * Eléments XAML : 
  ```xaml
   <MediaElement Name="Lecteur" LoadedBehavior="Manual"/>
   <TextBlock Name="TimerDisplay" Foreground="White"/>
   <Slider IsMoveToPointEnabled="true" Name="ProgressBar"MouseLeftButtonUp="ProgressBarChanged"
     ValueChanged="ProgressBarValueSlided" Grid.Row="1" Grid.ColumnSpan="7" Margin="40,5"  Orientation="Horizontal"/>
  ```
  
  
  * Déclaration d'un Timer gérant l'affichage du temps, et la position de la barre de progression :
  ```csharp
            timer.Interval = TimeSpan.FromMilliseconds(1000); //Déclaration de la période du timer, toutes les opérations d'actualisation du timer se font toutes les 1000 ms
            timer.Tick += new EventHandler(tick); //Abonnement du timer à l'événement tick, qui sera exécuté à chaque fin de période
            timer.Start(); //Démarrage du timer
  ```
  * Détail de ce qu'il se passe à chaque tick du Timer :
  ```csharp
   void tick(object sender, EventArgs e) 
        {
            ProgressBar.Value = Lecteur.Position.TotalSeconds;
            TimerDisplay.Text = Lecteur.Position.ToString(@"mm\:ss");
        }
  ```
  * Fonction de Lecture depuis n'importe quelle Piste :
  ```csharp
  public void LireDepuis(int index)
        {
            Mgr.ManagerPlayer.MediaIndex = index; //Position de la piste dans la liste de l'album


            Mgr.ManagerPlayer.EnsembleLu = MgrEnsemble.EnsembleSelect;

            //On récupère l'attribut source qui est un string du chemin d'un fichier multimédia, que l'on converti en Uri, pour pouvoir l'affecter au lecteur
            Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source));
            //On affiche dans le lecteur le titre de la piste lue
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;

            //On lance la lecture 
            Lecteur.Play();
            //On change l'icone en pause 
            PlayPauseIcon.Kind = PackIconKind.Pause;
            isPlaying = true;

        }
  ```
  * Fonction d'enchainement de lecture, peut soit être commandée par le lecteur avec le bouton FastForward, ou alors est délenchée dans tout les cas à la fin de la lecture d'un titre
  ```csharp
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
            //On change la source du lecteur avec la piste suivante récupérée dans la playlist avec l incrémentation de MediaIndex
            Lecteur.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Mgr.ManagerPlayer.Playlist.ElementAt(Mgr.ManagerPlayer.MediaIndex).Source));
            //On reprend la lecture
            Lecteur.Play();
            
           
            isPlaying = true;
            PlayPauseIcon.Kind = PackIconKind.Pause;
            TitleDisplay.Text = Mgr.ManagerPlayer.Playlist.ElementAtOrDefault(Mgr.ManagerPlayer.MediaIndex).Titre;

        }
 ```
* A chaque chargement de piste dans le lecteur, on prépare la barre de progression :
```csharp                                                                              
   private void Media_Opened(object sender, EventArgs e) 
{
  //position correspond ici à la durée du fichier multimedia
  position = Lecteur.NaturalDuration.TimeSpan;
  //Permet de calibrer le minimum et le maximum des valeurs de la progress bar
  ProgressBar.Minimum = 0;
  ProgressBar.Maximum = position.TotalSeconds;
}
```
* Fonction permettant de changer la position sur la barre de progression :
```csharp
private void ProgressBarChanged(object sender, MouseButtonEventArgs e)
{
  int pos = Convert.ToInt32(ProgressBar.Value);
  Lecteur.Position = new TimeSpan(0, 0, 0, pos, 0);
}
```
### Utilitaire de Recherche
* Recherche par Genre :
```csharp 
public static Dictionary<EnsembleAudio, LinkedList<Piste>>  RechercherParGenre(EGenre GenreRecherche, ReadOnlyDictionary<EnsembleAudio,LinkedList<Piste>> discotheque)
{
  return discotheque.Where(ensemble => ensemble.Key.Genre.Equals(GenreRecherche)).ToDictionary(x => x.Key, x=> x.Value) ;
}
```
* Recherche par mots clé :
```csharp
   public static Dictionary<EnsembleAudio,LinkedList<Piste>> RechercherParMotCle(string rech, ReadOnlyDictionary<EnsembleAudio, LinkedList<Piste>> Discotheque)
        {
            Dictionary<EnsembleAudio, LinkedList<Piste>> Recherche = new();
            Recherche = Discotheque.Where(ensemble => (ensemble.Key.Titre.ToLower().Contains(rech.ToLower()))).ToDictionary(x=> x.Key, x=> x.Value);

            foreach (LinkedList<Piste> liste in Discotheque.Values)
            {
                foreach (Piste piste in liste)
                {
                    
                    if(Recherche.ContainsKey(Discotheque.FirstOrDefault(x => x.Value == liste).Key))
                    {
                        break;
                    }
                    if (piste.Titre.ToLower().Contains(rech.ToLower()))
                    {
                        Recherche.Add(Discotheque.FirstOrDefault(x => x.Value == liste).Key, liste);
                    }
                    else if (piste is Morceau && ((Morceau)piste).Artiste.ToLower().Contains(rech.ToLower()))
                    {
                        Recherche.Add(Discotheque.FirstOrDefault(x => x.Value == liste).Key, liste);
                    }
                    else if (piste is Podcast && ((Podcast)piste).Auteur.ToLower().Contains(rech.ToLower()))
                    {
                        Recherche.Add(Discotheque.FirstOrDefault(x => x.Value == liste).Key, liste);
                    }                
                }
            }
            return Recherche;
        }
 ```
 * Fonction de recherche principale 
 ```csharp
  public static ObservableCollection<EnsembleAudio> Recherche(string rech, EGenre GenreRecherche, ReadOnlyDictionary<EnsembleAudio, LinkedList<Piste>> discotheque)
        {
            ///Dictionnaire qui va contenir le résultat des recherche
            Dictionary<EnsembleAudio, LinkedList<Piste>> discothequeResultat;

            ///Si un genre est seletionné (ou plutôt un genre différent de AUCUN) alors on effectue d'abord la recherche par genre qu'on place dans le dictionnaire de résultat
            ///On effectue ensuite une recherche par mot-clé, si la recherche n'est pas nulle ou ne contient pas que des espaces, sur le dictionnaire de résultat
            ///qu'on doit passer en ReadOnlyDictionary car la méthode accepte seulement les ReadOnlyDictionary (pour pouvoir aussi accepter une recherche sur 
            ///l'élément discotheque qui est un ReadOnlyDictionary)
  
            ///discothequeResultat doit lui être un dictionnaire pour pouvoir être passé en ObservableCOllection par la suite
            if (GenreRecherche != EGenre.AUCUN)
            {
                discothequeResultat = URecherche.RechercherParGenre(GenreRecherche, discotheque);

                if (!string.IsNullOrWhiteSpace(rech))
                {
                    discothequeResultat = URecherche.RechercherParMotCle(rech, new ReadOnlyDictionary<EnsembleAudio, LinkedList<Piste>>(discothequeResultat));
                }
            }
            ///Si aucun genre n'est selectionné (ou plutôt le genre AUCUN) alors on effectue seulement une recherche par mot-clé, si celui ci n'est pas nul
            ///ou qu'il ne contient pas que des espaces
            else if (!string.IsNullOrWhiteSpace(rech))
            {
                discothequeResultat = URecherche.RechercherParMotCle(rech, discotheque);
            }
            ///Si aucun genre n'est selectionné et que la recherche est nulle alors on retourne une ObservableCollection correspondant aux clés du dictionnaire passé
            ///en paramètres
            else
            {
                return new ObservableCollection<EnsembleAudio>(discotheque.Keys.ToList());
            }
            ///On retourne une ObservableCollection contenant les clés du dictionnaire contenant le résultats de la/les recherche(s)
            return new ObservableCollection<EnsembleAudio>(discothequeResultat.Keys.ToList());
        }
```
  ### Gestion des images 
  > Les images importées (pochette et photo de profil) par l'utilisateur dans l'applications sont copiées dans un répertoire géré par l'appli. Elles sont renommées, et toutes les ressources inutilisées sont supprimées à chaque démarrage de l'appli.
  
## Documentation
  > Toute la documentation concernant le projet peut se trouver [ici](https://github.com/GuillaumeAssailly/Audium/tree/master/Documents)

## Contacts
  * Guillaume Assailly (guillaume.assailly@etu.uca.fr)
  * Yannick Boyer (yannick.boyer@etu.uca.fr)
                                                                                 
