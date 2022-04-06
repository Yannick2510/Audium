using Donnees;
using Gestionnaires;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Stub
{
    /// <summary>
    /// La classe Stub implémente IPersistanceManager, qui permet d'avoir des méthodes de sauvegarde et de chargement de données.
    /// Le Stub permet de faire comme si l'on avait enregistré des éléments depuis l'application et de les charger. 
    /// On ne l'utilise plus dans le projet final.
    /// </summary>
    public class Stub : IPersistanceManager
    {
        /// <summary>
        /// Méthode permettant de simuler un chargement de données
        /// </summary>
        /// <returns> Retourne le 3-uplet contenant le dictionnaire d'ensembles audio, la liste des favoris et le manager profil</returns>
        public (Dictionary<EnsembleAudio, LinkedList<Piste>>mediatheque, List<EnsembleAudio>listeFavoris, ManagerProfil MP) ChargeDonnees()
        {
            Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque = new();
            List<EnsembleAudio> listeFavoris = new();
            ManagerProfil MP = new ManagerProfil();
            MP.ModifierProfil("Nom par Défaut", null);
            MP.ModifierParamètres("Red", null);
            EnsembleAudio RAM = new EnsembleAudio("Random Access Memories", "Daft Punk", "ram.jpg", EGenre.BANDEORIGINALE, 3);
            LinkedList<Piste> LP1 = new();
            LP1.AddLast(new Morceau("Give Life Back to Music", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("The Game of Love", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            LP1.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            LP1.AddLast(new Podcast("Interview exclusive de Guy-Man", "Interview données à France Inter le 14 juin 2012", "France Inter", "", DateTime.Now));
            mediatheque.Add(RAM, LP1);



            EnsembleAudio HYP = new EnsembleAudio("The Hypnoflip Invasion", "Stipiflip", "hypnoflip.jpg", EGenre.HIPHOP, 5);
            LinkedList<Piste> LP2 = new();
            LP2.AddLast(new Morceau("Intro", "Stupeflip", ""));
            LP2.AddLast(new Morceau("Stupeflip Vite!", "Stupeflip", ""));
            LP2.AddLast(new Morceau("La Menuiserie", "Stupeflip", ""));
            mediatheque.Add(HYP, LP2);


            EnsembleAudio WAG = new EnsembleAudio("Wagner", "Stipiflip", "wagner.jpg", EGenre.CLASSIQUE, 2);
            LinkedList<Piste> LP3 = new();
            LP3.AddLast(new Morceau("Give Life Back to Music", "Daft Punk", "iafaf"));
            LP3.AddLast(new Morceau("The Game of Love", "Daft Punk", "iafaf"));
            LP3.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            mediatheque.Add(WAG, LP3);

            EnsembleAudio IAM = new EnsembleAudio("IAM", "Stipiflip", "iam.jpg", EGenre.HIPHOP, 3);
            LinkedList<Piste> LP4 = new();
            LP4.AddLast(new Morceau("Give Life Back to Music", "Daft Punk", "iafaf"));
            LP4.AddLast(new Morceau("The Game of Love", "Daft Punk", "iafaf"));
            LP4.AddLast(new Morceau("Giorgio by Moroder", "Daft Punk", "iafaf"));
            mediatheque.Add(IAM, LP4);

            RAM.ModifierFavori();
            HYP.ModifierFavori();
            listeFavoris.Add(RAM);
            listeFavoris.Add(HYP);

            return (mediatheque, listeFavoris, MP);

        }

        /// <summary>
        /// Simule une sauvegarde de données en affichant dans le Debug que la sauvegarde a été demandée
        /// </summary>
        /// <param name="mediatheque"> Dictionnaire d'ensembles audio </param>
        /// <param name="listeFavoris"> Liste des favoris </param>
        /// <param name="MP"> Le manager profil </param>
        public void SauvegardeDonnees(Dictionary<EnsembleAudio, LinkedList<Piste>> mediatheque, List<EnsembleAudio> listeFavoris, ManagerProfil MP)
        {
            Debug.WriteLine("Sauvegarde demandée");
        }

    }
}
