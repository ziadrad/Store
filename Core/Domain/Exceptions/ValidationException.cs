using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> errors) : Exception(message:"Validation Errors")

    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}
