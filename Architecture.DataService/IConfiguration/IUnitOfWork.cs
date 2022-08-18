using Architecture.DataService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataService.IConfiguration
{
    public interface IUnitOfWork
    {
        IAppUserRepository appUser { get; set; }
        Task SaveAsync();
    }
}
