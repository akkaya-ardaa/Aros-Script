using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<Author> Login(AuthorForLoginDto authorForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(Author user);
    }
}
