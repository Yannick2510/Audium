using System;
using System.Collections.Generic;
using Donnees;
using Gestionnaires;

namespace Test_UTri
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test de l'utilitaire de tri");
            Manager master = new();
            EnsembleAudio e1 = new EnsembleAudio("RAM", "Album de Doft Punk", "img.png", EGenre.CLASSIQUE,4);
            LinkedList<Piste> le1 = new();
            for (int i = 0; i < 10; i++)
            {
                le1.AddLast(new Morceau($"Titre {i}", "Daft Punk", "chemin"));
            }

            EnsembleAudio e2 = new EnsembleAudio("ROUM", "Album de Doft Punk", "img.png", EGenre.JAZZ,4);
            LinkedList<Piste> le2 = new();
            for (int i = 0; i < 10; i++)
            {
                le2.AddLast(new Piste($"test {i}"));
            }
            EnsembleAudio e3 = new EnsembleAudio("ROM", "Read Only Memory", "img.png", EGenre.JAZZ,4);
            LinkedList<Piste> le3 = new();
            for (int i = 0; i < 10; i++)
            {
                le2.AddLast(new Piste($"test {i}"));
            }
            master.AjouterEnsemblePiste(e1, le1);
            master.AjouterEnsemblePiste(e2, le2);
            master.AjouterEnsemblePiste(e3, le3);
            //Dictionary<EnsembleAudio, LinkedList<Piste>> rech = URecherche.RechercherParGenre(EGenre.JAZZ);
           // Dictionary <EnsembleAudio, LinkedList<Piste>> res = UTri.TrierParDatePlusRecent(rech);
            
            Console.WriteLine("Médiathèque triée par date d'ajout (le plus récent): ");
            /*
            foreach (KeyValuePair<EnsembleAudio, LinkedList<Piste>> cle in res)
            {
                Console.WriteLine($"Clé : {cle.Key} Valeur : {cle.Value}");
            }
            */

        }
    }
}
