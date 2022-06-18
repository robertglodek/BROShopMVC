using BRO.Domain.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.Extensions
{
    public static class ModelStateExtensions
    {
        public static void PopulateValidation(this ModelStateDictionary modelState, IEnumerable<Result.Error> errors, string errorMessage, string prefix = null, List<string> errorsWithNoPrefix = null)
        {
            modelState.Clear();
            if (errors.Count() > 0)
            {
                foreach (var error in errors)
                {
                    if (errorsWithNoPrefix != null)
                    {
                        if (errorsWithNoPrefix.Contains(error.PropertyName))
                            modelState.AddModelError(error.PropertyName, error.Message);
                        else
                            modelState.AddModelError(prefix + error.PropertyName, error.Message);
                    }
                    else
                        modelState.AddModelError(prefix + error.PropertyName, error.Message);
                }
            }
            else
                modelState.AddModelError("", errorMessage);
        }
        public static void AddError(this ModelStateDictionary modelState, string errorProperty, string errorMessage)
        {
            if (errorProperty != null)
                modelState.AddModelError(errorProperty, errorMessage);
            else
                modelState.AddModelError("", errorMessage);
        }
    }
}
