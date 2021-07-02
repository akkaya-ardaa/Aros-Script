using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.CrossCuttingConcerns.Try;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Category category)
        {
            ErrorHandler.Handle(() => {
                _categoryDal.Add(category);
            });
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IResult Delete(Category category)
        {
            ErrorHandler.Handle(() => {
                _categoryDal.Delete(category);
            });
            return new SuccessResult();
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }
    }
}
