using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Donnees;

namespace Gestionnaires
{
    public abstract class UTri
    {
        public static Dictionary<EnsembleAudio, LinkedList<Piste>> TrierParNbEcoute(Dictionary<EnsembleAudio, LinkedList<Piste>> Discotheque)
        {
            return Discotheque.OrderBy(x => x.Key.CmptEcoute).ToDictionary(x => x.Key, x => x.Value);
        }

        public static Dictionary<EnsembleAudio, LinkedList<Piste>> TrierParDatePlusRecent(Dictionary<EnsembleAudio, LinkedList<Piste>> Discotheque)
        {
            return Discotheque.OrderByDescending(x => x.Key.DateAjout).ToDictionary(x => x.Key, x => x.Value);
        }
        public static Dictionary<EnsembleAudio, LinkedList<Piste>> TrierParDatePlusAncien(Dictionary<EnsembleAudio, LinkedList<Piste>> Discotheque)
        {
            return Discotheque.OrderBy(x => x.Key.DateAjout).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
