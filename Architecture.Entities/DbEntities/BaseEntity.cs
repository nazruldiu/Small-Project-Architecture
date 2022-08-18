using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entities.DbEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int Status { get; set; } = 1;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
