using System;
namespace CheeseMVC.Models
{
    public class Cheese
    {
        private static int nextCheeseId = 1;
        public int CheeseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Cheese(string name, string description)
        {
            CheeseId = nextCheeseId++;
            Name = name;
            Description = description;
        }

    }
}
