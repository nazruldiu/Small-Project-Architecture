using Architecture.DataService.Data;
using Architecture.DataService.IRepository;
using Architecture.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataService.Repository
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext dbContext): base(dbContext)
        {
        }

        public AppUser GetUser(string Username, string Password)
        {
            var user = _dbSet.FirstOrDefault(x => x.Username == Username && x.Password == Password);
            return user;
        }
    }
}
