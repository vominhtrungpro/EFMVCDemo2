using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFMVCDemo2.Context;
using EFMVCDemo2.Models;

namespace EFMVCDemo2.Controllers
{
    public class StudentSubjectsController : Controller
    {
        private readonly MVCContext _context;

        public StudentSubjectsController(MVCContext context)
        {
            _context = context;
        }

        // GET: StudentSubjects
        public async Task<IActionResult> Index()
        {
            var mVCContext = _context.StudentSubjects.Include(s => s.student).Include(s => s.subject);
            return View(await mVCContext.ToListAsync());
        }

        // GET: StudentSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects
                .Include(s => s.student)
                .Include(s => s.subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: StudentSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,SubjectId")] StudentSubject studentSubject)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(studentSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //_context.Add(studentSubject);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,SubjectId")] StudentSubject studentSubject)
        {
            if (id != studentSubject.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(studentSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSubjectExists(studentSubject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects
                .Include(s => s.student)
                .Include(s => s.subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentSubjects == null)
            {
                return Problem("Entity set 'MVCContext.StudentSubjects'  is null.");
            }
            var studentSubject = await _context.StudentSubjects.FindAsync(id);
            if (studentSubject != null)
            {
                _context.StudentSubjects.Remove(studentSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSubjectExists(int id)
        {
          return (_context.StudentSubjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
