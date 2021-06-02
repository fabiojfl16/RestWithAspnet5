using RestWithAspnet5.Data.Converter.Implementations;
using RestWithAspnet5.Data.VO;
using RestWithAspnet5.Model;
using RestWithAspnet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspnet5.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var item = _converter.Parse(person);
            item = _repository.Create(item);
            return _converter.Parse(item);
        }

        public PersonVO Update(PersonVO person)
        {
            var item = _converter.Parse(person);
            item = _repository.Update(item);
            return _converter.Parse(item);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}