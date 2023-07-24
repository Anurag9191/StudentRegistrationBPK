using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using StudentRegistrationBPK.Models;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace StudentRegistrationBPK.Controllers
{
    
    public class StudentController : Controller
    {

        private readonly CredProDB_TRN1Context mvcDbContext;
        public StudentController(CredProDB_TRN1Context mvcDbContext) { 
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

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var student = mvcDbContext.Students.FirstOrDefault(student => student.Phone == login.Phone && login.Dob == student.Dob);
            if (student != null || VerifyDob((DateTime)login.Dob, (DateTime)student.Dob) && student.FirstName != null && student.ParentName!=null)
            {
                return View("Details", "Student");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Credentials");
            }
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

            var arr =  await mvcDbContext.Students.ToListAsync();
            int count = arr.Count+1;
            var students = new Student()
            {
                Id = count,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Dob = (DateTime)registerUser.Dob,
                Address = registerUser.Address,
                City = registerUser.City,
                Phone = registerUser.Phone,
                ParentName = registerUser.ParentName,
                Maths = registerUser.Maths, 
                Science = registerUser.Science, 
                Hindi = registerUser.Hindi, 
                SocialScience = registerUser.SocialScience, 
                English = registerUser.English
            };
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
        [HttpGet]
        public IActionResult Details(String FirstName, String parentName)
        {
            var student = mvcDbContext.Students.FirstOrDefault(student => student.FirstName == FirstName && parentName == student.ParentName);
            return View(student);
        }
        public Boolean VerifyDob(DateTime Logindate,DateTime ModelDate)
        {
           if(Logindate == ModelDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
