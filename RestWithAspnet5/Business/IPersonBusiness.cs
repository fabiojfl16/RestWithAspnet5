using RestWithAspnet5.Data.VO;
using System.Collections.Generic;

namespace RestWithAspnet5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        PersonVO Update(PersonVO person);

        void Delete(long id);
    }
}