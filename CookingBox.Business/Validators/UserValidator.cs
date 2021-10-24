using CookingBox.Business.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBoxProject.Business.Validators
{
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.address)
                .NotNull().WithMessage("loi roi kia")
                //.LessThan(DateTime.Now);
                .Length(20, 100);
        }
    }
}
