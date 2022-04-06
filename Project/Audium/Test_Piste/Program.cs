using System;
using Donnees;

namespace Test_Piste
{
    class Program
    {
        static void Main(string[] args)
        {
         
            
            Piste NRJ = new StationRadio("NRJ12", "http://source");
            Console.WriteLine(NRJ);

            Morceau Musique = new Morceau("SUPER MUSIQUE","artiste génial","chemin");
            Console.WriteLine(Musique);

            Musique.ModifierMorceau("Nouveau titre", "nouveau chemin", "nouvel artiste");
            Console.WriteLine(Musique);
        }
    }
}
