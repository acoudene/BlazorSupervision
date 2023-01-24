// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1

using BlazorSupervision.Shared.Exceptions;
using BlazorSupervision.Shared.Exceptions.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace BlazorSupervision.Server.Middlewares
{
  /// <summary>
  /// Middleware to manage and wrap exception to known exceptions
  /// </summary>
  public class ExceptionHandlerMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
      this._next = next;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment env, ILogger<ExceptionHandlerMiddleware> logger)
    {
      try
      {
        await _next(context);
      }
      catch (LoggedExceptionBase ex)
      {
        await HandleExceptionAsync(context, ex, logger);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, new ServerException(ex.Message, ex), logger);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, LoggedExceptionBase exception, ILogger<ExceptionHandlerMiddleware> logger)
    {      
      int code = context.Response.StatusCode;
      var log = exception.Log;

      // TODO - ACE: temporaire, à enrichir avec la portée, contexte SERILOG
      logger.LogError(exception,
          "{ErrorMessage} - Id: {Id} - Category: {Category} - [{ExceptionMessages}] - [{StackTrace}]",
          log.Message,
          log.Id,
          log.CategoryName,
          string.Join("|", log.ExceptionMessages),
          log.StackTrace);

      string result = JsonConvert.SerializeObject(log);
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = code;
      return context.Response.WriteAsync(result);
    }
  }
}

