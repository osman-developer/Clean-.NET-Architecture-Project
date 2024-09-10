using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearnArch.Domain.Entities
{
    public class Book : BaseEntity
    {
        public int BookId { get; set; }
        public string Title{ get; set; }
    }
}
