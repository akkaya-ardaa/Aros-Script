using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CommentValidator:AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(p => p.FirstName).NotNull().WithMessage("İsim boş olamaz!");
            RuleFor(p => p.Mail).NotNull().WithMessage("E-Posta boş olamaz!");
            RuleFor(p => p.Body).MinimumLength(10).WithMessage("Yorumunuz çok kısa!");
            RuleFor(p => p.ArticleId).NotEmpty().NotNull().NotEqual(0).WithMessage("Hatalı istek!");
        }
    }
}
