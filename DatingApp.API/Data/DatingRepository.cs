using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _conxtext;

        public DatingRepository(DataContext conxtext)
        {
            _conxtext = conxtext;

        }
        public void Add<T>(T entity) where T : class
        {
            _conxtext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _conxtext.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _conxtext.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _conxtext.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _conxtext.SaveChangesAsync() > 0;
        }
    }
}