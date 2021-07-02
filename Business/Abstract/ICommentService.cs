using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetCommentsByArticle(int articleId);
        IResult Add(Comment comment);
    }
}
