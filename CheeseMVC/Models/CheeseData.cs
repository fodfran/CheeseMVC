using System;
using System.Collections.Generic;
using System.Linq;

namespace CheeseMVC.Models
{
    public class CheeseData
    {
        static private List<Cheese> cheeses = new List<Cheese>();

        //getall
        public static List<Cheese> GetAll()
        {
            return cheeses;
        }

        //add
        public static void AddCheese(Cheese newCheese)
        {
            cheeses.Add(newCheese);
        }

        //remove
        public static void RemoveCheese(int id)
        {
            cheeses.Remove(GetByID(id));
        }

        //getbyid
        public static Cheese GetByID(int id)
        {
            return cheeses.Single(x => x.CheeseId == id);
        }
 
    }
}
