using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IArticleService
    {
        IDataResult<List<Article>> GetByCategory(int categoryId);
        IDataResult<List<Article>> Search(string query);
        IResult Add(Article article);
        IResult Delete(int articleId);
    }
}
