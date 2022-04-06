using System;
using System.Collections.Generic;
using Donnees;
using Gestionnaires;



namespace Test_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager master = new Manager(new Stub.Stub());
            EnsembleAudio RAM = new EnsembleAudio("RAM", "Daft Punk", "img/cool.png", EGenre.JAZZ,4);
            Piste p1 = new Morceau("Piste 1","artiste génial","source");
            Piste p2 = new Morceau("Piste 2","artiste génial","source");
            Piste p3 = new Morceau("Piste 3","artiste génial","source");
            Piste p4 = new Morceau("Piste 4","artiste génial","source");
            Piste p5 = new Morceau("Piste 5","artiste génial","source");
            Piste p6 = new StationRadio("Radio 6","source");
            Piste p7 = new Podcast("Podcast", "description", "auteur", "source", DateTime.Now);

            LinkedList<Piste> LP = new();
            LP.AddLast(p1);
            LP.AddLast(p2);
            LP.AddLast(p3);
            LP.AddLast(p4);
            LP.AddLast(p5);
            LP.AddLast(p6);
            LP.AddLast(p7);
            master.AjouterEnsemblePiste(RAM, LP);
            Console.WriteLine(master);



            EnsembleAudio test = master.CreerEnsembleAudio("HypnoFlip");
            master.AjouterEnsemblePiste(test, LP);
            EnsembleAudio test1 = master.CreerEnsembleAudio("HypnooFlip");
            master.AjouterEnsemblePiste(test1, LP);
            EnsembleAudio test2 = master.CreerEnsembleAudio("HypnoFlip");
            master.AjouterEnsemblePiste(test2, LP);
            EnsembleAudio test3 = master.CreerEnsembleAudio("HypnoFlip");
            master.AjouterEnsemblePiste(test3, LP);
            EnsembleAudio test4 = master.CreerEnsembleAudio("HypnoFlip");
            master.AjouterEnsemblePiste(test4, LP);

            Console.WriteLine(test);
            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            

        }
    }
}
