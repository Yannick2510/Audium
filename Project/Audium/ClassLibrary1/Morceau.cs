using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Donnees
{
    /// <summary>
    /// Classe Morceau, hérite de la Classe Piste
    /// </summary>
    [DataContract]
    public class Morceau : Piste
    {
        /// <summary>
        /// Constructeur de Morceau. Fait remonter le titre et le chemin à l'objet Piste parent.
        /// </summary>
        /// <param name="titre">Titre du morceau de type string</param>
        /// <param name="artiste">Titre de l'artiste de type string</param>
        /// <param name="chemin">Chemin du fichier multimédia du morceau de type string </param>
        public Morceau(string titre, string artiste, string chemin)
            :base(titre,chemin)
        {
           
            Artiste = artiste;
           
        }

        /// <summary>
        /// Artiste du Morceau
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Artiste { get; set; }
       
         
        /// <summary>
        /// Méthode de modification d'un morceau
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="chemin"></param>
        /// <param name="artiste"></param>
        public void ModifierMorceau(string titre, string chemin, string artiste)
        {
            base.Titre = titre;
            base.Source = chemin;
            Artiste = artiste;
        }


        /// <summary>
        /// To String utilisé dans le debug et les tests
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"Morceau : \n Titre : {base.Titre}\nArtiste : {Artiste} \n Chemin : {base.Source}";
        }
    }
}
