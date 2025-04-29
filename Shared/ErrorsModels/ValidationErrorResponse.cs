﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.ErrorsModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Errors";
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
