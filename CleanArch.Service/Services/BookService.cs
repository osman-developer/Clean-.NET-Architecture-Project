using ClearnArch.Domain.DTOs.Book;
using ClearnArch.Domain.Entities;
using ClearnArch.Domain.Interfaces.Services;
using Domain.Interfaces;


namespace CleanArch.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repo;
        public BookService(IGenericRepository<Book> repo)
        {
            _repo = repo;
        }
        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public async Task<Book> Get(int id)
        {
            return await _repo.Get(id);
        }

        public async Task<List<GetBookDTO>> GetAll()
        {
            var bookDb = await _repo.GetAll();
            var bookDTO = bookDb.Select(o => new GetBookDTO()
            {
                BookId = o.BookId,
                Title = o.Title,
              
            }).ToList();

            return bookDTO;
        }

        public bool Save(AddBookDTO book)
        {
            if (book.BookId != null)
            {
                var bookResult = _repo.GetQueryable().Where(b => b.BookId == book.BookId).FirstOrDefault();
                bookResult.BookId = book.BookId.Value;
                bookResult.Title = book.Title;
                return _repo.Update(bookResult);
            }

            var bookDTO = new Book
            {
                Title = book.Title,
            };

            return _repo.Add(bookDTO);
        }
    }
}
