using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        List<OperationClaim> GetClaims(Author author);
        Author GetByMail(string email);
    }
}
