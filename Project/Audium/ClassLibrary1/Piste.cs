using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Donnees
{

    /// <summary>
    /// Classe abstraite Piste qui sera implémentée par les objets Morceau, StationRadio et Podcast. Cet objet contient toutes les informations relatives aux pistes de lecture, dont les spécificités seront redéfinies plus loin.
    /// Cette classe implémente par ailleurs IEquatable<Piste> pour redéfinir son Equals, et son HashCode, à partir de sa propriété DateAjotu. 
    /// </summary>
    [DataContract]
    [KnownType(typeof(Morceau))]
    [KnownType(typeof(Podcast))]
    [KnownType(typeof(StationRadio))]
    public abstract class Piste : IEquatable<Piste>
    {

        /// <summary>
        /// Constructeur de Piste, appelé notamment pendant la construction de ses héritiers. La Propriété DateAjout est calculée au moment 
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="source"></param>
        public Piste(string titre, string source)
        {
            Titre = titre;
            DateAjout = DateTime.Now;
            Source = source;

        }

        /// <summary>
        /// Source multimédia de la piste
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Date d'ajout de la Piste
        /// </summary>
        [DataMember]
        private DateTime DateAjout { get; set; }


        /// <summary>
        /// Titre de la piste
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Titre { get; set; }


        /// <summary>
        /// To String d'une Piste utilisé uniquement dans certains tests
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"Piste : \n Titre : {Titre}\n ";
        }


        /// <summary>
        /// Rédéfinition du Equals, en utilisant la propriété DateAjout
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] Piste other)
        {
            return DateAjout.Equals(other.DateAjout);
        }



        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            if (GetType() != obj.GetType()) return false;
            return Equals(obj as Piste);
        }


        /// <summary>
        /// Redéfinition du HashCode en utilisant la propriété DateAjout
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return DateAjout.GetHashCode();
        }



    }
}
