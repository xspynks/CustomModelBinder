using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jala.Custom.ModelBinder.ModelBinders;

public class RichardProvider : IRichardProvider
{
    public IRichard? GetBinder(RichardProviderContext context)
    {
        return context.Metadata.ModelType == typeof(Page) ? new Richard() : null;
    }
}