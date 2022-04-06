using System;
using Gestionnaires;
using Donnees;



namespace Test_ManagerProfil
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagerProfil profil = new();
            Console.WriteLine(profil);
            profil.ModifierParamètres("Indigo", "SuperChemin");
            profil.ModifierProfil("Dudu", "/img.png");
            Console.WriteLine(profil);
            EnsembleAudio RAM = new EnsembleAudio("RAM", "Daft Punk", "/img.png", EGenre.JAZZ,5);
            Console.WriteLine(profil);
        }
    }
}
