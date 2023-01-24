using BlazorSupervision.Shared.Exceptions.Base;
using CommunityToolkit.Diagnostics;

namespace BlazorSupervision.Client.Services
{
  public interface ILogService
  {
    LogDTO GenerateLog(Exception exception);
    
    string GetMessageContent(LogDTO log);
    string GetMessageContent(Exception exception);

    LogDTO Notify(LogDTO log);
    LogDTO Notify(Exception exception);
  }
}
