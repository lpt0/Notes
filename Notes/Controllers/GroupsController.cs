/** GroupsController.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 6th, 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationContext _context;

        public GroupsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.Include(m => m.Notes).ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .Include(m => m.Notes) // include the notes related to this group
                .ThenInclude(n => n.User) // include the users as well
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                // include the notes, so the razor page can check if there are notes before deleting
                .Include(m => m.Notes) 
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //TODO: Error check on delete
            var @group = await _context.Groups.FindAsync(id);
            try
            {
                _context.Groups.Remove(@group);
                // since this waits (await) for the changes to save, it gets run synchronously
                await _context.SaveChangesAsync();
            }
            catch (SqlException)
            {
                /* Do nothing, just catch the error.
                 * Assuming one user uses the site at a time:
                 * A regular user would not reach this route during normal 
                 * usage.
                 * It's only if a user manually sends a POST or DELETE 
                 * request to this endpoint, could this error happen
                 * (notes that depend on this group)
                 * So, for the purposes of this lab, this error is ignored
                 * and the user gets sent back to the main group list.
                 * They would be able to see that the group was not deleted,
                 * and if they tried to delete it again, they could see that
                 * there are notes in this group, and it cannot be deleted.
                 */
            }
            // send the user back to the index page for groups
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
