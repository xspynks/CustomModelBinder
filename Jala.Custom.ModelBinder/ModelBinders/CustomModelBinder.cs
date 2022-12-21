using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Jala.Custom.ModelBinder.ModelBinders;

    public class CustomModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            int? queryId = null;

            if (bindingContext.ActionContext.HttpContext.Request.Query.Count > 0)
            {
                if (int.TryParse(bindingContext.ActionContext.HttpContext.Request.Query["id"], out var id))
                {
                    queryId = id;
                }
            }

            string valueFromBody;
            using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = await streamReader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(valueFromBody))
            {
                return;
            }

            Page modelInstance;
            try
            {
                modelInstance = JsonConvert.DeserializeObject<Page>(valueFromBody);
                if (queryId.HasValue)
                {
                    modelInstance.Id = queryId.Value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            bindingContext.Result = ModelBindingResult.Success(modelInstance);
        }
    }
