using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Donnees;



namespace Gestionnaires
{
    /// <summary>
    /// Utilitaire statique de méthodes de recherche. Généralement, c'est la recherche principale qui est appelée, mais il existe aussi deux plus petites recherches avec des critères différents 
    /// La méthode principale Recherche se sert de RechercherParGenre et de RechercherParMotCle pour effectuer la recherche
    /// </summary>
    public abstract class URecherche 
    {
     
        /// <summary>
        /// Recherche les ensembles audio ayant un genre correspondant à celui passé en argument dans un ReadOnlyDictionary passé en argument
        /// </summary>
        /// <param name="GenreRecherche"> Genre recherché par l'utilisateur </param>
        /// <param name="discotheque"> ReadOnlyDictionary sur lequel doit agir la recherche par genre </param>
        /// <returns> Retourne un Dictionary correspondant à la recherche demandée dans le dictionnaire passé en argument </returns>
       public static Dictionary<EnsembleAudio, LinkedList<Piste>>  RechercherParGenre(EGenre GenreRecherche, ReadOnlyDictionary<EnsembleAudio,LinkedList<Piste>> discotheque)
       {
            return discotheque.Where(ensemble => ensemble.Key.Genre.Equals(GenreRecherche)).ToDictionary(x => x.Key, x=> x.Value) ;
       }
      
        /// <summary>
        /// Recherche dans un dictionnaire passé en argument, il faut que la recherche passée en argument soit présente dans : 
        /// le Titre de l'ensemble audio, le titre d'une des piste de l'ensemble audio, le nom de l'artiste d'un des morceaux de l'ensemble audio
        /// ou le nom de l'auteur d'un des podcasts de l'ensemble audio; afin qu'un ensemble audio soit accepté dans la recherche et qu'il soit ajouté,
        /// avec sa liste de piste correspondante, au dictionnaire de recherche.
        /// </summary>
        /// <param name="rech"> String contenant le mot-clé recherché par l'utilsiateur </param>
        /// <param name="Discotheque"> Dictionnaire sur lequel la recherche va s'effectuer </param>
        /// <returns> Retourne un dictionnaire contenant les albums qui correspondent aux critères de recherche </returns>
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

        /// <summary>
        /// Méthode de recherche qui permet de combiner recherche par mot-clé et par genre dans le ReadOnlyDictionary passé en paramètre
        /// </summary>
        /// <param name="rech"> Mot-clé recherché par l'utilisateur </param>
        /// <param name="GenreRecherche"> Genre recherché par l'utilisateur </param>
        /// <param name="discotheque"> ReadOnlyDictionary sur lequel la recherche doit agir </param>
        /// <returns> Retourne une ObservableCollection d'EnsembleAudio qui sera ensuite affichée sur la page principale de l'application </returns>
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
    }
}
