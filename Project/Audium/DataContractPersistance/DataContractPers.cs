using Donnees;
using Gestionnaires;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

using System.Xml;

namespace DataContractPersistance 
{
    /// <summary>
    /// Classe contenant la méthode de peristance XML, elle implemente IPersistanceManager pour utiliser les méthodes ChargeDonnees et SauvegardeDonnees
    /// </summary>
    public class DataContractPers : IPersistanceManager
    {
        /// <summary>
        /// Combine l'endroit où se trouve l'exécutable sur l'ordinateur de l'utilisateur et le chemin relatif du dossier de sauvegarde XML par rapport à l'exécutable
        /// </summary>
        public string FilePath  => Path.Combine(Directory.GetCurrentDirectory(), RelativePath);

        /// <summary>
        /// Nom du fichier de sauvegarde JSON
        /// </summary>
        public string FileName { get; set; } = "audium.xml";

        /// <summary>
        /// Chemin relatif du dossier du fichier de sauvegarde XML depuis l'endroit où se trouve l'exécutable
        /// </summary>
        public string RelativePath { get; set; } = "..\\XML";
        /// <summary>
        /// Permet d'obtenir le chemin du fichier de sauvegarde XML propre à l'ordinateur de l'utilisateur
        /// </summary>
        protected string PersFile => Path.Combine(FilePath, FileName);

        /// <summary>
        /// Serializer permettant d'écrire et de lire les objets dans le fichier XML
        /// </summary>
        protected XmlObjectSerializer Serializer = new DataContractSerializer(typeof(DataToPersist),
                                                                           new DataContractSerializerSettings()
                                                                           {
                                                                               PreserveObjectReferences = true
                                                                           });

       
        /// <summary>
        /// Méthode permettant de charger les données en XML
        /// </summary>
        /// <returns> Tuple contenant les différents éléments d'un DataToPersist </returns>
        public virtual (Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque, List<EnsembleAudio> listeFavoris, ManagerProfil MP) ChargeDonnees()
        {

            DataToPersist data = new DataToPersist();

            ///Si le fichier n'existe pas, alors on créé le repértoire, puis le fichier en écrivant dedans les différents éléments du DataToPersist vide
            if (!File.Exists(PersFile))
            {
                //throw new FileNotFoundException("the persistance file is missing");
                Directory.CreateDirectory(FilePath);

               
                    var settings = new XmlWriterSettings() { Indent = true };
                    using (TextWriter tw = File.CreateText(PersFile))
                    {
                        using (XmlWriter writer = XmlWriter.Create(tw, settings))
                        {
                            Serializer.WriteObject(writer, data);
                        }
                    }
                
            }

         


          
            ///On lit les objets dans le fichier de sauvegarde XML qu'on met dans un DataTo Persist
            using (Stream s = File.OpenRead(PersFile))
            {
                data = Serializer.ReadObject(s) as DataToPersist;
            }


            return (data.Mediatheque, data.ListeFav, data.MP);
        }
        /// <summary>
        /// Classe permettant de sauvegarder les données en XML
        /// </summary>
        /// <param name="mediatheque"> Dictionnaire correspondant aux albums avec en clés des ensembles audio et en valeurs des LinkedList de Piste </param>
        /// <param name="listeFavoris"> Liste des ensembles audio favoris </param>
        /// <param name="MP"> Manager profil contenant les informations du profil </param>
        public virtual void SauvegardeDonnees(Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque, List<EnsembleAudio> listeFavoris, ManagerProfil MP)
        {


            ///Si le fichier n'existe pas, alors on créé le repértoire, puis le fichier, s'il n'existait pas, en écrivant dedans les différents éléments 
            ///qu'on vient de mettre dans le DataToPersist
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            DataToPersist data = new DataToPersist();
            data.Mediatheque = mediatheque;
            data.ListeFav.AddRange(listeFavoris);
            data.MP = MP;

            var settings = new XmlWriterSettings() { Indent = true };
            using (TextWriter tw = File.CreateText(PersFile))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, settings))
                {
                    Serializer.WriteObject(writer, data);
                }
            }
        



        }
    }
}
