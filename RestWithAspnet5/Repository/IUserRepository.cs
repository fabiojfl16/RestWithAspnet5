using RestWithAspnet5.Data.VO;
using RestWithAspnet5.Model;

namespace RestWithAspnet5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string userName);

        bool RevokeToken(string userName);

        User RefreshUserInfo(User user);
    }
}