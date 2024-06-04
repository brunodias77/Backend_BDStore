using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BDS.Api.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected List<string> Errors = [];

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperationValid())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "MessagesHttp", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddErrors(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddErrors(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool OperationValid()
    {
        return !Errors.Any();
    }

    protected bool OperationInvalid()
    {
        return !OperationValid();
    }

    protected void AddErrors(params string[] errors)
    {
        Errors.AddRange(errors);
    }

    protected void ClearErrors()
    {
        Errors.Clear();
    }
}