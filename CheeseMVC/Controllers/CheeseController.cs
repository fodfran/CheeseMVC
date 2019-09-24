using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();
        //private static List<string> Cheeses = new List<string>();
        static private string error;

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            ViewBag.Cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese")]
        public IActionResult Remove(string[] cheeses)
        {
            foreach (string cheese in cheeses)
            {
                Cheeses.Remove(cheese);
            }
            return Redirect("/Cheese");
        }

        public IActionResult Add()
        {
            ViewBag.error = error;
            return View();

        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            //Add new cheese to existing cheeses
            
            Regex rgx = new Regex(@"^[A-Za-z ]+$");
            if (name == null || rgx.IsMatch(name) == false)
            {
                error = "Invalid Name";
                return Redirect("/Cheese/Add");
            }

            Cheeses.Add(name, description);
            return Redirect("/Cheese");
        }


        /*public IActionResult Index2()
        {
            return View("Index");
        }*/
    }
}
