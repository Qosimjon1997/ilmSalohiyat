using ilmSalohiyat.Areas.Manager.ViewModels.Teacher;
using ilmSalohiyat.Data;
using ilmSalohiyat.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ilmSalohiyat.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TeachersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        // GET: TeachersController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            var teacherViewModel = new TeacherViewModel()
            {
                Id = teacher.Id,
                Firstname = teacher.Firstname,
                Lastname = teacher.Lastname,
                Description = teacher.Description,
                ExistingImage = teacher.ImageURL
            };
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacherViewModel);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = ProcessUploadedFile(model);
                Teacher teacher = new Teacher
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Description = model.Description,
                    ImageURL = uniqueFilename
                };
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: v/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            var teacherViewModel = new TeacherViewModel()
            {
                Id = teacher.Id,
                Firstname = teacher.Firstname,
                Lastname = teacher.Lastname,
                Description = teacher.Description,
                ExistingImage = teacher.ImageURL
            };
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacherViewModel);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _context.Teachers.FindAsync(model.Id);
                teacher.Firstname = model.Firstname;
                teacher.Lastname = model.Lastname;
                teacher.Description = model.Description;

                if (model.TeacherPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    teacher.ImageURL = ProcessUploadedFile(model);
                }
                _context.Update(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            var teacherViewModel = new TeacherViewModel()
            {
                Id = teacher.Id,
                Firstname = teacher.Firstname,
                Lastname = teacher.Lastname,
                Description = teacher.Description,
                ExistingImage = teacher.ImageURL
            };

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacherViewModel);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            var TeacherImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", teacher.ImageURL);
            _context.Teachers.Remove(teacher);
            if (await _context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(TeacherImage))
                {
                    System.IO.File.Delete(TeacherImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(TeacherViewModel model)
        {
            string uniqueFileName = null;

            if (model.TeacherPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.TeacherPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.TeacherPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
