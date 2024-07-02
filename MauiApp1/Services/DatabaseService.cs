using SQLite;
using System.IO;
using System.Threading.Tasks;
using MauiApp1.models; 

namespace MauiApp1.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;
        private static DatabaseService _instance = null! ;

        public static DatabaseService Instance => _instance ??= new DatabaseService();

        private DatabaseService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Whaoinoiunt.db3");

            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<User>().Wait();
        }

        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync(); 
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
