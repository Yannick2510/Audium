using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Donnees;
using Gestionnaires;



namespace Test_URecherche
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager LeManager = new Manager(new Stub.Stub());
            LeManager.Charger();
            ObservableCollection<EnsembleAudio> res = new ObservableCollection<EnsembleAudio>(LeManager.Mediatheque.Keys.ToList());
            Console.WriteLine("Liste des albums : ");
            foreach(EnsembleAudio e in res)
            {
                Console.WriteLine(e);
            }
            
            res = URecherche.Recherche("Random", EGenre.BANDEORIGINALE, LeManager.Mediatheque);
            Console.WriteLine("Résultat de la recherche : ");
            foreach(EnsembleAudio e in res)
            {
                Console.WriteLine(e);
            }



        }
    }

}
