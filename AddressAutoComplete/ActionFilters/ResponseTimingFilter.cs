using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class ResponseTimingFilter : IActionFilter
{
    private Stopwatch _stopwatch;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        if (context.Result is ObjectResult result)
        {
            result.Value = new { ElapsedTime = $"{_stopwatch.ElapsedMilliseconds} ms", Data = result.Value };
        }

        context.HttpContext.Response.Headers.Add("X-Response-Time", $"{_stopwatch.ElapsedMilliseconds} ms");
    }
}
