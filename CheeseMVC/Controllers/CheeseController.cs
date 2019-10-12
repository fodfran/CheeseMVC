using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
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
            
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
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
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();

            //ViewBag.error = error;
            //error = "";
            return View(addCheeseViewModel);

        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            //Add new cheese to existing cheeses
            if (ModelState.IsValid)
            {
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Type = addCheeseViewModel.Type
                };

                CheeseData.AddCheese(newCheese);

                return Redirect("/Cheese");
            }

            
            /*
            Regex rgx = new Regex(@"^[A-Za-z ]+$");
            if (addCheeseViewModel.Name == null || rgx.IsMatch(addCheeseViewModel.Name) == false)
            {
                error = "Invalid Name";
                return Redirect("/Cheese/Add");
            }*/


            return View(addCheeseViewModel);
        }

        public IActionResult Edit(int cheeseId)
        {
            //ViewBag.cheese = CheeseData.GetByID(cheeseId);
            Cheese cheese = CheeseData.GetByID(cheeseId);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel
            {
                Name = cheese.Name,
                Description = cheese.Description,
                Type = cheese.Type,
                cheeseId = cheese.CheeseId
            };

            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        [Route("/Cheese/Edit")]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese cheeseEdit = CheeseData.GetByID(addEditCheeseViewModel.cheeseId);
                cheeseEdit.Name = addEditCheeseViewModel.Name;
                cheeseEdit.Description = addEditCheeseViewModel.Description;
                cheeseEdit.Type = addEditCheeseViewModel.Type;

                return Redirect("/Cheese");
            }

            return View(addEditCheeseViewModel);
        }

    }
}
