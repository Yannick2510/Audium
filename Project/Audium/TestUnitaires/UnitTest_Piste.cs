using System;
using Xunit;
using Donnees;

namespace TestUnitaires
{
    public class UnitTest_Piste
    {
        
        [Fact]
        public void TestStationRadio()
        {
            Piste NRJ = new StationRadio("NRJ12", "http://source");
            Assert.Equal("NRJ12", NRJ.Titre);
            Assert.Equal("http://source", NRJ.Source);

            
        }

        [Fact]
        public void TestMorceau()
        {
            Morceau Musique = new Morceau("SUPER MUSIQUE", "artiste génial", "chemin");
            Assert.Equal("artiste génial", Musique.Artiste);

            Musique.ModifierMorceau("Nouveau titre", "nouveau chemin", "nouvel artiste");
            Assert.Equal("nouveau chemin",Musique.Source);
        }

        [Fact]
        public void TestPodcast()
        {
            Podcast podcast = new Podcast("Podcast1", "podcast radio 18/05/2021","auteur", "chemin", DateTime.Now);
            Assert.Equal(2021.ToString(), podcast.DateDeSortie.Year.ToString());

            podcast.ModifierPodcast("Nouveau Podcast","nouvelle description", "nouveau chemin", "nouvel auteur", DateTime.Now);
            Assert.Equal("nouvel auteur", podcast.Auteur);
        }

    }
}
