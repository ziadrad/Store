﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorsModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
