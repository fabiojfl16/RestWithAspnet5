using RestWithAspnet5.Data.Converter.Implementations;
using RestWithAspnet5.Data.VO;
using RestWithAspnet5.Hypermedia.Utils;
using RestWithAspnet5.Repository;
using System.Collections.Generic;

namespace RestWithAspnet5.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = !string.IsNullOrWhiteSpace(sortDirection) &&
                       !sortDirection.Equals("desc") ? "asc" : "desc";
            
            var size = pageSize < 1 ? 10 : pageSize;
            
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM person p WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = $"{query} AND p.first_name LIKE '%{name}%'";
            }

            query = $"{query} ORDER BY p.first_name {sort} LIMIT {size} OFFSET {offset}";

            string countQuery = "SELECT count(*) FROM person p WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery = $"{countQuery} AND p.first_name LIKE '%{name}%'";
            }

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}