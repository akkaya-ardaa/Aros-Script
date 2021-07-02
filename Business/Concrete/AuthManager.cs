using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private IAuthorService _authorService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IAuthorService authorService, ITokenHelper tokenHelper)
        {
            _authorService = authorService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<Author> Login(AuthorForLoginDto authorForLoginDto)
        {
            var userToCheck = _authorService.GetByMail(authorForLoginDto.Mail);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Author>("Kullanıcı bulunamadı");
            }

            if (new PasswordHasher().VerifyHashedPassword(userToCheck.HashedPassword,authorForLoginDto.Password) != PasswordVerificationResult.Success)
            {
                return new ErrorDataResult<Author>("Parola hatası");
            }

            return new SuccessDataResult<Author>(userToCheck, "Başarılı giriş");
        }

        public IDataResult<AccessToken> CreateAccessToken(Author user)
        {
            var claims = _authorService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }
    }
}
