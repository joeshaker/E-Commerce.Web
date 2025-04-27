using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiResponseValidationError(ActionContext context)
        {
            var Error = context.ModelState.Where(M => M.Value.Errors.Any()).
                Select(M => new ValidationError()
                {
                    Field = M.Key,
                    Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                });
            var Respone = new ValidationToReturn()
            {
                ValidationErrors = Error
            };
            return new BadRequestObjectResult(Respone);

        }
    }
}
