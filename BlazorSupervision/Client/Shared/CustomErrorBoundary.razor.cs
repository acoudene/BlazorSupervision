﻿// Changelogs Date  | Author                | Description
// 2022-05-16       | Anthony Coudène(ACE)  | Creation - Make code more resilient step 1
// 2022-06-17       | Anthony Coudène (ACE) | MN-915 Change client architecture to better manage lifecycles
// 2022-06-22       | Anthony Coudène (ACE) | MN-934 Add CancellationToken management

using BlazorSupervision.Client.Resources;
using BlazorSupervision.Client.Services;
using BlazorSupervision.Shared.Exceptions.Base;
using CommunityToolkit.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorSupervision.Client.Shared
{
  /// <summary>
  /// Code-behind to manage specifically all error/exceptions
  /// </summary>
  public partial class CustomErrorBoundary
  {
    private readonly List<Exception> _receivedExceptions = new();
    private int _nbOfRetries = 0;

    [Inject]
    private ILogService? LogService { get; set; }

    [Inject]
    private IHttpClientFactory? ClientFactory { get; set; } // TODO - ACE: change to Proxy layer 

    protected string GetMessageContent(Exception exception)
    {
      Guard.IsNotNull(exception);
      Guard.IsNotNull(LogService);

      return LogService.GetMessageContent(exception);
    }

    protected override async Task OnErrorAsync(Exception exception)
    {
      Guard.IsNotNull(exception);
      Guard.IsNotNull(LogService);
      Guard.IsNotNull(ClientFactory);

      var log = LogService.Notify(exception);

      // Add current exception to all received exceptions
      _receivedExceptions.Add(exception);
      await base.OnErrorAsync(exception);

      try
      {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(10000);
        var httpClient = ClientFactory.CreateClient();
        await httpClient.PostObjectAsync<LogDTO>(log, "Log", cancellationTokenSource.Token);
      }
      catch (Exception)
      {
        // Do nothing for the moment
      }
    }

    public void Reset()
    {
      _nbOfRetries = 0;
      _receivedExceptions.Clear();
    }

    public new void Recover()
    {
      Reset();
      base.Recover();
    }
  }
}