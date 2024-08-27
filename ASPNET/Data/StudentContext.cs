using ASPNET.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNET.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
