using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models.DTO;
using RecipeMVC.Data;
using Domain.IRepositories;
using Domain.Repositories;
using System.IO;

namespace RecipeMVC.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesRepo recipesRepo;

        public RecipesController(IRecipesRepo recipesRepo)
        {
            this.recipesRepo = recipesRepo;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await recipesRepo.GetList();
                return View(model);
            }
            catch (Exception)
            {
                return View(null);
            }

        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipesDTO = await recipesRepo.Get((int)id);
            if (recipesDTO == null)
            {
                return NotFound();
            }

            return View(recipesDTO);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Set<CategoryDTO>(), "ID", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Image, ImageFile, Body,CategoryId")] RecipesDTO recipesDTO)
        {
            if (recipesDTO.ImageFile == null || recipesDTO.ImageFile.Length == 0)
                return View(recipesDTO);

            await convertToBase64Async(recipesDTO);
            //recipesDTO.Image = recipesDTO.ImageFile.FileName;

            if (ModelState.IsValid)
            {
                await recipesRepo.Add(recipesDTO);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Set<CategoryDTO>(), "ID", "Name", recipesDTO.CategoryId);
            return View(recipesDTO);
        }

        private static async Task convertToBase64Async(RecipesDTO recipesDTO)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", recipesDTO.ImageFile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await recipesDTO.ImageFile.CopyToAsync(stream);
            }
            var byteArray = await System.IO.File.ReadAllBytesAsync(path);
            recipesDTO.Image = Convert.ToBase64String(byteArray);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipesDTO = await recipesRepo.Get((int)id);
            if (recipesDTO == null)
            {
                return NotFound();
            }
           // ViewData["CategoryId"] = new SelectList(_context.Set<CategoryDTO>(), "ID", "Name", recipesDTO.CategoryId);
            return View(recipesDTO);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Image,Body,CategoryId")] RecipesDTO recipesDTO)
        {
            if (id != recipesDTO.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await recipesRepo.Update(recipesDTO);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (recipesDTO.ID == null)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Set<CategoryDTO>(), "ID", "Name", recipesDTO.CategoryId);
            return View(recipesDTO);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipesDTO = await recipesRepo.Get((int)id);
            if (recipesDTO == null)
            {
                return NotFound();
            }

            return View(recipesDTO);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await recipesRepo.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRow(int id)
        {
            try
            {
                await recipesRepo.Delete((int)id);
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
               // throw;
            }

        }
    }
}
