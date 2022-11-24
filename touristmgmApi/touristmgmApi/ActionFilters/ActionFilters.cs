using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace touristmgmApi.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new Dictionary<string, string>();

                foreach (var key in context.ModelState.Keys)
                {
                    result.Add(key, string.Join(",", context.ModelState[key].Errors.Select(p => p.ErrorMessage)));
                }

                context.Result = new ObjectResult(result) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
