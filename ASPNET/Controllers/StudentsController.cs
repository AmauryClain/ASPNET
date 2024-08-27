using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ASPNET.Data;
using ASPNET.Models.ViewModels;
using ASPNET.Models.Entities;


namespace ASPNET.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                Phone = addStudentViewModel.Phone,
                Subscribed = addStudentViewModel.Subscribed,
            };

            await dbContext.Students.AddAsync(student);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Students");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
		}

        [HttpPost]
        public async Task<IActionResult> Edit(Student studentViewModel)
        {
            var student = await dbContext.Students.FindAsync(studentViewModel.Id);

            if(student is not null)
            {
                student.Name = studentViewModel.Name;
                student.Email = studentViewModel.Email;
                student.Phone = studentViewModel.Phone;
                student.Subscribed = studentViewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student studentViewModel)
        {
            var student = await dbContext.Students.FindAsync(studentViewModel.Id);
            
            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }
}
