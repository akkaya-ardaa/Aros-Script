using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Try;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CommentManager:ICommentService
    {
        ICommentDal _commentDal;
        IArticleService _articleService;
        public CommentManager(ICommentDal commentDal,IArticleService articleService)
        {
            _commentDal = commentDal;
            _articleService = articleService;
        }

        [ValidationAspect(typeof(CommentValidator))]
        public IResult Add(Comment comment)
        {
            comment.Confirmed = false;
            var result = BusinessRules.Run(CheckSwearing(comment.FirstName,comment.LastName,comment.Body),CheckArticleExists(comment.ArticleId));
            if (result != null && !result.Success)
            {
                return result;
            }
            ErrorHandler.Handle(() =>
            {
                _commentDal.Add(comment);
            });
            return new SuccessResult("Yorum eklendi ve onaylandığında gösterilecek.");
        }

        public IDataResult<List<Comment>> GetCommentsByArticle(int articleId)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(p=>p.ArticleId == articleId && p.Confirmed));
        }

        public IResult CheckSwearing(string firstName,string lastName,string body)
        {
            var swearingObjects = new string[] { "orospu","yarrak","amk","piç" }; //küfür yakala
            foreach (var item in swearingObjects)
            {
                if (firstName.ToLower().Contains(item.ToLower()))
                {
                    return new ErrorResult("Yorumda küfür tespit edildi!");
                }
                if (lastName.ToLower().Contains(item.ToLower()))
                {
                    return new ErrorResult("Yorumda küfür tespit edildi!");
                }
                if (body.ToLower().Contains(item.ToLower()))
                {
                    return new ErrorResult("Yorumda küfür tespit edildi!");
                }
            }
            return new SuccessResult();
        }

        public IResult CheckArticleExists(int articleId)
        {
            if(_articleService.GetById(articleId) == null)
            {
                return new ErrorResult("Haber mevcut değil!");
            }
            return new SuccessResult();
        }
    }
}
