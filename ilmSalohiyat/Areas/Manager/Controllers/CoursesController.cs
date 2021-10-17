using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ilmSalohiyat.Data;
using ilmSalohiyat.Models;
using Microsoft.AspNetCore.Hosting;
using ilmSalohiyat.Areas.Manager.ViewModels.Course;
using System.IO;
using System;

namespace ilmSalohiyat.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CoursesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            var courseViewModel = new CourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Comment = course.Comment,
                Price = course.Price,
                ExistingImage = course.ImageURL
            };
            if (course == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = ProcessUploadedFile(model);
                Course course = new Course
                {
                    Comment = model.Comment,
                    Name = model.Name,
                    Price = model.Price,
                    ImageURL = uniqueFilename
                };
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            var courseViewModel = new CourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Comment = course.Comment,
                Price = course.Price,
                ExistingImage = course.ImageURL
            };
            if (course == null)
            {
                return NotFound();
            }
            return View(courseViewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if(ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(model.Id);
                course.Comment = model.Comment;
                course.Name = model.Name;
                course.Price = model.Price;

                if(model.CoursePicture != null)
                {
                    if(model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    course.ImageURL = ProcessUploadedFile(model);
                }
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            var courseViewModel = new CourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Comment = course.Comment,
                Price = course.Price,
                ExistingImage = course.ImageURL
            };
            if (course == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            var CurrectImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", course.ImageURL);
            _context.Courses.Remove(course);
            if(await _context.SaveChangesAsync() > 0)
            {
                if(System.IO.File.Exists(CurrectImage))
                {
                    System.IO.File.Delete(CurrectImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(CourseViewModel model)
        {
            string uniqueFileName = null;

            if (model.CoursePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoursePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoursePicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
