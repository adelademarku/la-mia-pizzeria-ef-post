﻿using LaMiaPizzeriaF.Database;
using LaMiaPizzeriaF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LaMiaPizzeriaF.Controllers
{
    public class PizzeController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> listaDellePizze = db.Pizza.ToList<Pizza>();
                return View(listaDellePizze);
            }

        }

        public IActionResult Detail(int id)
        {

            using (PizzaContext db = new PizzaContext())
            {
                // LINQ: syntax methos
                Pizza postTrovato = db.Pizza
                    .Where(SingoloPostNelDb => SingoloPostNelDb.Id == id)
                    .FirstOrDefault();


                if (postTrovato != null)
                {
                    return View(postTrovato);
                }

                return NotFound("Il post con l'id cercato non esiste!");

            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Pizza postToUpdate = db.Pizza.Where(articolo => articolo.Id == formData.Id).FirstOrDefault();

                if (postToUpdate != null)
                {
                    postToUpdate.Name = formData.Name;
                    postToUpdate.Description = formData.Description;
                    postToUpdate.Image = formData.Image;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post che volevi modificare non è stato trovato!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToDelete = db.Pizza.Where(post => post.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizza.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post da eliminare non è stato trovato!");
                }
            }
        }


    }
}
