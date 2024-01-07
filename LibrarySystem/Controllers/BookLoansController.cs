using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;
using System.Security.Claims;
using NuGet.Protocol.Plugins;
using System.Security.Principal;

namespace LibrarySystem.Controllers
{
    public class BookLoansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookLoansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookLoans
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.BookLoan
                .Include(b => b.Book);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoan
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // GET: BookLoans/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["UserId"] = userId;
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            return View();
        }

        // POST: BookLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,FromDate,ToDate")] BookLoan bookLoan)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _context.Add(bookLoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLoan.BookId);

            return View(bookLoan);
        }

        // GET: BookLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }


            var bookLoan = await _context.BookLoan
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookLoan == null)
            {
                return NotFound();
            }

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLoan.BookId);

            return View(bookLoan);
        }

        // POST: BookLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,FromDate,ToDate")] BookLoan bookLoan)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id != bookLoan.Id)
            {
                return NotFound();
            }

            if (bookLoan.UserId !=  userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookLoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookLoanExists(bookLoan.Id))
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

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLoan.BookId);

            return View(bookLoan);
        }

        // GET: BookLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoan
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // POST: BookLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookLoan = await _context.BookLoan
                .Where(b => b.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookLoan != null)
            {
                _context.BookLoan.Remove(bookLoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookLoanExists(int id)
        {
            return _context.BookLoan.Any(e => e.Id == id);
        }
    }
}
