using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Dto
{
    public class ServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string[] Errors { get; private set; }

        private ServiceResult()
        {
            IsSuccess = true;
        }

        private ServiceResult(params string[] errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        public static ServiceResult Success = new ServiceResult();

        public static ServiceResult Fail(params string[] errors) => new ServiceResult(errors);
    }
}
