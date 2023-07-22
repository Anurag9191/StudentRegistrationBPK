using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationBPK.Models;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace StudentRegistrationBPK.Controllers
{
    public class StudentController : Controller
    {
        int count = 1;
        private readonly CredProDB_TRN1Context mvcDbContext;
        public StudentController(CredProDB_TRN1Context mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var students = new Student()
            {
                Id = count,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Dob = registerUser.Dob,
                Address = registerUser.Address,
                City = registerUser.City,
                Phone = registerUser.Phone,
                ParentName = registerUser.ParentName,
            };
            count++;
            await mvcDbContext.Students.AddAsync(students);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Login","Student");

        }
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var students = await mvcDbContext.Students.ToListAsync();
            return View(students);
        }

    }
}
