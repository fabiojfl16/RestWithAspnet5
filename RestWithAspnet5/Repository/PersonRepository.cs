using RestWithAspnet5.Model;
using RestWithAspnet5.Model.Context;
using RestWithAspnet5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspnet5.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base(context)
        {
        }

        public Person Disable(long id)
        {
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user == null)
            {
                return null;
            }

            user.Enabled = false;

            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            return _context.Persons.Where(p =>
                (string.IsNullOrEmpty(firstName) || p.FirstName.Contains(firstName)) &&
                (string.IsNullOrEmpty(lastName) || p.LastName.Contains(lastName))).ToList();
        }
    }
}