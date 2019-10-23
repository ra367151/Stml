using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Inputs.FluentValidators
{
    #region AccountLoginValidatations
    public class AccountLoginValidatations : AbstractValidator<UserLoginInput>
    {
        public AccountLoginValidatations()
        {
            RuleFor(u => u.UserName).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
        }
    }
    #endregion
}
