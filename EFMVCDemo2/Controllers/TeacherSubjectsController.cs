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
    public class TeacherSubjectsController : Controller
    {
        private readonly MVCContext _context;

        public TeacherSubjectsController(MVCContext context)
        {
            _context = context;
        }

        // GET: TeacherSubjects
        public async Task<IActionResult> Index()
        {
            var mVCContext = _context.TeacherSubject.Include(t => t.subject).Include(t => t.teacher);
            return View(await mVCContext.ToListAsync());
        }

        // GET: TeacherSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherSubject == null)
            {
                return NotFound();
            }

            var teacherSubject = await _context.TeacherSubject
                .Include(t => t.subject)
                .Include(t => t.teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherSubject == null)
            {
                return NotFound();
            }

            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: TeacherSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,SubjectId")] TeacherSubject teacherSubject)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(teacherSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", teacherSubject.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherSubject == null)
            {
                return NotFound();
            }

            var teacherSubject = await _context.TeacherSubject.FindAsync(id);
            if (teacherSubject == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", teacherSubject.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,SubjectId")] TeacherSubject teacherSubject)
        {
            if (id != teacherSubject.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(teacherSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherSubjectExists(teacherSubject.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", teacherSubject.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherSubject == null)
            {
                return NotFound();
            }

            var teacherSubject = await _context.TeacherSubject
                .Include(t => t.subject)
                .Include(t => t.teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherSubject == null)
            {
                return NotFound();
            }

            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherSubject == null)
            {
                return Problem("Entity set 'MVCContext.TeacherSubject'  is null.");
            }
            var teacherSubject = await _context.TeacherSubject.FindAsync(id);
            if (teacherSubject != null)
            {
                _context.TeacherSubject.Remove(teacherSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherSubjectExists(int id)
        {
          return (_context.TeacherSubject?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
