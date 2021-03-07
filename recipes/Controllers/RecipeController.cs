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

        // GET: Recipe/GetAllRecipes    
        public ActionResult GetAllRecipes()
        {

            RecipeRepository RecipeRepo = new RecipeRepository();
            ModelState.Clear();
            return View(RecipeRepo.GetAllRecipes());
        }

        // POST: Recipe/AddRecipe    
        [HttpPost]
        public ActionResult AddRecipe(Recipe rec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RecipeRepository RecipeRepo = new RecipeRepository();

                    if (RecipeRepo.AddRecipe(rec))
                    {
                        ViewBag.Message = "Recipe added successfully";
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
        public ActionResult EditRecipe(Guid id)
        {
            RecipeRepository RecipeRepo = new RecipeRepository();

            return View(RecipeRepo.GetAllRecipes().Find(Rec => Rec.id == id));

        }

        // POST: Recipe/UpdateRecipe/{id}    
        [HttpPost]

        public ActionResult UpdateRecipe(Guid id, Recipe obj)
        {
            try
            {
                RecipeRepository RecipeRepo = new RecipeRepository();

                RecipeRepo.UpdateRecipe(obj);
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
                RecipeRepository RecipeRepo = new RecipeRepository();
                if (RecipeRepo.DeleteRecipe(id))
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
