using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        IAuthorDal _authorDal;
        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }
        public Author GetByMail(string email)
        {
            return _authorDal.Get(p => p.Mail == email);
        }

        public List<OperationClaim> GetClaims(Author author)
        {
            return _authorDal.GetClaims(author);
        }
    }
}
