using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ArticleValidator:AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(p => p.Title).MaximumLength(100).WithMessage("Haber başlıkları en fazla 100 haneli olmalıdır.");
            RuleFor(p => p.CategoryId).NotNull().WithMessage("Kategori boş olamaz.");
            RuleFor(p => p.AuthorId).NotNull().WithMessage("Yazar boş olamaz?");
        }
    }
}
