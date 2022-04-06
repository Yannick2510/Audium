using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPersistance
{
    /// <summary>
    /// Classe permettant de gérer un dictionnaire afin de pouvoir le sauvegarder et le charger grâce à un array
    /// </summary>
    public class DictionaryAsArrayResolver : DefaultContractResolver
    {
        /// <summary>
        /// Créer un JsonArrayContract si le type de l'objet est un dictionnaire, permettant de le sauvegarder dans un fichier JSON, ou de le charger
        /// </summary>
        /// <param name="objectType"> Type de l'bjet qui est en train d'être chargé/sauvegardé </param>
        /// <returns> Retourne un JsonContract ou JsonArrayContract contenant les informations de l'objet passé en paramètres
        /// en fonction du type passé en paramètre </returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            if (IsDictionary(objectType))
            {
                JsonArrayContract contract = base.CreateArrayContract(objectType);
                contract.OverrideCreator = (args) => CreateInstance(objectType);
                return contract;
            }

            return base.CreateContract(objectType);
        }
        /// <summary>
        /// Vérifie si le type passé est un Dictionary
        /// </summary>
        /// <param name="objectType"> Type de l'objet en cours de traitement </param>
        /// <returns> Retourne true ou false en fonction du résultat </returns>
        internal static bool IsDictionary(Type objectType)
        {
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                return true;
            }

            if (objectType.GetInterface(typeof(IDictionary<,>).Name) != null)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Créer une instance de type dictionnaire
        /// </summary>
        /// <param name="objectType"> Type de l'objet en cours de traitement </param>
        /// <returns> Retourne l'instance créée </returns>
        private object CreateInstance(Type objectType)
        {
            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(objectType.GetGenericArguments());
            return Activator.CreateInstance(dictionaryType);
        }
    }
}
