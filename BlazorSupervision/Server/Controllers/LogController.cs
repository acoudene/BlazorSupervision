// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1
// 2022-07-26       | Anthony Coudène (ACE) | MN-221 Integrate Oidc/OAuth2 protocol as unique authentication mode
// 2022-12-14       | Anthony Coudène (ACE) | MN-1198 Adaptation to Full OIDC

using BlazorSupervision.Shared.Exceptions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorSupervision.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LogController : ControllerBase
  {
    private readonly ILogger<LogController> _logger;
    public LogController(ILogger<LogController> logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // TODO ACE - Anonymous for log is it ok?
    [AllowAnonymous]
    [HttpPost]
    public void Post([FromBody] LogDTO log)
    {
      if (log == null) throw new ArgumentNullException(nameof(log));
      WriteLog(log, LogLevel.Error, HttpContext, _logger);
    }

    /// <summary>
    /// écrit dans le log
    /// </summary>
    /// <param name="log"></param>
    /// <param name="level"></param>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    public static void WriteLog(LogDTO log, LogLevel level, HttpContext context, ILogger logger, LoggedExceptionBase? exception = null)
    {
      // TODO - ACE: temporaire, à enrichir avec la portée, contexte SERILOG
      logger?.Log(level, exception,
          // "{ErrorMessage} - Id: {Id} - Category: {Category} - InnerCategory: {InnerCategory} - [{ExceptionMessages}] - [{StackTrace}]", 
          "Error={Message} | GUID={Id} | Category={Category} | InnerCategory={InnerCategory} | Message=[{ExceptionMessages}] | CLIENTSTACKTRACE=[{StackTrace}]",
          log.Message,
          log.Id,
          log.CategoryName,
          log.InnerCategoryName,
          string.Join("|", log.ExceptionMessages),
          log.StackTrace);

      //// TODO ACE: set in an enricher
      //using (Serilog.Context.LogContext.PushProperty("Id", log.Id))
      //using (Serilog.Context.LogContext.PushProperty("InnerExceptions", log.InnerExceptionMessages))
      //using (Serilog.Context.LogContext.PushProperty("StackTrace", log.StackTrace))
      //using (Serilog.Context.LogContext.PushProperty("Category", log.CategoryName))
      //using (Serilog.Context.LogContext.PushProperty("Source", log.Source))
      //{
      //  _logger.Log(log.Level, log.Message);
      //}
    }
  }
}
