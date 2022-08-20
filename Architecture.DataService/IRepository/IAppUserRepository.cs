using Architecture.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataService.IRepository
{
    public interface IAppUserRepository: IGenericRepository<AppUser>
    {
        AppUser GetUser(string Username, string Password);
    }
}
