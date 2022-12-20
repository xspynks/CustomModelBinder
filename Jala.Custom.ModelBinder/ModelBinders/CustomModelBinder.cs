using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Jala.Custom.ModelBinder.ModelBinders;

public class CustomModelBinder: IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // if (bindingContext.ActionContext.HttpContext.Request.Query.Count > 0)
        // {
        //     var id = bindingContext.ActionContext.HttpContext.Request.Query["id"];
        //     var page = new Page()
        //     {
        //         Id = int.Parse(id),
        //         Name = ""
        //     };
        //     bindingContext.Result = ModelBindingResult.Success(page);
        //     return Task.CompletedTask;
        // }
        // return Task.CompletedTask;

        string valueFromBody;
       //The using statement here will dispose the connection opened to read the data from the body stream after it finish the reading
        using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
        {
            //Read the data from body asynchronously 
            valueFromBody = await streamReader.ReadToEndAsync();
        }
        
        //Check if it was possible to read the data from the body stream
        if(string.IsNullOrWhiteSpace(valueFromBody) && string.IsNullOrEmpty(valueFromBody))
            return;

        Page? modelInstance = null;
        try
        {
            //Try to deserialize the string to the Page type
            modelInstance = JsonConvert.DeserializeObject<Page>(valueFromBody);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }
        //Signalizes that the binding was successful and past in the data extracted from the request
        bindingContext.Result = ModelBindingResult.Success(modelInstance);
    }
}