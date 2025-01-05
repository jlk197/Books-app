using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interfaces;

namespace BookAppWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly IDaoMock dao;

        public BooksController(BLC.BLC blc)
        {
            dao = blc.DAO;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var Books = dao.GetAllBooks();
            return View(Books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = dao.GetAllBooks().FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string Title, int Genre, int PublicationYear, int ProducerId)
        {
            IBook book = dao.CreateNewBook();
            book.Id = Id;
            book.Title = Title;
            book.PublicationYear = PublicationYear;
            book.Genre = (GenreType)Genre;
            book.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);

            this.ModelState.Clear();
            this.TryValidateModel(book);
            
            if (ModelState.IsValid)
            {
                dao.AddBook(book);
                dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name"); 
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = dao.GetAllBooks().FirstOrDefault(b => b.Id ==id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name"); 
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, string Title, int Genre, int PublicationYear, int ProducerId) {
            IBook book = dao.GetAllBooks().FirstOrDefault(c => c.Id == Id);
            IBook newBook = dao.CreateNewBook();

            if (Id != book.Id) {
                return NotFound();
            }

            newBook.Id = Id;
            newBook.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);
            newBook.Title = Title;
            newBook.PublicationYear = PublicationYear;
            newBook.Genre = (GenreType)Genre;
            this.ModelState.Clear();
            this.TryValidateModel(newBook);

            if (ModelState.IsValid) {
                book.Title = newBook.Title;
                book.Genre = newBook.Genre;
                book.PublicationYear = newBook.PublicationYear;
                book.Producer = newBook.Producer;
                try {
                    dao.SaveChanges();
                } catch (Exception e) {
                    if (!BookExists(book.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");

            return View(newBook);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = dao.GetAllBooks().FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = dao.GetAllBooks().FirstOrDefault(m => m.Id == id);
            if (book != null)
            {
                dao.RemoveBook(book);
            }

            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return dao.GetAllBooks().Any(e => e.Id == id);
        }
    }
}
