using ClearnArch.Domain.DTOs.Book;
using ClearnArch.Domain.Entities;


namespace ClearnArch.Domain.Interfaces.Services
{
    public interface IBookService
    {
        bool Save(AddBookDTO book);
        bool Delete(int id);
        Task<List<GetBookDTO>> GetAll();
        Task<Book> Get(int id);
    }
}
