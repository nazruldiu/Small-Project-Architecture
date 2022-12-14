using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entities.DbEntities
{
    public class AppUser : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
    }
}
