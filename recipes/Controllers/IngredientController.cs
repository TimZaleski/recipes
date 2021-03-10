using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using recipes.Repositories;

namespace recipes.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IngredientRepository _repo;

        public IngredientController(IngredientRepository repo)
        {
            _repo = repo;
        }
        // GET: Ingredient/GetAllIngredients    
        public ActionResult GetAllIngredients()
        {
            ModelState.Clear();
            return View(_repo.GetAllIngredients());
        }

        // GET: Ingredient/AddIngredient   
        public ActionResult AddIngredient(Recipe rec)
        {
            return View();
        }

        // POST: Ingredient/AddIngredient  
        [HttpPost]
        public ActionResult AddIngredient([FromBody] Ingredient ing)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repo.AddIngredient(ing))
                    {
                        ViewBag.Message = "Ingredient added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/UpdateIngredient/{id}   
        public ActionResult UpdateIngredient(Guid id)
        {
            return View(_repo.GetAllIngredients().Find(Ing => Ing.id == id));
        }

        // POST: Ingredient/UpdateIngredient/{id}    
        [HttpPost]

        public ActionResult UpdateIngredient(Guid id, Ingredient obj)
        {
            try
            {
                _repo.UpdateIngredient(obj);
                return RedirectToAction("GetIngredients", "Recipe", new { id = obj.recipeId });
            }
            catch
            {
                return View();
            }
        }

        // DELETE: Ingredient/DeleteIngredient/{id}  
        public ActionResult DeleteIngredient(Guid id)
        {
            try
            {
                Ingredient ing = _repo.GetIngredient(id);
                if (_repo.DeleteIngredient(id))
                {
                    ViewBag.AlertMsg = "Ingredient deleted successfully";

                }
                return RedirectToAction("GetIngredients", "Recipe", new { id = ing.recipeId });
            }
            catch
            {
                return View();
            }
        }
    }
}
