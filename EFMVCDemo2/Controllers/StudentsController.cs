using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFMVCDemo2.Context;
using EFMVCDemo2.Models;
using Microsoft.EntityFrameworkCore.Storage;
using EFMVCDemo2.Dtos;

namespace EFMVCDemo2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MVCContext _context;

        public StudentsController(MVCContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'MVCContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMultistudent cms)
        {

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // add student 1
                    var student1 = new Student { StudentName = cms.StudentName1, StudentAddress = cms.StudentAddress1, StudentAge = cms.StudentAge1 };
                    _context.Students.Add(student1);
                    _context.SaveChanges();
                    await transaction.CreateSavepointAsync("insert student 1");
                    //add student 2
                    var student2 = new Student { StudentName = cms.StudentName2, StudentAddress = cms.StudentAddress2, StudentAge = cms.StudentAge2 };
                    _context.Students.Add(student2);
                    _context.SaveChanges();
                    await transaction.CreateSavepointAsync("insert student 2");
                    // add student 3
                    var student3 = new Student { StudentName = cms.StudentName3, StudentAddress = cms.StudentAddress3, StudentAge = cms.StudentAge3 };
                    _context.Students.Add(student3);
                    _context.SaveChanges();
                    await transaction.CreateSavepointAsync("insert student 3");
                    transaction.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint("insert student 1");
                }
            }
            return View();
        }


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,StudentAddress,StudentAge")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MVCContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
