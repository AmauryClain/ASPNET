namespace ASPNET.Models.ViewModels
{
    public class AddStudentViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
