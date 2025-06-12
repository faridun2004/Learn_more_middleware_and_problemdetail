namespace RegisterService.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.UtcNow;
    }
}
