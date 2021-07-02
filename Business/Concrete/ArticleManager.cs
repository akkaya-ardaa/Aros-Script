using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Try;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Business.Concrete
{
    public class ArticleManager:IArticleService
    {
        IAuthorService _authorService;
        IArticleDal _articleDal;
        IHttpContextAccessor _httpContextAccessor;
        public ArticleManager(IAuthorService authorService,IArticleDal articleDal,IHttpContextAccessor httpContextAccessor)
        {
            _authorService = authorService;
            _articleDal = articleDal;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(ArticleValidator))]
        public IResult Add(Article article)
        {
            ErrorHandler.Handle(() => {
                _articleDal.Add(article);
            });
            return new SuccessResult();
        }

        public IResult Delete(int articleId)
        {
            Debug.WriteLine(articleId);
            var result = BusinessRules.Run(CheckArticleOwnership(articleId,int.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId())));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            Debug.WriteLine(int.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId()));
            ErrorHandler.Handle(()=> {
                _articleDal.Delete(new Article() { Id = articleId });
            });
            return new SuccessResult();
        }

        public IDataResult<List<Article>> GetByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(p=>p.CategoryId == categoryId));
        }

        public IResult CheckArticleOwnership(int articleId,int authorId)
        {
            var isModOrAdmin = false;
            _authorService.GetClaims(new Author() { Id = authorId }).ForEach(p => { 
               
                if(p.Name == "admin" || p.Name == "moderator")
                {
                    isModOrAdmin = true;
                }

            });

            if (isModOrAdmin)
            {
                return new SuccessResult();
            }

            var article = _articleDal.Get(p => p.Id == articleId);
            if (article.AuthorId == authorId)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Bu haber size ait değil. Dolayısıyla silemezsiniz!");
        }

        public IDataResult<List<Article>> Search(string query)
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(p=>p.Title.ToLower().Contains(query.ToLower())));
        }

        public Article GetById(int articleId)
        {
            return _articleDal.Get(p => p.Id == articleId);
        }
    }
}
