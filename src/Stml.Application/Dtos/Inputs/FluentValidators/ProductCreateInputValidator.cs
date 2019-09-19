using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Inputs.FluentValidators
{
    public class ProductCreateInputValidator : AbstractValidator<ProductCreateInputDto>
    {
        public ProductCreateInputValidator()
        {
            RuleFor(r => r.Name).NotEmpty().Length(0, 20).WithMessage("产品名称长度必须在0-20之间");
            RuleFor(r => r.Price).InclusiveBetween(0, 1000).WithMessage("产品必须在0-1000之间");
        }
    }
}
