using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using token.Controllers;

public class LoginFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string actionName = (string)context.RouteData.Values["action"];
        string controllerName = (string)context.RouteData.Values["controller"];
        var controller = (HomeController)context.Controller;

        if (HasIgnoreAttribute(context)) { return; }
        context.HttpContext.Session.TryGetValue("token", out var result);
        if (result == null)
        {           
            context.Result = controller.RedirectToAction("Login", "Home");
        }
        else
        {
            string refreshToken=IsRefreshToken(result, context);
            if(!String.IsNullOrWhiteSpace(refreshToken))
            {
                context.Result = controller.RedirectToAction(actionName, controllerName,new { token = refreshToken });
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public bool HasIgnoreAttribute(ActionExecutingContext context)
    {
        foreach (var filterDescriptors in ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes)
        {
            if (filterDescriptors.AttributeType == typeof(IgnoreAttribute))
            {
                return true;
            }
        }
        return false;
    }
    public string IsRefreshToken(byte[] sessionToken, ActionExecutingContext context)
    {
        string tokenSession = System.Text.Encoding.UTF8.GetString(sessionToken).Split('æ')[1];
        DateTime sessionCreateTime = DateTime.Parse(tokenSession);
        TimeSpan remainingTime = DateTime.Now - sessionCreateTime;
        if (remainingTime.TotalSeconds >= 40)
        {            
            string token = Guid.NewGuid().ToString()+ "æ" + DateTime.Now;
            context.HttpContext.Session.Set("token", System.Text.Encoding.UTF8.GetBytes(token));
            return token;
        }
        return string.Empty;
    }
}