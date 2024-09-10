using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearnArch.Domain.DTOs.Book
{
    public class AddBookDTO
    {
        public int? BookId { get; set; }
        public string Title { get; set; }
    }
}
