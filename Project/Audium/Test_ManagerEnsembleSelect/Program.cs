using System;
using System.Collections.Generic;
using Donnees;
using Gestionnaires;


namespace Test_ManagerEnsembleSelect
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Manager master = new Manager(new Stub.Stub());


            Console.WriteLine("Avant ajout :");
            foreach(Piste p in master.ManagerEnsemble.ListeSelect)
            {
                Console.WriteLine(p.Titre);
            }

            try
            {
                master.ManagerEnsemble.AjouterStationRadio("radio", "urlderadio");
                master.ManagerEnsemble.AjouterPodcast("podcastcool", "description chouette", "auteur bien", "chemin", DateTime.Now);
                master.ManagerEnsemble.AjouterMorceau("morceau cool", "artiste cool", "chemin");
                master.ManagerEnsemble.AjouterMorceau("morceau cool", "artiste cool", "chemin");
                master.ManagerEnsemble.AjouterMorceau("morceau cool", "artiste cool", "chemin");
                master.ManagerEnsemble.AjouterMorceau("    ", "artiste cool", "chemin"); //Cause une erreur
                
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("Après ajout :");
            foreach (Piste p in master.ManagerEnsemble.ListeSelect)
            {
                Console.WriteLine(p.Titre);
            }

            master.ManagerEnsemble.SupprimerPiste(master.ManagerEnsemble.ListeSelect[5]);
           



            Console.WriteLine("Après suppression :");
            foreach (Piste p in master.ManagerEnsemble.ListeSelect)
            {
                Console.WriteLine(p.Titre);
            }
        }
    }
}
