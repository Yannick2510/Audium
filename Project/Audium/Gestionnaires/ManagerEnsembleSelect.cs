using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Donnees;

namespace Gestionnaires
{
    /// <summary>
    /// Classe qui gère toutes les opérations sur un Ensemble Audio sélectionné, implémente également INotifyPropertyChanged
    /// </summary>
    public class ManagerEnsembleSelect : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// Ensemble Audio sélectionné, lorsque sa valeur change, on essaye automatiquement de récupérer ses clés associées dans la médiathèque pour les stocker
        /// dans listeSelect
        /// </summary>
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

        /// <summary>
        /// Piste sélectionnée dans la liste associé à l'Ensemble Select
        /// </summary>
        public Piste PisteSelect
        {
            get => pisteSelect;
            set
            {
                
            pisteSelect = value;
            OnPropertyChanged(nameof(PisteSelect));
                
            }
        }
        private Piste pisteSelect;

        public Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque;

        public ReadOnlyCollection<Piste> ListeSelect { get; set; }
        private LinkedList<Piste> listeSelect;

        /// <summary>
        /// Constructeur de Manager Ensemble Select, qui récupère la référence de la médiathèque de la part du Manager
        /// </summary>
        /// <param name="mediatheque"></param>
        public ManagerEnsembleSelect(Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque)
        {
            this.mediatheque = mediatheque;
           

            
        }


        /// <summary>
        /// Ajout d'un morceau à la liste sélectionnée (et donc à la mediatheque)
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="artiste"></param>
        /// <param name="chemin"></param>
        /// <returns></returns>
        public Morceau AjouterMorceau(string titre, string artiste, string chemin)
        {
            int i = 1;
          

           


            Morceau morceau= new(titre,artiste,chemin);
            //Si le titre existe déjà; on ajoute entre paranthèses le nombre de fois où il apparaît
            while (listeSelect.Contains(morceau))
            {
                morceau.Titre = $"{titre} ({i})";
                i++;
            }

            listeSelect.AddLast(morceau);
            mediatheque.TryGetValue(ensembleSelect, out listeSelect);
            ListeSelect = new ReadOnlyCollection<Piste>(listeSelect.ToList());
            OnPropertyChanged(nameof(ListeSelect));
            return morceau;
        }

        /// <summary>
        /// Même méthode pour ajouter station de radio
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="url"></param>
        public void AjouterStationRadio(string titre, string url)
        {
            int i = 1;

            

            StationRadio radio = new(titre,url);

            while (listeSelect.Contains(radio))
            {
                radio.Titre = $"{titre} ({i})";
                i++;
            }

            listeSelect.AddLast(radio);
            mediatheque.TryGetValue(ensembleSelect, out listeSelect);
            ListeSelect = new ReadOnlyCollection<Piste>(listeSelect.ToList());
            OnPropertyChanged(nameof(ListeSelect));
        }


        /// <summary>
        /// Même méthode pour ajouter podcast
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="description"></param>
        /// <param name="auteur"></param>
        /// <param name="chemin"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Podcast AjouterPodcast(string titre, string description, string auteur, string chemin, DateTime date)
        {
            int i = 1;

         

            Podcast podcast = new(titre, description, auteur, chemin,date);

            while (listeSelect.Contains(podcast))
            {
                podcast.Titre = $"{titre} ({i})";
                i++;
            }

            listeSelect.AddLast(podcast);
            mediatheque.TryGetValue(ensembleSelect, out listeSelect);
            ListeSelect = new ReadOnlyCollection<Piste>(listeSelect.ToList());
            OnPropertyChanged(nameof(ListeSelect));
            return podcast;
        }


        /// <summary>
        /// Suppression d'une piste de la liste Select (et donc de la mediatheque) avec gestion des exceptions
        /// </summary>
        /// <param name="pisteAsuppr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualisation de la liste Select, après chaque modification. Pourrait être évité en remplacant liste select par une observable collection
        /// </summary>
        public void ActualiserListe()
        {
            mediatheque.TryGetValue(ensembleSelect, out listeSelect);
            ListeSelect = new ReadOnlyCollection<Piste>(listeSelect.ToList());
            OnPropertyChanged(nameof(ListeSelect));
        }






    }
}
