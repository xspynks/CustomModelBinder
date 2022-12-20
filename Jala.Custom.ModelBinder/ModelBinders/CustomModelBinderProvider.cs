using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jala.Custom.ModelBinder.ModelBinders;

public class CustomModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(Page) ? new CustomModelBinder() : null;
    }
}