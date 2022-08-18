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
    }
}
