using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PRN.ExceptionHandler
{
    //ref: https://stackoverflow.com/questions/29184887/best-way-to-check-for-null-parameters-guard-clauses
    public static class Guard
    {
        public static void ThrowIfNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        // other validation methods
    }
}
