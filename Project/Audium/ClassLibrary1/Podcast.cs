using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Donnees
{

    /// <summary>
    /// Classe Podcast, hérite de la classe Piste
    /// </summary>
    [DataContract]
    public class Podcast : Piste
    {

        /// <summary>
        /// Constructeur de Piste, transmet les propriétés titre et chemin à la Piste parente
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="description"></param>
        /// <param name="auteur"></param>
        /// <param name="chemin"></param>
        /// <param name="datedesortie"></param>
        public Podcast(string titre, string description, string auteur, string chemin, DateTime datedesortie)
            :base(titre,chemin)
        {
            DateDeSortie = datedesortie;
            Description = description;
            Auteur = auteur;
           
        }

        /// <summary>
        /// Date de sortie du podcast
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public DateTime DateDeSortie { get; set; }


        /// <summary>
        /// Description du podcast
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Description { get; set; }


        /// <summary>
        /// Auteur du podcast
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Auteur { get;  set; }
       
          
        /// <summary>
        /// Methode de modification du podcast
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="description"></param>
        /// <param name="chemin"></param>
        /// <param name="auteur"></param>
        /// <param name="dateDeSortie"></param>
        public void ModifierPodcast(string titre, string description, string chemin, string auteur, DateTime dateDeSortie)
        {
            base.Titre = titre;
            base.Source = chemin;
            DateDeSortie = dateDeSortie;
            Description = description;
            Auteur = auteur;
          
        }



        /// <summary>
        /// To String du podcast utilisé pour les tests et pour le debug
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"Podcast : \n Titre : {base.Titre}\n Auteur : {Auteur} \n DateDeSortie : {DateDeSortie} \n Description : {Description} \n Chemin : {base.Source} \n";
        }
    }
}
