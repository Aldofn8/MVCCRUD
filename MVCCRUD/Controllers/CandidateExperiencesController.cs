using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly MvccrudContext _context;

        public CandidateExperiencesController(MvccrudContext context)
        {
            _context = context;
        }

        // GET: CandidateExperiences
        public async Task<IActionResult> Index()
        {
            var mvccrudContext = _context.CandidateExperiences.Include(c => c.IdCandidateNavigation);
            return View(await mvccrudContext.ToListAsync());
        }

        // GET: CandidateExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CandidateExperiences == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperiences
                .Include(c => c.IdCandidateNavigation)
                .FirstOrDefaultAsync(m => m.IdCandidateExperiences == id);
            if (candidateExperience == null)
            {
                return NotFound();
            }

            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Create
        public IActionResult Create()
        {
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate");
            return View();
        }

        // POST: CandidateExperiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidateExperiences,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidateExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CandidateExperiences == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperiences.FindAsync(id);
            if (candidateExperience == null)
            {
                return NotFound();
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // POST: CandidateExperiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidateExperiences,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            if (id != candidateExperience.IdCandidateExperiences)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExperienceExists(candidateExperience.IdCandidateExperiences))
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
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CandidateExperiences == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperiences
                .Include(c => c.IdCandidateNavigation)
                .FirstOrDefaultAsync(m => m.IdCandidateExperiences == id);
            if (candidateExperience == null)
            {
                return NotFound();
            }

            return View(candidateExperience);
        }

        // POST: CandidateExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CandidateExperiences == null)
            {
                return Problem("Entity set 'MvccrudContext.CandidateExperiences'  is null.");
            }
            var candidateExperience = await _context.CandidateExperiences.FindAsync(id);
            if (candidateExperience != null)
            {
                _context.CandidateExperiences.Remove(candidateExperience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExperienceExists(int id)
        {
          return (_context.CandidateExperiences?.Any(e => e.IdCandidateExperiences == id)).GetValueOrDefault();
        }
    }
}
