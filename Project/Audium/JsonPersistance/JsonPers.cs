using Donnees;
using Gestionnaires;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using DataContractPersistance;
using Newtonsoft.Json.Serialization;

namespace JsonPersistance
{
    /// <summary>
    /// Classe contenant la méthode de peristance JSON, elle implemente IPersistanceManager pour utiliser les méthodes ChargeDonnees et SauvegardeDonnees
    /// </summary>
    public class JsonPers : IPersistanceManager
    {
        /// <summary>
        /// Combine l'endroit où se trouve l'exécutable sur l'ordinateur de l'utilisateur et le chemin relatif du dossier de sauvegarde JSON par rapport à l'exécutable
        /// </summary>
        public string FilePath => Path.Combine(Directory.GetCurrentDirectory(), RelativePath);

        /// <summary>
        /// Nom du fichier de sauvegarde JSON
        /// </summary>
        public string FileName { get; set; } = "audium.json";

        /// <summary>
        /// Chemin relatif du dossier du fichier de sauvegarde JSON depuis l'endroit où se trouve l'exécutable
        /// </summary>
        public string RelativePath { get; set; } = "..\\JSON";

        /// <summary>
        /// Permet d'obtenir le chemin du fichier de sauvegarde JSON propre à l'ordinateur de l'utilisateur
        /// </summary>
        protected string PersFile => Path.Combine(FilePath, FileName);

        /// <summary>
        /// Méthode permettant de charger les données depuis le fichier JSON
        /// </summary>
        /// <returns> Retourne le 3-uplet contenant le dictionnaire d'ensembles audio et de pistes, la liste des favoris et le manager profil </returns>
        public (Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque, List<EnsembleAudio> listeFavoris, ManagerProfil MP) ChargeDonnees()
        {
            /// Si le fichier de sauvegarde n'est pas trouvé (qu'il n'existe pas) alors on créé d'abord le dossier qui contiendra le fichier de sauvegarde
            /// Ensuite on créé un nouveau DataToPersist qui contiendra les éléments nécessaires à la sauvegarde.
            /// On créé le fichier de sauvegarde avec WriteAllText à l'endroit indiqué et on lui passe le SerializeObject avec le DataToPersist vide et les 
            /// paramètres de sauvegarde (on passe un contract resolver pour vérifier si les données sont des dictionnaires). 
            /// Cela permet d'avoir un fichier de sauvegarde contenant les différents éléments qui sont vides.
            /// Ensuite on retourne les éléments vide du DataToPersist pour sortir de la méthode
            if (!File.Exists(PersFile))
            {
                Directory.CreateDirectory(FilePath);
               
                    DataToPersist Vide = new();
                    var defaut = JsonConvert.SerializeObject(Vide, new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.All,
                        TypeNameHandling = TypeNameHandling.All,
                        Formatting = Formatting.Indented,
                        ContractResolver = new DictionaryAsArrayResolver()
                    });
                    File.WriteAllText(PersFile, defaut);


                    return (Vide.Mediatheque, Vide.ListeFav, Vide.MP);
                

            }

            /// Si le fichier existe alors on lit les informations qui sont dedans, on les deserialize avec les différents paramètres (garder les références, le type,
            /// le format et le resolver pour les dictionnaires)
            /// On retourne ensuite les éléments qui sont dans data et qui contiennent les éléments sauvegardés.
            var json = File.ReadAllText(PersFile);
            var data = JsonConvert.DeserializeObject<DataToPersist>(json, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = new DictionaryAsArrayResolver()
            });
            return (data.Mediatheque, data.ListeFav, data.MP);
        }
        /// <summary>
        /// Méthode permettant de sauvegarder les données en JSON
        /// </summary>
        /// <param name="mediatheque"> Dictionnaire d'ensembles audio et de pistes </param>
        /// <param name="listeFavoris"> Liste des ensembles audio favoris </param>
        /// <param name="MP"> Le Manager Profil </param>
        public void SauvegardeDonnees(Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque, List<EnsembleAudio> listeFavoris, ManagerProfil MP)
        {
            ///Si le chemin pour accéder au fichier n'existe pas, on le créé
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            ///On place les informations à sauvegarder dans un DataToPersist
            DataToPersist data = new();
            
            data.Mediatheque = mediatheque;
            data.ListeFav = listeFavoris;
            data.MP = MP;

            ///On serialize les données et on les écrit dans le fichier en prenant en compte les différents paramètres
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = new DictionaryAsArrayResolver()
            });
            File.WriteAllText(PersFile,json);
        }
    }
}
