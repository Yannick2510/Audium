using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Donnees
{
    /// <summary>
    /// Classe Station de Radio, hérite de Piste. En version 1.0 de Audium, le lecteur ne peut pas encore gérer les pistes de type Station de Radio, cette classe n'est donc pas fonctionnelle dans la vue
    /// </summary>
    [DataContract]
    public class StationRadio : Piste
    {

        /// <summary>
        /// Constructeur de Statio Radio, met l'état de l'émission à faux par défaut
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="URL"></param>
        public StationRadio(string titre, string URL)
            : base(titre, URL)
        {
            Emet = false;

        }


        /// <summary>
        /// Statut de l'émission de la station de radio
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool Emet { get; private set; }

        public void ModifierRadio(string titre, string URL)
        {
            base.Titre = titre;
            base.Source = URL;

        }
        /// <summary>
        /// ToString de la classe Station Radio
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"Radio : \n Titre : {base.Titre}\n AdresseURL : {base.Source} \n Emet : {Emet} \n";
        }
    }
}
