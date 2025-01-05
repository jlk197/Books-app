using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BookAppWeb.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IDaoMock dao;

        public ProducersController(BLC.BLC blc)
        {
            dao = blc.DAO;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            var producers = dao.GetAllProducers();
            return View(producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string Name, string Country, int EstablishmentYear)
        {
            IProducer prod = dao.CreateNewProducent();
            prod.Id = Id;
            prod.Name = Name;
            prod.Country = Country;
            prod.EstablishmentYear = EstablishmentYear;

            this.ModelState.Clear();
            this.TryValidateModel(prod);
            ModelState.Remove("Books");

            if (ModelState.IsValid)
            {
                dao.AddProducer(prod);
                dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, string Name, string Country, int EstablishmentYear)
        {
            IProducer prod = dao.GetAllProducers().FirstOrDefault(c => c.Id == Id);
            IProducer newProd = dao.CreateNewProducent();

            if (Id != prod.Id)
            {
                return NotFound();
            }

            newProd.Id = Id;
            newProd.Name = Name;
            newProd.Country = Country;
            newProd.EstablishmentYear = EstablishmentYear;
            
            this.ModelState.Clear();
            this.TryValidateModel(newProd);

            if (ModelState.IsValid)
            {
                prod.Name = newProd.Name;
                prod.Country = newProd.Country;
                prod.EstablishmentYear = newProd.EstablishmentYear;
                try
                {
                    dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!ProducerExists(prod.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(newProd);

        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            if (producer != null)
            {
                dao.RemoveProducer(producer);
            }

            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return dao.GetAllProducers().Any(e => e.Id == id);
        }
    }
}
