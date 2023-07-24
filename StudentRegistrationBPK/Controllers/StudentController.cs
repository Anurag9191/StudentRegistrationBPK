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
            if (ModelState.IsValid)
            {
                var stu = mvcDbContext.Students.FirstOrDefault(stu => stu.Phone == login.Phone && stu.ParentName == login.ParentName);

                if(stu != null)
                {
                    return RedirectToAction("Details",new{id = stu.Id});
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials Please Try Again.");
                }

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
                Dob = registerUser.Dob,
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
        public IActionResult Details(int id)
        {
            var Stu = mvcDbContext.Students.FirstOrDefault(Stu => Stu.Id == id);
            return View(Stu);
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
