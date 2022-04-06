using Donnees;
using Gestionnaires;
using Stub;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestUnitaires
{
    public class UnitTest_URecherche
    {
        [Fact]
        public void TestRecherche()
        { 
            Manager LeManager = new Manager(new Stub.Stub());
            LeManager.Charger();
            ObservableCollection<EnsembleAudio> res = new ObservableCollection<EnsembleAudio>(LeManager.Mediatheque.Keys.ToList());
            res = URecherche.Recherche("Random", EGenre.BANDEORIGINALE, LeManager.Mediatheque);
            foreach(EnsembleAudio e in res)
            {
                
                Assert.Equal(EGenre.BANDEORIGINALE.ToString(),e.Genre.ToString());
            }
        }
    }
}
