using Donnees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestionnaires
{
    /// <summary>
    /// Classe permettant de gérer les pistes et ensembles lus par le lecteur audio
    /// </summary>
    public class ManagerPlayer : INotifyPropertyChanged
    {


        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// EnsembleAudio actuellement lu par le lecteur
        /// </summary>
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

        /// <summary>
        /// Liste correspondant aux pistes restant à lire, et qui seront enchainées par le lecteur, à la fin de la lecture d'une piste.
        /// </summary>
        public LinkedList<Piste> Playlist;


        public Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque;


        /// <summary>
        /// Index de la piste dans Playlist.
        /// </summary>
        public int MediaIndex;

        public ManagerPlayer(Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque)
        {
            this.mediatheque = mediatheque;



        }
    }
}
