using FluentValidation;
using Stml.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Inputs.FluentValidators
{
    public class UserCreateInputValidator : AbstractValidator<UserCreateInput>
    {
        public UserCreateInputValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .Length(4, 15);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            // identity password default regex: ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$
            // (?=.*[a-z]) : Should have at least one lower case
            // (?=.*[A-Z]) : Should have at least one upper case
            // (?=.*\d) : Should have at least one number
            // (?=.*[#$^+=!*()@%&] ) : Should have at least one special character
            // .{8,} : Minimum 8 characters
            RuleFor(u => u.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-zA-Z])(?=.*\d).{6,}$")
                .WithMessage("密码必须包含至少6位字母数字");

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password);
        }
    }
}
