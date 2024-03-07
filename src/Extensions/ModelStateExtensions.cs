using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoApp.Extensions;

public static class ModelStateExtensions
{
    public static List<string> GetErrors(this ModelStateDictionary modelStateDictionary)
    {
        var errors = new List<string>();
        foreach(var value in modelStateDictionary.Values)
            errors.AddRange(value.Errors.Select(x => x.ErrorMessage));
        return errors;
    }
}

