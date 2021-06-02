using RestWithAspnet5.Data.Converter.Implementations;
using RestWithAspnet5.Data.VO;
using RestWithAspnet5.Model;
using RestWithAspnet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspnet5.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var item = _converter.Parse(book);
            item = _repository.Create(item);
            return _converter.Parse(item);
        }

        public BookVO Update(BookVO book)
        {
            var item = _converter.Parse(book);
            item = _repository.Update(item);
            return _converter.Parse(item);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}