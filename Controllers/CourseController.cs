using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using courseLinkApp.Data;
using courseLinkApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace courseLinkApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameFull");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(new Course
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    TeacherId = course.TeacherId,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameFull");
            return View(course);
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(k => k.Teacher).ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context
            .Courses
            .Include(c => c.CourseRegistrations)
            .ThenInclude(cr => cr.Student)
            .Select(k => new CourseViewModel
            {
                CourseId = k.CourseId,
                CourseName = k.CourseName,
                TeacherId = k.TeacherId,
                CourseRegistrations = k.CourseRegistrations

            })
            .FirstOrDefaultAsync(k => k.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameFull");
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Course()
                    {
                        CourseId = course.CourseId,
                        CourseName = course.CourseName,
                        TeacherId = course.TeacherId

                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.CourseId == course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameFull");
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}