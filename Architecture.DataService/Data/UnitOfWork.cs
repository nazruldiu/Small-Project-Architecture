using Architecture.DataService.IConfiguration;
using Architecture.DataService.IRepository;
using Architecture.DataService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected AppDbContext _appDdContext;
        public IAppUserRepository appUser {get; set;}

        public UnitOfWork(AppDbContext appDdContext)
        {
            _appDdContext = appDdContext;
            appUser = new AppUserRepository(appDdContext);
        }
        public async Task SaveAsync()
        {
           await _appDdContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDdContext.Dispose();
        }
    }
}
