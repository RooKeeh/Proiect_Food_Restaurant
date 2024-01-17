using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Proiect.Models.RestaurantViewModels;

namespace Proiect.Controllers
{
    public class ChefsController : Controller
    {
        private readonly RestaurantContext _context;

        public ChefsController(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, int? foodID)
        {
            var viewModel = new ChefIndexData();
            viewModel.Chefs = await _context.Chefs
                .Include(i => i.CreatedFoodItems)
                .ThenInclude(i => i.Food)
                .ThenInclude(i => i.Orders)
                .ThenInclude(i => i.Client)
                .AsNoTracking()
                .OrderBy(i => i.ChefName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["ChefID"] = id.Value;
                Chef chef = viewModel.Chefs.FirstOrDefault(i => i.ID == id.Value);

                if (chef != null)
                {
                    viewModel.Foods = chef.CreatedFoodItems.Select(i => i.Food);
                }
            }

            if (foodID != null)
            {
                ViewData["FoodID"] = foodID.Value;
                viewModel.Orders = viewModel.Foods?.FirstOrDefault(i => i.ID == foodID)?.Orders;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ChefName")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs.Include(i => i.CreatedFoodItems).ThenInclude(i => i.Food).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if (chef == null)
            {
                return NotFound();
            }
            PopulateCreatedFoodData(chef);
            return View(chef);
        }

        private void PopulateCreatedFoodData(Chef chef)
        {
            var allFoods = _context.Foods;
            var createdFoods = new HashSet<int>(chef.CreatedFoodItems.Select(c => c.FoodID));
            var viewModel = new List<CreatedFoodData>();
            foreach (var food in allFoods)
            {
                viewModel.Add(new CreatedFoodData
                {
                    FoodID = food.ID,
                    Name = food.Name,
                    IsCreated = createdFoods.Contains(food.ID)
                });
            }
            ViewData["Foods"] = viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedFoods)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chefToUpdate = await _context.Chefs.Include(i => i.CreatedFoodItems).ThenInclude(i => i.Food).FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Chef>(chefToUpdate, "", i => i.ChefName))
            {
                UpdatePublishedFoods(selectedFoods, chefToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                    ModelState.AddModelError("", "Unable to save changes.");
                }
                return RedirectToAction(nameof(Index));
            }

            UpdatePublishedFoods(selectedFoods, chefToUpdate);

            PopulateCreatedFoodData(chefToUpdate);

            return View(chefToUpdate);
        }
        private void UpdatePublishedFoods(string[] selectedFoods, Chef chefToUpdate)
        {
            if (selectedFoods == null)
            {
                chefToUpdate.CreatedFoodItems = new List<CreatedFoodItem>();
                return;
            }
            var selectedFoodsHS = new HashSet<string>(selectedFoods);
            var createdFoods = new HashSet<int>
            (chefToUpdate.CreatedFoodItems.Select(c => c.Food.ID));
            foreach (var food in _context.Foods)
            {
                if (selectedFoodsHS.Contains(food.ID.ToString()))
                {
                    if (!createdFoods.Contains(food.ID))
                    {
                        chefToUpdate.CreatedFoodItems.Add(new CreatedFoodItem
                        {
                            ChefID =
                       chefToUpdate.ID,
                            FoodID = food.ID
                        });
                    }
                }
                else
                {
                    if (createdFoods.Contains(food.ID))
                    {
                        CreatedFoodItem foodToRemove = chefToUpdate.CreatedFoodItems.FirstOrDefault(i => i.FoodID == food.ID);
                        _context.Remove(foodToRemove);
                    }
                }
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef != null)
            {
                _context.Chefs.Remove(chef);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChefExists(int id)
        {
            return (_context.Chefs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
