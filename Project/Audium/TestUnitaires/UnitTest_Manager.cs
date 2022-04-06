using Donnees;
using Gestionnaires;
using JsonPersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestUnitaires
{
    public class UnitTest_Manager
    {
        [Fact]
        public void TestAjouterFavoris()
        {
            Manager Mgr = new(new JsonPers());
            EnsembleAudio e = new EnsembleAudio("test", "un album test", "img.png", EGenre.JAZZ, 3);
            Mgr.ModifierListeFavoris(e);
            Assert.Single(Mgr.ListeFavoris);
            Assert.True(e.Favori);

        }

        [Fact]
        public void TestRetirerFavoris()
        {
            Manager Mgr = new(new JsonPers());
            EnsembleAudio e = new EnsembleAudio("test", "un album test", "img.png", EGenre.JAZZ, 3);
            Mgr.ModifierListeFavoris(e);
            Mgr.ModifierListeFavoris(e);
            Assert.Empty(Mgr.ListeFavoris);
            Assert.False(e.Favori);
        }
    }
}
