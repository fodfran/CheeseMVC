using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        static private List<Cheese> Cheeses = new List<Cheese>();
        static private string error;

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            ViewBag.Cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        [Route("/Cheese")]
        public IActionResult Remove(int[] cheeses)
        {
            foreach (int id in cheeses)
            {
                CheeseData.RemoveCheese(id);
            }
            
            return Redirect("/Cheese");
        }

        public IActionResult Add()
        {
            ViewBag.error = error;
            error = "";
            return View();

        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese)
        {
            //Add new cheese to existing cheeses
           
            
            Regex rgx = new Regex(@"^[A-Za-z ]+$");
            if (newCheese.Name == null || rgx.IsMatch(newCheese.Name) == false)
            {
                error = "Invalid Name";
                return Redirect("/Cheese/Add");
            }
  
            
            CheeseData.AddCheese(newCheese);
            
            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetByID(cheeseId);
            ViewBag.error = error;
            error = "";
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Edit")]
        public IActionResult Edit(int cheeseId, string name, string description)
        {

            Regex rgx = new Regex(@"^[A-Za-z ]+$");
            if (name == null || rgx.IsMatch(name) == false)
            {
                error = "Invalid Name";
                return RedirectToAction("Edit", new { cheeseId = cheeseId});
            }

            Cheese cheeseEdit = CheeseData.GetByID(cheeseId);
            cheeseEdit.Name = name;
            cheeseEdit.Description = description;

            return Redirect("/Cheese");
        }

    }
}
