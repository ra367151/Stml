using JetBrains.Annotations;
using Stml.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Outputs
{
    public class UserRegisterDto
    {
        public UserRegisterDto()
        {
            IsSuccess = true;
        }

        public UserRegisterDto([NotNull]string error)
        {
            Check.NotNullOrEmpty(error, nameof(error));
            IsSuccess = false;
            Errors = new List<string> { error };
        }

        public UserRegisterDto([NotNull]IEnumerable<string> errors)
        {
            Check.NotNull(errors, nameof(errors));
            IsSuccess = false;
            Errors = errors;
        }

        public bool IsSuccess { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
    }
}
