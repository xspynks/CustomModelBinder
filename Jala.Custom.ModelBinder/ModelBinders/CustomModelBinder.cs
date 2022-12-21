using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Jala.Custom.ModelBinder.ModelBinders
{
    public class CustomModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Initialize a nullable integer variable to null
            int? queryId = null;

            // Check if the request has any query string parameters
            if (bindingContext.ActionContext.HttpContext.Request.Query.Count > 0)
            {
                // Try to parse the value of the "id" query string parameter as an integer
                if (int.TryParse(bindingContext.ActionContext.HttpContext.Request.Query["id"], out var id))
                {
                    // If the parsing is successful, assign the parsed integer value to the queryId variable
                    queryId = id;
                }
            }

            // Read the body of the request into a string
            string valueFromBody;
            using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = await streamReader.ReadToEndAsync();
            }

            // If the body of the request is null or white space, return immediately
            if (string.IsNullOrWhiteSpace(valueFromBody))
            {
                return;
            }

            // Try to deserialize the body of the request into a Page object
            Page modelInstance;
            try
            {
                modelInstance = JsonConvert.DeserializeObject<Page>(valueFromBody);

                // If the queryId variable has a value, set the Id property of the Page object to that value
                if (queryId.HasValue)
                {
                    modelInstance.Id = queryId.Value;
                }
            }
            catch (Exception e)
            {
                // If an exception is thrown during, set the Result property of the bindingContext object to a ModelBindingResult with a Failed value and return
                Console.WriteLine(e);
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            // If the deserialization is successful, set the Result property of the bindingContext object to a ModelBindingResult
            // with a Success value and the deserialized Page object as its value
            bindingContext.Result = ModelBindingResult.Success(modelInstance);
        }
    }
}