using RestWithAspnet5.Model;
using RestWithAspnet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspnet5.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);

        List<Person> FindByName(string firstName, string lastName);
    }
}