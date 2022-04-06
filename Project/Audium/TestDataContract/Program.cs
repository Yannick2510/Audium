using Gestionnaires;
using System;
using Stub;


namespace TestDataContract
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager LeManager = new Manager(new Stub.Stub());
            LeManager.Charger();
    
            LeManager.Persistance = new JsonPersistance.JsonPers();
            LeManager.Sauvegarder();
        
            Manager LeManager2 = new Manager(new JsonPersistance.JsonPers());
            LeManager2.Charger();
           
        



        }
    }
}
