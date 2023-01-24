using BlazorSupervision.Client.Resources;
using BlazorSupervision.Shared.Exceptions.Base;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;

namespace BlazorSupervision.Client.Services
{
  public class SnackbarService : ILogService
  {
    private readonly ISnackbar _snackbar;
    private readonly IStringLocalizer<LogResource> _stringLocalizer;

    public SnackbarService(ISnackbar snackbar, IStringLocalizer<LogResource> stringLocalizer)
    {
      Guard.IsNotNull(snackbar);
      Guard.IsNotNull(stringLocalizer);

      _snackbar= snackbar;
      _stringLocalizer = stringLocalizer;
    }

    public LogDTO GenerateLog(Exception exception)
    {
      Guard.IsNotNull(exception);

      var log = default(LogDTO);

      var loggedException = exception as ILoggedException;
      if (loggedException != null)
      {
        log = loggedException.Log;
      }

      log = log ?? new LogDTO()
      {
        Message = exception.Message,
        Exception = exception
      };

      return log;
    }

    public string GetMessageContent(Exception exception) => GetMessageContent(GenerateLog(exception));

    public string GetMessageContent(LogDTO log)
    {
      Guard.IsNotNull(log);

      string categoryName = log.CategoryName ?? nameof(Exception);
      string innerCategoryName = log.InnerCategoryName ?? string.Empty;

      string id = log.Id.ToString();

      // Set dedicated message following to environment
      bool isDevelopment = true;
      string message = isDevelopment
      ?
          $"<ul>" +
          $"<li>{_stringLocalizer[categoryName]}</li>" +
          $"<li>{_stringLocalizer["Category"]}: {categoryName}</li>" +
          $"<li>{_stringLocalizer["Identifier"]}: {id}</li>" +
          $"<li>{_stringLocalizer["Contact support"]}</li>" +
          $"</ul>"
      :
          $"<ul>" +
          $"<li>{_stringLocalizer[categoryName]}</li>" +
          $"<li>{_stringLocalizer["Category"]}: {categoryName}</li>" +
          $"<li>{_stringLocalizer["InnerCategory"]}: {innerCategoryName}</li>" +
          $"<li>{_stringLocalizer["Identifier"]}: {id}</li>" +
          $"<li>{string.Join("|", log.ExceptionMessages)}</li>" + // Dev
          $"<li>{_stringLocalizer["See logs"]} / {_stringLocalizer["Contact support"]}</li>" + // Dev
          $"</ul>";

      return message;
    }

    public LogDTO Notify(Exception exception) => Notify(GenerateLog(exception));

    public LogDTO Notify(LogDTO log)
    {
      Guard.IsNotNull(log);

      string message = GetMessageContent(log);

      // Create the notification with error options
      _snackbar.Add(message, Severity.Error, options =>
      {
        options.ShowCloseIcon = true;
        options.VisibleStateDuration = 60000;
        options.HideTransitionDuration = 500;
        options.ShowTransitionDuration = 500;
        options.SnackbarVariant = Variant.Filled;
      });

      return log;
    }
  }
}
