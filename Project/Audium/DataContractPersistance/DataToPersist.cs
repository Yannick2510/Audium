using Donnees;
using Gestionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContractPersistance
{
    /// <summary>
    /// Classe contenant les différents éléments à persister (sauvegarder/charger), un dictionnaire avec en clé des ensembles audio et en valeurs des linkedlist de Piste,
    /// Une liste d'ensembles audio favoris et un manager profil
    /// </summary>
    [DataContract]
    public class DataToPersist
    {

        [DataMember (Order=0)]
        public Dictionary<EnsembleAudio, LinkedList<Piste>> Mediatheque { get; set; } = new();


        [DataMember(Order = 1)]
        public List<EnsembleAudio> ListeFav { get; set; } = new();


        [DataMember(Order = 2)]
        public ManagerProfil MP { get; set; } = new();

      
    }
}
