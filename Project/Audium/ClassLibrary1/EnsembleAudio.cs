using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Donnees
{
    /// <summary>
    /// Classe qui contient toutes les données relatives à un EnsembleAudio, ainsi que les fonctions permettant d'agir dessus. Elle implémente
    /// IEquatable<EnsembleAudio> pour pouvoir redéfinir son HashCode et son Equals, mais également INotifyPropertyChanged pour mettre à jour la vue quand son état change
    /// </summary>
    [DataContract]
    public class EnsembleAudio : IEquatable<EnsembleAudio>, INotifyPropertyChanged
    {
        /// <summary>
        /// Constructeur d'Ensemble Audio. La Date d'ajout, qui permet de différencier les Ensembles Audio est déterminée à ce moment précis. Par défaut, l'Ensemble Audio n'est pas en favoris
        /// </summary>
        /// <param name="titre"> Titre de l'Ensemble de type string</param>
        /// <param name="description"> Description de l'Ensemble de type string</param>
        /// <param name="cheminImage"> Chemin de l'image de la couverture de type string</param>
        /// <param name="genre"> Genre de l'Ensemble du type Genre</param>
        /// <param name="note"> Note de l'Ensemble de type int</param>
        public EnsembleAudio(string titre, string description, string cheminImage, EGenre genre, int note)
        {

            Titre = titre;
            Description = description;
            CheminImage = cheminImage;
            Genre = genre;
            Note = note;
            DateAjout = DateTime.Now;
            CmptEcoute = 0;
            Favori = false;

            //Debug.WriteLine(DateAjout);
        }


        /// <summary>
        /// Titre de l'Ensemble
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Titre
        {
            get => titre;
            private set
            {
                titre = value;
                OnPropertyChanged(nameof(Titre));
            }
        }
        private string titre;


        /// <summary>
        /// Date d'ajout qui sert d'identifiant.
        /// </summary>
        [DataMember]
        public DateTime DateAjout { get; private set; }

        /// <summary>
        /// Note de l'Ensemble Audio
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int Note { get; set; }

        /// <summary>
        /// Description de l'Ensemble
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Description { get; private set; }

        /// <summary>
        /// Chemin de l'image de la pochette
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CheminImage
        {
            get => cheminImage;
            private set
            {
                cheminImage = value;
                OnPropertyChanged(nameof(CheminImage));
            }
        }
        private string cheminImage;

        /// <summary>
        /// Compteur d'écoute (pas exploité dans la version 1.0) 
        /// </summary>
        [DataMember]
        public int CmptEcoute { get; private set; }


        /// <summary>
        /// Etat de l'Ensemble Audio => si vrai il est en favori
        /// </summary>
        [DataMember]
        public bool Favori
        {
            get => favori;
            private set
            {
                favori = value;
                OnPropertyChanged(nameof(Favori));
            }
        }
        private bool favori;


        /// <summary>
        /// Genre de l'Ensemble Audio, qui est du type Enum EGenre
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public EGenre Genre { get; private set; }


        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// Méthode très rudimentaire qui permet de modifier l'EnsembleAudio
        /// </summary>
        /// <param name="Titre"></param>
        /// <param name="Note"></param>
        /// <param name="Description"></param>
        /// <param name="CheminImage"></param>
        /// <param name="Genre"> Doit être du typ EGenre</param>
        public void ModifierEnsemble(string Titre, int Note, string Description, string CheminImage, EGenre Genre)
        {
            this.Titre = Titre;
            this.Note = Note;
            this.Description = Description;
            this.CheminImage = CheminImage;
            this.Genre = Genre;
        }


        /// <summary>
        /// Modification de l'image (permet de modifier uniquement la pochette sans avoir à modifier tout les paramètres avec la méthode précédente
        /// </summary>
        /// <param name="imageSource"></param>
        public void ModifierImage(string imageSource)
        {
            CheminImage = imageSource;
        }


        /// <summary>
        /// Inverse l'état du favori
        /// </summary>
        public void ModifierFavori()
        {
            Favori = !Favori;
        }

        /// <summary>
        /// Redéfinition du Equal, en utilisant la Date d'ajout comme élément de comparaison.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>Re
        public bool Equals([AllowNull] EnsembleAudio other)
        {
            return DateAjout.Equals(other.DateAjout);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            if (GetType() != obj.GetType()) return false;
            return Equals(obj as EnsembleAudio);
        }


        /// <summary>
        /// Redéfinition du HashCode, en utilisant la valeur de la Date d'Ajout
        /// </summary>
        /// <returns>HashCode de l'objet </returns>
        public override int GetHashCode()
        {
            return DateAjout.GetHashCode();
        }

        /// <summary>
        /// To String utilisé en application Console et pour le Debug;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Titre : {Titre}\n Note (sur 5) : {Note}\n Description : {Description} \n Image : {CheminImage} \n Genre : {Genre} \n Date : {DateAjout}";
        }


    }
}
