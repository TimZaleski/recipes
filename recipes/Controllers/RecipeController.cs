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
    public class RecipeController : Controller
    {
        private readonly RecipeRepository _repo;
        private readonly IngredientRepository _ingRepo;

        public RecipeController(RecipeRepository repo, IngredientRepository ingRepo)
        {
            _repo = repo;
            _ingRepo = ingRepo;
        }
        // GET: Recipe/GetAllRecipes    
        public ActionResult GetAllRecipes()
        {
            ModelState.Clear();
            return View(_repo.GetAllRecipes());
        }

        // GET: Recipe/{id}/GetIngredients    
        public ActionResult GetIngredients(Guid id)
        {
            ModelState.Clear();
            return View(_ingRepo.GetIngredientsByRecipeId(id));
        }

        // GET: Recipe/AddRecipe    
        public ActionResult AddRecipe()
        {
            return View();
        }

        // POST: Recipe/AddRecipe    
        [HttpPost]
        public ActionResult AddRecipe(Recipe rec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repo.AddRecipe(rec))
                    {
                        ViewBag.Message = "Recipe added successfully";
                        return RedirectToAction("GetAllRecipes");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/UpdateRecipe/{id}   
        public ActionResult UpdateRecipe(Guid id)
        {
            return View(_repo.GetAllRecipes().Find(Rec => Rec.id == id));

        }

        // POST: Recipe/UpdateRecipe/{id}
        [HttpPost]
        public ActionResult UpdateRecipe(Guid id, Recipe obj)
        {
            try
            {
                _repo.UpdateRecipe(obj);
                return RedirectToAction("GetAllRecipes");
            }
            catch
            {
                return View();
            }
        }

        // DELETE: Recipe/DeleteRecipe/{id}  
        public ActionResult DeleteRecipe(Guid id)
        {
            try
            {
                if (_repo.DeleteRecipe(id))
                {
                    ViewBag.AlertMsg = "Recipe deleted successfully";

                }
                return RedirectToAction("GetAllRecipes");
            }
            catch
            {
                return View();
            }
        }
    }
}
