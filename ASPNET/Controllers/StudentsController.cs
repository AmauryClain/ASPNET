using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ASPNET.Data;
using ASPNET.Models.ViewModels;
using ASPNET.Models.Entities;
using ASPNET.Services;


namespace ASPNET.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return View(students);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]

        public async Task<IActionResult> GetStudentByIdAsync(Guid id)
        {
            var students = await _studentService.GetStudentByIdAsync(id);
            if (students is null)
            {
                return NotFound(); // Return a 404 if the student is not found
            }

            return View(students);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View("Add");
        }


        [HttpPost]

        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateStudentAsync(student);
                return RedirectToAction("List");
            }
            return View(student);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if(ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction("List");
            }
            return View(student);
        }

        [HttpGet]
        public async  Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			try
			{
				await _studentService.DeleteStudentAsync(id);
				return RedirectToAction("List");
			}
			catch (Exception)
			{
				return NotFound();
			}
		}

	}
}
