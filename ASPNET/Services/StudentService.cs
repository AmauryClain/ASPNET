
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ASPNET.Data;
using ASPNET.Models.ViewModels;
using ASPNET.Models.Entities;
using ASPNET.Services;

namespace ASPNET.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null)
            {
                throw new Exception($"Student with ID {id} was not found."); // Or handle it in another way
            }
            return student;
        }

        public async Task CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if(existingStudent == null)
            {
                throw new Exception($"Student with ID {student.Id} was not found.");
            }

			existingStudent.Name = student.Name;
			existingStudent.Email = student.Email;
			existingStudent.Phone = student.Phone;
			existingStudent.Subscribed = student.Subscribed;

			_context.Students.Update(existingStudent);
			await _context.SaveChangesAsync();
		}

        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                throw new Exception($"Student with ID {id} Not found.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
