using SQLite;

namespace MauiApp1.models
{
    public class User
    {
        [PrimaryKey, MaxLength(100), Unique]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
