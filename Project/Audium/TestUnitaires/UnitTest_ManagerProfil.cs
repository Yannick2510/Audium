using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Donnees;
using Gestionnaires;
using JsonPersistance;
using Xunit;

namespace TestUnitaires
{
    public class UnitTest_ManagerProfil
    {

        [Fact]
        public void TestCouleurDéfaut()
        {
            ManagerProfil profil = new();
            Assert.Equal("Blue", profil.CouleurTheme);
            

        }

        [Fact]
        public void TestModifParam()
        {
            ManagerProfil profil = new();
            profil.ModifierParamètres("Indigo", "SuperChemin");
            Assert.Equal("SuperChemin", profil.CheminBaseDonnees);
        }

        [Fact]
        public void TestModifProfil()
        {
            ManagerProfil profil = new();
            profil.ModifierProfil("Charles", "Image");
            Assert.Equal("Charles", profil.Nom);
        }

 

    }
}
