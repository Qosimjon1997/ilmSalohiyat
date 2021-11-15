using ilmSalohiyat.Areas.Manager.ViewModels.Teacher;
using ilmSalohiyat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ilmSalohiyat.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeachersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _applicationDbContext.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _applicationDbContext.Teachers
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
    }
}
