using RestWithAspnet5.Configurations;
using RestWithAspnet5.Data.VO;
using RestWithAspnet5.Repository;
using RestWithAspnet5.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithAspnet5.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusiness(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _repository.RefreshUserInfo(user);

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO
            (
                true,
                createDate.ToString(DateFormat),
                expirationDate.ToString(DateFormat),
                accessToken,
                refreshToken
            );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;
            var user = _repository.ValidateCredentials(userName);

            if(user == null || 
               user.RefreshToken != refreshToken || 
               user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null;
            }

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO
            (
                true,
                createDate.ToString(DateFormat),
                expirationDate.ToString(DateFormat),
                accessToken,
                refreshToken
            );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}