using RestWithAspnet5.Model;
using RestWithAspnet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspnet5.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book Book)
        {
            return _repository.Create(Book);
        }

        public Book Update(Book Book)
        {
            return _repository.Update(Book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}